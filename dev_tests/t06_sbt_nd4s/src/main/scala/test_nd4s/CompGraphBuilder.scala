package test_nd4s

import scala.collection.{GenSet, mutable}

class CompGraphBuilder {
  private val nodes = scala.collection.mutable.Map[String, CompNode]()

  private val nodeInputs = scala.collection.mutable.Map[String, (String, String)]()
  private val nodeOutputs = scala.collection.mutable.Map[String, (String, String)]()
  private val nodeVariables = scala.collection.mutable.Map[String, (String, String)]()

  private val inputs = scala.collection.mutable.Set[String]()
  private val outputs = scala.collection.mutable.Set[String]()

  private val links = scala.collection.mutable.IndexedSeq[(String, String)]()

  private def extNames = nodes.keySet ++ inputs ++ outputs
  private def intNames = nodeInputs.keySet ++ nodeOutputs.keySet ++ nodeVariables.keySet
  private def allNames = extNames ++ intNames
  private def link0Names = inputs ++ nodeOutputs.keySet
  private def link1Names = outputs ++ nodeInputs.keySet

  def addNodes(nds: Map[String, CompNode]) : Unit = {
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
    assert(allNames.intersect(in).isEmpty, "addInputs: name already exists!")
    inputs ++= in
  }
  def addOutputs(out: Set[String]) : Unit = {
    assert(allNames.intersect(out).isEmpty, "addOutputs: name already exists!")
    outputs ++= out
  }
  def addLinks(lnks: Seq[(String, String)]) : Unit = {
    assert(links.intersect(lnks).isEmpty, "addLinks: link already exists!")
    assert(links.map(_._1).toSet.diff(link0Names).isEmpty, "addLinks: incorrect link source!")
    assert(links.map(_._2).toSet.diff(link1Names).isEmpty, "addLinks: incorrect link source!")
  }

}

object CompGraphBuilder {
  def checkLinkStructure(links: Seq[(String, String)]) : Boolean = {
    //create structure like v -> [(v1, false), .., (vk, false)], where exists link (v, vi)
    val vMap = links.groupBy(_._1).mapValues(_.map(_._2))
    val vTraversed = mutable.Set[String]()

    var foundCycles = false
    while((vTraversed.size != vMap.size) && !foundCycles){
      //select first not traversed vertice:
      val vStart = vMap.find{v => !vTraversed.contains(v._1)}.get._1

      //how to traverse in depth from vStart
      
    }
    foundCycles
  }
}
