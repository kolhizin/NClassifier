package test_nd4s

import org.nd4j.linalg.factory.Nd4j
import org.nd4j.linalg.api.ndarray.INDArray
import org.nd4s.Implicits._

object TestApp extends App {
  def benchmarkND4J(dim: Int, num_runs: Int) : Double = {
    val A = Nd4j.rand(dim, dim)
    val B = Nd4j.rand(dim, dim)
    val start = System.nanoTime()
    for(i <- 1 to num_runs){
      val C = A ** B
    }
    val end = System.nanoTime()
    (end - start).toDouble / 1000000000.0
  }

  def makeSample3x2(num_samples: Int) : (INDArray, INDArray) = {
    val x = Nd4j.randn(num_samples, 3)
    val r = Nd4j.randn(num_samples, 2)
    val t = Nd4j.randn(3, 2)
    val s = Nd4j.randn(1, 2)
    println(t, s)
    val y = x ** t + s.broadcast(num_samples, 2)
    (x, y)
  }

  def makeStep(x:INDArray, y:INDArray, lf: LossL2, fc: LayerFC, learnRate: Double) : Unit = {
    val z = fc.forward_x(x)
    val dz = lf.backward_x(Nd4j.ones(1), z, y)
    val db = fc.backward_b(dz, x)
    val da = fc.backward_a(dz, x)
    fc.update_a(-da * learnRate)
    fc.update_b(-db * learnRate)
  }

  def makeStep(in: Map[String, INDArray], cn: CompNode, learnRate: Double) : Unit = {
    val (z, s) = cn.forward(in)
    val grad = cn.backward(cn.output.map{k => (k, Nd4j.ones(1))}.toMap, s)
    cn.update(grad.filterKeys(cn.variables.contains(_)).mapValues(-_*learnRate))
  }


  /*// test broadcast
  val tmp1 = Nd4j.ones(3,3)
  val tmp2 = (1 to 3).asNDArray(3)
  val tmp3 = tmp1 + tmp2.broadcast(3,3)
  println(tmp3)
  */

  /*// test benchmark
  //warmup
  val warmup = benchmarkND4J(1000, 3)
  println(warmup)

  val dtime = benchmarkND4J(1000, 100)
  println(dtime)
  */


  /*// test backprop
  val lf = new LossL2
  val fc = LayerFC(Nd4j.randn(2, 1), Nd4j.randn(2, 3))
  val (x, y) = makeSample3x2(30)

  val l1 = lf.forward_loss(fc.forward_x(x), y)
  for(i <- 1 to 30) makeStep(Map("x"->x, "y"->y), lf, fc, 0.01)
  val l2 = lf.forward_loss(fc.forward_x(x), y)

  println(l1, l2)
  println(fc.get("a"))
  println(fc.get("b"))
  */

  val lf = new LossL2
  val fc = LayerFC(Nd4j.randn(2, 1), Nd4j.randn(2, 3))
  /*
  val f = new LinkSeq(Array(fc, lf),
      Array(Map("x"->"fc.in", "y"->"fc.out", "a" -> "fc.a", "b" -> "fc.b"),
            Map("x" -> "fc.out", "y" -> "loss.ref", "loss" -> "loss.res")),
      Map("fc.in" -> "x", "loss.ref" -> "y"),
      Map("loss.res" -> "loss"))
  */
  val f = LinkSeq(fc, lf, Map("y" -> "x"), Map("x" -> "x"))

  val (x, y) = makeSample3x2(30)
  val (l1, _) = f.forward(Map("x" -> x, "y" -> y))
  println(l1)

  for(i <- 1 to 20) makeStep(Map("x" -> x, "y" -> y), f, 0.01)

  val (l2, _) = f.forward(Map("x" -> x, "y" -> y))
  println(l2)
  //println(f.get("fc.a"))
  //println(f.get("fc.b"))

  /*
  Graph.addInputs("x", "y")
  Graph.addNodes(Map("fc1" -> fc1, "fc2" -> fc2, "loss" -> fl))
  Graph.addLinks(Map("fc1.out" -> "fc2.in", "fc2.out" -> "loss.in1", "x" -> "fc1.in", "y" -> "loss.in2"))
  Graph.addOutputs(Map("loss.out" -> "loss"))
  Graph.compile()
  (loss, state) = Graph.forward("loss.out", Map("x"->x, "y"->y))
  grad = Graph.backward("loss.out", state)
  Graph.update(-grad*learnRate)

  what is gradient flow?
   */

  val r = List("a" -> "b", "b" -> "c", "b" -> "d", "a" -> "d")

  println(r)
  println(CompGraphBuilder.checkLinkStructure(r))

  println("Done")
}
