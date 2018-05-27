package test_nd4s

import org.nd4j.linalg.factory.Nd4j
import org.nd4s.Implicits._

object TestApp extends App {
  println(Nd4j.ones(3,3,3))

  println("Done")
}
