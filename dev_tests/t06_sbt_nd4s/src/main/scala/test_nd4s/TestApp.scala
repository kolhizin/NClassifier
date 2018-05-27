package test_nd4s

import org.nd4j.linalg.factory.Nd4j
import org.nd4s.Implicits._

object TestApp extends App {
  val tmp1 = Nd4j.ones(3,3)
  val tmp2 = (1 to 3).asNDArray(3)
  val tmp3 = tmp1 + tmp2.broadcast(3,3)
  println(tmp3)
  println("Done")
}
