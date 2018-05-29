package test_nd4s

import org.nd4j.linalg.factory.Nd4j
import org.nd4j.linalg.api.ndarray.INDArray
import org.nd4s.Implicits._

class LossL2 extends CompNode {
  def forward_loss(X: INDArray, Y: INDArray) : INDArray = {
    val diff = X - Y
    (diff * diff).sum(1).mean(0) //elementwise multiplication
  }
  def backward_x(dLoss: INDArray, X: INDArray, Y: INDArray) = {
    (X - Y) * 2.0// * dLoss
  }
  def backward_y(dLoss: INDArray, X: INDArray, Y: INDArray) = {
    (Y - X) * 2.0// * dLoss
  }

  override def forward(in: Map[String, INDArray]) : Map[String, INDArray] = {
    assert(in.keySet == LossL2.input)
    Map("loss" -> forward_loss(in("x"), in("y")))
  }
  override def backward(dOut: Map[String, INDArray], in: Map[String, INDArray]) : Map[String, INDArray] = {
    assert(in.keySet == LossL2.input)
    assert(dOut.keySet == LossL2.output)
    Map("x" -> backward_x(dOut("loss"), in("x"), in("y")),
      "y" -> backward_y(dOut("loss"), in("x"), in("y")))
  }


  override def input : Set[String] = LossL2.input
  override def output : Set[String] = LossL2.output
  override def variables : Set[String] = LossL2.variables
}

object LossL2 {
  def variables : Set[String] = Set()
  def input = Set("x", "y")
  def output = Set("loss")
}
