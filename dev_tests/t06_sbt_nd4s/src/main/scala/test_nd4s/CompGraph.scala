package test_nd4s

import org.nd4j.linalg.factory.Nd4j
import org.nd4j.linalg.api.ndarray.INDArray

/*
1) simple seq a -> b is straight forward
  dL / da = dL / db * db / da
2) a -> b, a -> c
  dL / da = dL / db * db / da + dL / dc * dc / da
  => dL / da depends on dL / db and dL / dc
3) a -> c, b -> c
  dL / da = dL / dc * dc / da
*/


class CompGraphState(in: Map[String, INDArray], var nodesState: Map[String, CompState]) extends CompState(in)

class CompGraph(private val nodes: Map[String, CompNode],
                private val inputs: Set[String],
                private val outputs: Set[String],
                private val links: Set[(String, String)]) extends CompNode{
  //0. assert names does not contain .
  assert(nodes.keySet.forall(!_.contains(".")))
  assert(inputs.forall(!_.contains(".")))
  assert(outputs.forall(!_.contains(".")))
  //1. assert local names are unique
  private val nodeInputs = nodes.flatMap{case (k,v) => v.input.map{n => (k+"."+n, (k, n))}}.toMap
  private val nodeOutputs = nodes.flatMap{case (k,v) => v.output.map{n => (k+"."+n, (k, n))}}.toMap
  private val nodeVariables = nodes.flatMap{case (k,v) => v.variables.map{n => (k+"."+n, (k, n))}}.toMap
  assert(nodeInputs.size == nodes.values.map(_.input.size).reduceLeft(_+_))
  assert(nodeOutputs.size == nodes.values.map(_.output.size).reduceLeft(_+_))
  assert(nodeVariables.size == nodes.values.map(_.variables.size).reduceLeft(_+_))
  //2. assert all names are unique
  assert((nodeInputs.keySet ++ nodeOutputs.keySet ++ nodeVariables.keySet ++ inputs ++ outputs).size ==
    (nodeInputs.size + nodeOutputs.size + nodeVariables.size + inputs.size + outputs.size))
  //3. assert links are correct
  assert(links.map(_._1).diff(inputs ++ nodeOutputs.keySet).isEmpty) //links start is correct
  assert(links.map(_._2).diff(outputs ++ nodeInputs.keySet).isEmpty) //link end is correct
  assert(!CompGraphBuilder.existCycle(links.toList)) //no cycles (original names)

  private val depLinks = links.map{case (v1, v2) => (v1.split('.')(0), v2.split('.')(0))}
  assert(!CompGraphBuilder.existCycle(depLinks.toList)) //no cycles (node-level names)
  //4. assert all internal-inputs and internal-outputs are set
  assert(nodeInputs.keySet.diff(links.map(_._2)).isEmpty) //all inputs are set
  assert(nodeOutputs.keySet.diff(links.map(_._1)).isEmpty) //all outputs are set
  //5. assert there are no external-inputs and external-outputs hanging
  assert(inputs.diff(links.map(_._1)).isEmpty) //all inputs are fed out
  assert(outputs.diff(links.map(_._2)).isEmpty) //all outputs are fed in
  assert(links.groupBy(_._2).forall(_._2.size == 1)) //no 2+ links for 1 input

  private val fwdDependencies = depLinks.groupBy(_._2).mapValues(_.map(_._1))
  private val bwdDependencies = depLinks.groupBy(_._1).mapValues(_.map(_._2))
  private val fwdLinks = links.groupBy(_._1).mapValues(_.map(_._2)) //map of set[string]
  private val bwdLinks = links.groupBy(_._2).mapValues(_.head._1) //map of string

  override def input : Set[String] = inputs
  override def output : Set[String] = outputs
  override def variables : Set[String] = nodeVariables.keySet

  override def get(name: String) : INDArray = {
    assert(nodeVariables.contains(name))
    val (node, iname) = nodeVariables(name)
    nodes(name).get(iname)
  }
  override def update(dv: Map[String, INDArray]) : Unit = {
    assert(dv.keySet.subsetOf(nodeVariables.keySet))
    dv.groupBy[String]{case (kk, vv) => nodeVariables(kk)._1}.foreach{case (kn, mp) => {
      nodes(kn).update(mp.map{case (k, q) => (nodeVariables(k)._2, q)})
    }}
  }
  override def init(v: Map[String, INDArray]) : Unit = {
    assert(v.keySet.subsetOf(nodeVariables.keySet))
    v.groupBy[String]{case (kk, vv) => nodeVariables(kk)._1}.foreach{case (kn, mp) => {
      nodes(kn).init(mp.map{case (k, q) => (nodeVariables(k)._2, q)})
    }}
  }

  private def assembleInput(nodeName: String,
                            globalIn: Map[String, INDArray],
                            calcCache: Map[String, INDArray]) : Map[String, INDArray] = {
    val inNames = nodes(nodeName).input
    val tmp = inNames.map{x => (x, bwdLinks(nodeName + "." + x))}.toMap
    tmp.mapValues{x => if(inputs.contains(x)) globalIn(x) else calcCache(x)}
  }
  private def decode(nodeName: String, nodeOut: Map[String, INDArray]) : Map[String, INDArray] = {
    nodeOut.map{case (k, v) => (nodeName + "." + k, v)}
  }

  private def assembleGradients(nodeName: String,
                                globalOut: Map[String, INDArray],
                                calcCache: Map[String, INDArray]) : Map[String, INDArray] = {
    val outNames = nodes(nodeName).output
    val tmp = outNames.map{x => (x, fwdLinks(nodeName + "." + x))}.toMap
    tmp.mapValues(_.map{x => if(outputs.contains(x)) globalOut(x) else calcCache(x)}.reduceLeft(_.add(_)))
  }

  private def traverseGraph[R](startSet: Set[String], endSet: Set[String], depMap: Map[String, Set[String]],
                               calcFun: (String, collection.mutable.Map[String, R]) => R) : Map[String, R] = {
    val toCalc = endSet.to[collection.mutable.Stack]
    val rCache = collection.mutable.Map[String, R]()
    val sFinished = startSet.to[collection.mutable.Set]
    while(toCalc.nonEmpty){
      val calcName = toCalc.pop()
      if(!sFinished.contains(calcName)){
        //depMap will contain calcName
        assert(depMap.contains(calcName))
        val deps = depMap(calcName).filter(!sFinished.contains(_))
        if(deps.nonEmpty){
          toCalc.push(calcName)
          deps.foreach(toCalc.push(_))
        }else{
          if(!endSet.contains(calcName))
            rCache += ((calcName, calcFun(calcName, rCache)))
          sFinished += calcName
        }
      }
    }
    rCache.toMap
  }

  override def forward(in: Map[String, INDArray]) : (Map[String, INDArray], CompState) = {
    assert(inputs.subsetOf(in.keySet))

    def forwardCalc(nodeName: String, cache: collection.mutable.Map[String, (Map[String, INDArray], CompState)])
            : (Map[String, INDArray], CompState) = {
      val pCache = cache.values.map(_._1).reduceLeftOption(_++_).getOrElse(Map[String, INDArray]())
      val (nOut, nState) = nodes(nodeName).forward(assembleInput(nodeName, in, pCache))
      (decode(nodeName, nOut), nState)
    }

    val cache = traverseGraph(inputs, outputs, fwdDependencies, forwardCalc)
    val pCache = cache.values.map(_._1).reduceLeft(_++_)
    (outputs.map{x => (x, pCache(bwdLinks(x)))}.toMap,
      new CompGraphState(in, cache.mapValues(_._2)))
  }

  override def backward(dOut: Map[String, INDArray], s: CompState) : Map[String, INDArray] = {
    assert(outputs.subsetOf(dOut.keySet))
    assert(s.isInstanceOf[CompGraphState])
    val gs = s.asInstanceOf[CompGraphState]

    def backwardCalc(nodeName: String, cache: collection.mutable.Map[String, Map[String, INDArray]])
            : Map[String, INDArray] = {
      val pCache = cache.values.reduceLeftOption(_++_).getOrElse(Map[String, INDArray]())
      val nGradient = nodes(nodeName).backward(assembleGradients(nodeName, dOut, pCache),
                                              gs.nodesState(nodeName))
      decode(nodeName, nGradient)
    }

    val cache = traverseGraph(outputs, inputs, bwdDependencies, backwardCalc)
    val pCache = cache.values.reduceLeft(_++_)

    val dIn = inputs.map{x => (x, fwdLinks(x).map(pCache(_)).reduceLeft(_.add(_)))}.toMap
    val dVar = variables.map{x => (x, pCache(x))}.toMap
    dIn ++ dVar
  }
}

object CompGraph{
  def link(n1 : CompNode, n2 : CompNode, links: Seq[(String, String)]) : CompGraph = {
    val cgb = new CompGraphBuilder
    cgb.addNodes(Map("n1" -> n1, "n2" -> n2))
    cgb.addInputs(n1.input ++ n2.input.diff(links.map(_._2).toSet))
    cgb.addOutputs(n2.output)
    //input links
    cgb.addLinks((n1.input.map{x => (x, "n1."+x)} ++ n2.input.diff(links.map(_._2).toSet).map{x => (x, "n2."+x)}).toSeq)
    //output links
    cgb.addLinks(n2.output.map{x => ("n2."+x, x)}.toSeq)
    //intra links
    cgb.addLinks(links.map{case (x,y) => ("n1."+x, "n2."+y)})
    cgb.graph
  }
}