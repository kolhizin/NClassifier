package test_nd4s

import org.nd4j.linalg.factory.Nd4j
import org.nd4s.Implicits._

object TestApp extends App {
  def benchmarkND4J(dim: Int, num_runs: Int) : Double = {
    val A = Nd4j.rand(dim, dim)
    val B = Nd4j.rand(dim, dim)
    val start = System.nanoTime()
    for(i <- 1 to num_runs){
      val C = A * B
    }
    val end = System.nanoTime()
    (end - start).toDouble / 1000000000.0
  }

  /*test broadcast
  val tmp1 = Nd4j.ones(3,3)
  val tmp2 = (1 to 3).asNDArray(3)
  val tmp3 = tmp1 + tmp2.broadcast(3,3)
  println(tmp3)
  */
  //warmup
  val warmup = benchmarkND4J(1000, 3)
  println(warmup)

  val dtime = benchmarkND4J(1000, 1000)
  println(dtime)

  //run
  //benchmarkND4J(1000, 10, 10)

  println("Done")
}
