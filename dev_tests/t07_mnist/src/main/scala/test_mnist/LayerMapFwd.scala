package test_mnist

import org.nd4j.linalg.api.ndarray.INDArray
import org.nd4j.linalg.ops.transforms.Transforms
import org.nd4s.Implicits._

class LayerMapFwd(fwd: INDArray => INDArray) extends CompNode {
  override def forward(in: Map[String, INDArray]) : (Map[String, INDArray], CompState) = {
    assert(in.keySet == input)
    (Map("out" -> fwd(in("x"))), new CompState(in))
  }

  override def input : Set[String] = Set("x")
  override def output : Set[String] = Set("out")
  override def variables : Set[String] = Set()
}

object LayerMapFwd {
  private def getArgmax(x: INDArray) = x.argMax(x.shape().length - 1)

  def argmax = new LayerMapFwd(getArgmax)
}