package test_nd4s

import org.nd4j.linalg.factory.Nd4j
import org.nd4j.linalg.api.ndarray.INDArray
import org.nd4s.Implicits._

class CompStateSeq(in: Map[String, INDArray], var seqs: IndexedSeq[CompState]) extends CompState(in)

class LinkSeq(layers: IndexedSeq[CompNode],
              nameMap : IndexedSeq[Map[String, String]],
              inputMap : Map[String, String],
              outputMap : Map[String, String]) extends CompNode {
  assert(layers.length == nameMap.length)
  //name completeness
  //name uniqueness
  for(i <- layers.indices){
    assert(layers(i).variables.subsetOf(nameMap(i).keySet))
    assert(layers(i).input.subsetOf(nameMap(i).keySet))
    assert(layers(i).output.subsetOf(nameMap(i).keySet))
    assert(nameMap(i).values.size == nameMap(i).values.toSet.size)
  }
  private val allNames = nameMap.map(_.values.toSet).reduceLeft(_.union(_))
  //input uniqueness & correctness
  assert(inputMap.values.size == inputMap.values.toSet.size)
  assert(inputMap.keySet.subsetOf(allNames))
  //output uniqueness & correctness
  assert(outputMap.values.size == outputMap.values.toSet.size)
  assert(outputMap.keySet.subsetOf(layers.last.output.map(nameMap.last(_))))
  //input fullness
  private val allInputs = layers.zipWithIndex.map{case (layer, ind) =>
      layer.input.map(nameMap(ind)(_))
  }.reduceLeft(_.union(_))
  private val allOutputs = layers.zipWithIndex.map{case (layer, ind) =>
    layer.output.map(nameMap(ind)(_))
  }.reduceLeft(_.union(_))
  private val realInputs = allInputs.diff(allOutputs)
  assert(realInputs == inputMap.keySet)

  private val finInputs = realInputs.map(inputMap(_))
  private val finOutputs = outputMap.values.toSet
  private val finVars = layers.zipWithIndex.map{case (layer, ind) => layer.variables.map(nameMap(ind)(_))}.reduceLeft(_.union(_))
  private val revMaps = nameMap.map(_.map(_.swap))
  private val revInput = inputMap.map(_.swap)
  private val revOutput = outputMap.map(_.swap)

  private def toLayerInput(i: Int, v: Map[String, INDArray]) : Map[String, INDArray] = {
    v.filterKeys(revMaps(i).contains(_)).map{case (k,q) => (revMaps(i)(k), q)}.filterKeys(layers(i).input.contains(_))
  }
  private def toLayerOutput(i: Int, v: Map[String, INDArray]) : Map[String, INDArray] = {
    v.filterKeys(revMaps(i).contains(_)).map{case (k,q) => (revMaps(i)(k), q)}.filterKeys(layers(i).output.contains(_))
  }

  private def combineMaps(highPriority: Map[String, INDArray], lowPriority: Map[String, INDArray]) : Map[String, INDArray] = {
    highPriority.keySet.union(lowPriority.keySet).map{k =>
      (k, if(highPriority.contains(k)) highPriority(k) else lowPriority(k))}.toMap
  }

  override def forward(in: Map[String, INDArray]) : (Map[String, INDArray], CompStateSeq) = {
    assert(in.keySet.subsetOf(finInputs))
    var cur_in = in.map{case (k,v) => (revInput(k), v)}
    val sseq = new Array[CompState](layers.size)

    for((layer, ind) <- layers.zipWithIndex){
      val (cur_out, cs) = layer.forward(toLayerInput(ind, cur_in))
      cur_in = combineMaps(cur_out.map{case (k, v) => (nameMap(ind)(k), v)}, cur_in)
      sseq(ind) = cs
    }

    val res = outputMap.map{case (k, v) => (v, cur_in(k))}
    (res, new CompStateSeq(in, sseq))
  }
  override def backward(dOut: Map[String, INDArray], s: CompState) : Map[String, INDArray] = {
    assert(s.isInstanceOf[CompStateSeq])
    assert(dOut.keySet == revOutput.keySet)
    val sseq = s.asInstanceOf[CompStateSeq]
    var cur_res = dOut.map{case (k,v) => (revOutput(k), v)}
    for((layer, ind) <- layers.zipWithIndex.reverse){
      val new_dOut = layer.backward(toLayerOutput(ind, cur_res), sseq.seqs(ind)).map{case (k, v) => (nameMap(ind)(k), v)}
      cur_res = combineMaps(new_dOut, cur_res)
    }
    cur_res.filterKeys{k => finVars.contains(k) || realInputs.contains(k)}.map{case (k, v) =>
        (if(finVars.contains(k))k else inputMap(k), v)}
  }

  override def get(name: String) : INDArray = {
    assert(finVars.contains(name))
    val ind = revMaps.indexWhere(_.contains(name))
    layers(ind).get(revMaps(ind)(name))
  }

  override def update(dv: Map[String, INDArray]) : Unit = {
    assert(dv.keySet.subsetOf(finVars))
    for(i <- layers.indices){
      layers(i).update(dv.filterKeys(revMaps(i).contains(_)).map{case (k, q) => (revMaps(i)(k), q)})
    }
  }
  override def init(v: Map[String, INDArray]) : Unit = {
    assert(v.keySet.subsetOf(finVars))
    for(i <- layers.indices){
      layers(i).update(v.filterKeys(revMaps(i).contains(_)).map{case (k, q) => (revMaps(i)(k), q)})
    }
  }

  override def input = finInputs
  override def output = finOutputs
  override def variables: Set[String] = finVars
}

object LinkSeq{
  def apply(cn1: CompNode, cn2: CompNode, out1Map: Map[String, String], in2Map: Map[String, String]): LinkSeq = {
    //nameMap
    val nameMap1 = List(cn1.variables.map{x => (x, "var1."+x)},
                        cn1.input.map{x => (x, "in."+x)},
                        cn1.output.diff(out1Map.keySet).map{x => (x, "disc."+x)},
                        cn1.output.intersect(out1Map.keySet).map{x => (x, "tmp."+out1Map(x))}).reduceLeft(_.union(_)).toMap
    val nameMap2 = List(cn2.variables.map{x => (x, "var2."+x)},
                        cn2.input.diff(in2Map.keySet).map{x => (x, "in."+x)},
                        cn2.input.intersect(in2Map.keySet).map{x => (x, "tmp."+in2Map(x))},
                        cn2.output.map{x => (x, "out."+x)}).reduceLeft(_.union(_)).toMap
    println(nameMap1)
    println(nameMap2)

    //inMap
    val in2 = cn2.input.diff(in2Map.keySet)
    val inMap = cn1.input.map{x => ("in."+x, x)}.union(in2.map{x => ("in."+x, x)}).toMap
    //outMap
    val outMap = cn2.output.map{x => ("out."+x, x)}.toMap

    println(inMap)
    println(outMap)
    new LinkSeq(Array(cn1, cn2), Array(nameMap1, nameMap2), inMap, outMap)
  }
}