package test_linreg

import breeze.linalg._

//Start simple: only vectors for now
class LayerFC(input_size: Int, output_size: Int){
  private var beta = DenseMatrix.zeros[Double](output_size, input_size)
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

//this is bad design -- restricts you on sample-update
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


  def makeOneStep(fc: LayerFC, lf: LossFunc, data: DenseMatrix[Double], learnRate: Double = 0.01):Double = {
    val z = fc.forward(data)
    val loss = lf.forward(z)
    val dz = lf.backward_dx(z)
    val (db, da) = fc.backward_dw(dz, data)

    val (b, a) = fc.get_weights()
    fc.update_weights((b - learnRate * db, a - learnRate * da))
    loss
  }

  def makeSample1x1(num_samples: Int) : (DenseMatrix[Double], DenseVector[Double]) = {
    val x = DenseMatrix.rand[Double](num_samples, 1, breeze.stats.distributions.Gaussian(0, 1))
    val y = (0.5 - 0.8 * x).toDenseVector
    (x, y)
  }
  def makeFC1x1 = {
    val res = new LayerFC(1, 1)
    res.update_weights((DenseMatrix.rand[Double](1,1, breeze.stats.distributions.Gaussian(0, 1)), DenseVector.rand[Double](1, breeze.stats.distributions.Gaussian(0, 1))))
    res
  }

  def makeSample3x1(num_samples: Int) : (DenseMatrix[Double], DenseVector[Double]) = {
    val x = DenseMatrix.rand[Double](num_samples, 3, breeze.stats.distributions.Gaussian(0, 1))
    val y = (15.5 - 0.8 * x(::, 0) + 0.3 * x(::, 1) + 2.7 * x(::, 2)).toDenseVector
    (x, y)
  }
  def makeFC3x1 = {
    val res = new LayerFC(3, 1)
    res.update_weights((DenseMatrix.rand[Double](1,3, breeze.stats.distributions.Gaussian(0, 1)), DenseVector.rand[Double](1, breeze.stats.distributions.Gaussian(0, 1))))
    res
  }

  def runTest(x:DenseMatrix[Double], y:DenseVector[Double], layerFC: LayerFC, num_steps: Int) : Unit = {
    val lossFunc = new LossFunc(y)
    val loss0 = lossFunc.forward(layerFC.forward(x))
    for (step <- 1 to num_steps) makeOneStep(layerFC, lossFunc, x)
    val loss1 = lossFunc.forward(layerFC.forward(x))
    println("Loss %2.4f -> %2.4f, Result is %s".formatLocal(java.util.Locale.US, loss0, loss1, layerFC.get_weights.toString))
  }

  //1x1 test
  println("\n1x1 test:")
  val (t1x1x, t1x1y) = makeSample1x1(50)
  val t1x1fc = makeFC1x1
  runTest(t1x1x, t1x1y, t1x1fc, 100)

  //3x1 test
  println("\n3x1 test:")
  val (t3x1x, t3x1y) = makeSample3x1(50)
  val t3x1fc = makeFC3x1
  runTest(t3x1x, t3x1y, t3x1fc, 100)


  println("Done")
}



/*

*/