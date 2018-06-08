package test_mnist
import org.nd4j.linalg.factory.Nd4j
import org.nd4j.linalg.api.ndarray.INDArray
import org.nd4j.linalg.ops.transforms.Transforms
import org.nd4s.Implicits._

class LayerLoss(fwd: (INDArray, INDArray) => INDArray,
                bwd: (INDArray, INDArray) => (INDArray, INDArray))
  extends CompNode {
  override def forward(in: Map[String, INDArray]) : (Map[String, INDArray], CompState) = {
    assert(in.keySet == input)
    (Map("loss" -> fwd(in("x"), in("y"))), new CompState(in))
  }
  override def backward(dOut: Map[String, INDArray], s: CompState) : Map[String, INDArray] = {
    assert(s.in.keySet == input)
    assert(dOut.keySet == output)
    val (x, y) = bwd(s.in("x"), s.in("y"))
    Map("x" -> x*dOut("loss"), "y" -> y*dOut("loss"))
  }

  override def input : Set[String] = Set("x", "y")
  override def output : Set[String] = Set("loss")
  override def variables : Set[String] = Set()
}

object LayerLoss {
  private def fwdL2(x: INDArray, y: INDArray): INDArray = {
    val diff = x - y
    (diff * diff).sum(1).mean(0) //here * is elementwise multiplication
  }
  private def bwdL2(dif: INDArray) : INDArray = dif * 2.0 / dif.shape()(0)

  /*
  what it is:
  p = softmax(x) = exp(x) / sum(exp(x))
  loss = -mean(sum(y * log(p))
  log(p) = x - log(sum(exp(x)))
  sum(y * log(p)) = sum(xy) - log(sum(exp(x))) //easy
  dx = y - softmax(x)
  dy = x
  */
  private def fwdSoftmaxCrossEntropyWithLogits(x: INDArray, y: INDArray): INDArray = {
    -((x*y).sum(1) - Transforms.log(Transforms.exp(x).sum(1))).mean(0)
  }
  private def bwdSoftmaxCrossEntropyWithLogits(x:INDArray, y:INDArray) : (INDArray, INDArray) = {
    ((-y + Transforms.softmax(x)) / x.shape()(0), - x / x.shape()(0))
  }

  def l2loss = new LayerLoss(fwdL2, (x, y) => (bwdL2(x - y), bwdL2(y - x)))
  def softmaxCrossEntropyWithLogits = new LayerLoss(fwdSoftmaxCrossEntropyWithLogits, bwdSoftmaxCrossEntropyWithLogits)
}