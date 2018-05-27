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

  def forward(X: DenseMatrix[Double]) : DenseMatrix[Double] = alpha + beta * X
  def forward(X: DenseVector[Double]) : DenseVector[Double] = alpha + beta * X

  def backward(dY: DenseMatrix[Double], X: DenseMatrix[Double])
    : (DenseMatrix[Double], (DenseMatrix[Double], DenseVector[Double])) = {
    /*
    Y = alpha + beta * X
    dL / dY - we have (first argument)
    dL / dX - we have to find

    dL / dXij = sum(a, b) { dL/dYab * dYab / dXij }
    dYab / dXij = (alpha_a + beta_a * X^b)
     */
  }
}

object LinReg extends App{
  val x = DenseVector.ones[Double](3)
  println(x)
  println("Done")
}
