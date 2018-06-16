package test_nd4s

import scala.collection.{GenSet, mutable}

class CompGraphBuilder {
  //actual nodes
  private val nodes = scala.collection.mutable.Map[String, CompNode]()

  //renames variables (or inputs/outputs) into <node_name>.<var_name> with value (<node_name>, <var_name>)
  private val nodeInputs = scala.collection.mutable.Map[String, (String, String)]()
  private val nodeOutputs = scala.collection.mutable.Map[String, (String, String)]()
  private val nodeVariables = scala.collection.mutable.Map[String, (String, String)]()

  //actual inputs/outputs
  private val inputs = scala.collection.mutable.Set[String]()
  private val outputs = scala.collection.mutable.Set[String]()

  private val links = scala.collection.mutable.ArrayBuffer[(String, String)]()

  private def extNames = nodes.keySet ++ inputs ++ outputs
  private def intNames = nodeInputs.keySet ++ nodeOutputs.keySet ++ nodeVariables.keySet
  private def allNames = extNames ++ intNames
  private def link0Names = inputs ++ nodeOutputs.keySet
  private def link1Names = outputs ++ nodeInputs.keySet
  private def isNameSuitable(v: String) : Boolean = (v.length > 0) && (v != ".")

  def addNodes(nds: Map[String, CompNode]) : Unit = {
    assert(!nds.keySet.map(isNameSuitable(_)).contains(false))

    val newIntNames = nds.flatMap{case (k,v) => v.input.map(k+"."+_)}.toSet ++
                      nds.flatMap{case (k,v) => v.output.map(k+"."+_)}.toSet ++
                      nds.flatMap{case (k,v) => v.variables.map(k+"."+_)}.toSet
    assert(allNames.intersect(nds.keySet ++ newIntNames).isEmpty, "addNodes: name already exists!")

    val mapInputs = nds.flatMap{case (k,v) => v.input.map{n => (k+"."+n, (k, n))}}.toMap
    val mapOutputs = nds.flatMap{case (k,v) => v.output.map{n => (k+"."+n, (k, n))}}.toMap
    val mapVariables = nds.flatMap{case (k,v) => v.variables.map{n => (k+"."+n, (k, n))}}.toMap

    nodeInputs ++= mapInputs
    nodeOutputs ++= mapOutputs
    nodeVariables ++= mapVariables
    nodes ++= nds
  }
  def addInputs(in: Set[String]) : Unit = {
    assert(!in.map(isNameSuitable(_)).contains(false))

    assert(allNames.intersect(in).isEmpty, "addInputs: name already exists!")
    inputs ++= in
  }
  def addOutputs(out: Set[String]) : Unit = {
    assert(!out.map(isNameSuitable(_)).contains(false))

    assert(allNames.intersect(out).isEmpty, "addOutputs: name already exists!")
    outputs ++= out
  }
  def addLinks(lnks: Seq[(String, String)]) : Unit = {
    assert(links.intersect(lnks).isEmpty, "addLinks: link already exists!")
    assert(links.map(_._1).toSet.diff(link0Names).isEmpty, "addLinks: incorrect link source!")
    assert(links.map(_._2).toSet.diff(link1Names).isEmpty, "addLinks: incorrect link source!")
    assert(!CompGraphBuilder.existCycle(links ++ lnks), "addLinks: new links create cycle in graph")
    links ++= lnks
  }
  def pendingNodeInputs = nodeInputs.keySet.diff(links.map(_._2).toSet)
  def unusedNodeOutputs = nodeOutputs.keySet.diff(links.map(_._1).toSet)
  def hangingInputs = inputs.toSet.diff(links.map(_._1).toSet)
  def hangingOutputs = outputs.toSet.diff(links.map(_._2).toSet)

  def graph : CompGraph = new CompGraph(nodes.toMap, inputs.toSet, outputs.toSet, links.toSet)
}

object CompGraphBuilder {
  def existCycle(links: Seq[(String, String)]) : Boolean = {
    //create structure like v -> [(v1, false), .., (vk, false)], where exists link (v, vi)
    val vMap = links.groupBy(_._1).mapValues(_.map(_._2))
    val vTraversed = mutable.Set[String]()

    var foundCycles = false
    while((vMap.keySet.diff(vTraversed).nonEmpty) && !foundCycles){
      //select first not traversed vertice:
      val vStart = vMap.find{v => !vTraversed.contains(v._1)}.get._1

      //how to traverse in depth from vStart
      var vList = mutable.MutableList(vStart)
      val vStack = new mutable.Stack[String]()
      while(vList.nonEmpty){
        val vName = vList.head
        vList = vList.tail
        if(vName == "."){
          vStack.pop()
        }else{
          vTraversed += vName
          if(vStack.contains(vName)){
            vList.clear()
            foundCycles = true
          }else{
            vList = (vMap.getOrElse(vName, Seq()) ++ List(".")) ++: vList
            vStack.push(vName)
          }
        }
      }
    }
    foundCycles
  }
}
