package app

import breeze.linalg._

object AppTest extends App{
  println("Hello World!")
  val x = DenseVector.ones[Double](5)
  println(x)
}
