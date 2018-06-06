package test_mnist
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

  override def forward(in: Map[String, INDArray]) : (Map[String, INDArray], CompState) = {
    assert(in.keySet == input)
    (Map("loss" -> forward_loss(in("x"), in("y"))), new CompState(in))
  }
  override def backward(dOut: Map[String, INDArray], s: CompState) : Map[String, INDArray] = {
    assert(s.in.keySet == input)
    assert(dOut.keySet == output)
    Map("x" -> backwardX(dOut("loss"), s.in("x"), s.in("y")),
      "y" -> backwardY(dOut("loss"), s.in("x"), s.in("y")))
  }


  override def input : Set[String] = LossL2.input
  override def output : Set[String] = LossL2.output
  override def variables : Set[String] = LossL2.variables
}

