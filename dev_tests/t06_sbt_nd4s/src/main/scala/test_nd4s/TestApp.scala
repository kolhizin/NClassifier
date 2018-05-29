package test_nd4s

import org.nd4j.linalg.factory.Nd4j
import org.nd4j.linalg.api.ndarray.INDArray
import org.nd4s.Implicits._
import test_nd4s.TestApp.{fc, lf, x, y}

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
  def makeStep(in: Map[String, INDArray], lf: LossL2, fc: LayerFC, learnRate: Double) : Unit = {
    val z = fc.forward(Map("x" -> in("x")))
    val dOut = Map("loss" -> Nd4j.ones(1))
    val dz = lf.backward(dOut, Map("x" -> z("y"), "y" -> in("y")))
    val da = fc.backward(Map("y" -> dz("x")), Map("x" -> in("x")))
    fc.update(da.filterKeys{f => (f == "a" || f == "b")}.mapValues(-_*learnRate))
  }


  /*test broadcast
  val tmp1 = Nd4j.ones(3,3)
  val tmp2 = (1 to 3).asNDArray(3)
  val tmp3 = tmp1 + tmp2.broadcast(3,3)
  println(tmp3)
  */

  /*test benchmark
  //warmup
  val warmup = benchmarkND4J(1000, 3)
  println(warmup)

  val dtime = benchmarkND4J(1000, 100)
  println(dtime)
  */


  val lf = new LossL2
  //val fc = new LayerFC(3, 2)
  //fc.update_a(Nd4j.randn(2, 1))
  //fc.update_b(Nd4j.randn(2, 3))

  val fc = LayerFC(Nd4j.randn(2, 1), Nd4j.randn(2, 3))

  val (x, y) = makeSample3x2(30)

  val l1 = lf.forward_loss(fc.forward_x(x), y)
  for(i <- 1 to 30) makeStep(Map("x"->x, "y"->y), lf, fc, 0.01)
  val l2 = lf.forward_loss(fc.forward_x(x), y)
  println(l1, l2)

  println(fc.get("a"))
  println(fc.get("b"))

  println("Done")
}
