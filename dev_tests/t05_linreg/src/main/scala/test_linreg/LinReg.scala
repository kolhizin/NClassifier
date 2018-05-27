package test_linreg

import breeze.linalg._

//Start simple: only vectors for now
class LayerFC(input_size: Int, output_size: Int){
  private var beta = DenseMatrix.zeros[Double](input_size, output_size)
  private var alpha = DenseVector.zeros[Double](output_size)

  def update_weights(weights : (DenseMatrix[Double], DenseVector[Double])) : Unit = {
    assert(beta.rows == weights._1.rows)
    assert(beta.cols == weights._1.cols)
    assert(alpha.length == weights._2.length)
    beta = weights._1
    alpha = weights._2
  }

  def get_weights() : (DenseMatrix[Double], DenseVector[Double]) = (beta, alpha)


  def forward(X: DenseMatrix[Double]) : DenseMatrix[Double] = {
    val tmp = X * beta.t
    tmp(*, ::) + alpha
  }

  def backward_dw(dY: DenseMatrix[Double], X: DenseMatrix[Double]) : (DenseMatrix[Double], DenseVector[Double]) = {
    (dY.t * X, sum(dY, Axis._0).t)
  }
  def backward_dx(dY: DenseMatrix[Double], X: DenseMatrix[Double]) : DenseMatrix[Double] = {
    dY * beta
  }
}

class LossFunc(y: DenseVector[Double]){
  def forward(X: DenseMatrix[Double]) : Double = {
    val diff = (X.toDenseVector - y)
    diff dot diff
  }
  def backward_dx(X: DenseMatrix[Double]) : DenseMatrix[Double] = {
    2.0 * (X - y.toDenseMatrix.t)
  }
}

object LinReg extends App{
  def makeSample1x1(num_samples: Int) : (DenseMatrix[Double], DenseVector[Double]) = {
    val x = DenseMatrix.rand[Double](num_samples, 1, breeze.stats.distributions.Gaussian(0, 1))
    val y = (0.5 - 0.8 * x).toDenseVector
    (x, y)
  }

  def makeOneStep(fc: LayerFC, lf: LossFunc, data: DenseMatrix[Double], learnRate: Double = 0.01):Double = {
    val z = fc.forward(data)
    val loss = lossf.forward(z)
    val dz = lossf.backward_dx(z)
    val (db, da) = clc.backward_dw(dz, data)
    val (b, a) = clc.get_weights()
    clc.update_weights((b - learnRate * db, a - learnRate * da))
    loss
  }

  val (x, y) = makeSample1x1(10)
  val clc = new LayerFC(1, 1)
  val lossf = new LossFunc(y)
  clc.update_weights((DenseMatrix.rand[Double](1,1, breeze.stats.distributions.Gaussian(0, 1)), DenseVector.rand[Double](1, breeze.stats.distributions.Gaussian(0, 1))))

  println(clc.get_weights)

  val loss_start = lossf.forward(clc.forward(x))
  for (step <- 1 to 50){
    println(makeOneStep(clc, lossf, x))
  }
  println(clc.get_weights)
  /*
  println(x)
  println("forward:")
  val z = clc.forward(x)
  println(z)
  println("loss:")
  val loss =  lossf.forward(z)
  println(loss)
  println("loss gradient:")
  val dz = lossf.backward_dx(z)
  println(dz)
  println("backward:")
  println(dz.rows, dz.cols, x.rows, x.cols)
  val dw = clc.backward_dw(dz, x)
  println(dw)
  */
  println("Done")
}



/*

*/