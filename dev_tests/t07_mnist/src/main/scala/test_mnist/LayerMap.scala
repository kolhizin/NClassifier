package test_mnist

import org.nd4j.linalg.api.ndarray.INDArray
import org.nd4j.linalg.ops.transforms.Transforms
import org.nd4s.Implicits._

class LayerMap(fwd: INDArray => INDArray, bwd: INDArray => INDArray) extends CompNode {
  override def forward(in: Map[String, INDArray]) : (Map[String, INDArray], CompState) = {
    assert(in.keySet == input)
    (Map("out" -> fwd(in("x"))), new CompState(in))
  }
  override def backward(dOut: Map[String, INDArray], s: CompState) : Map[String, INDArray] = {
    assert(s.in.keySet == input)
    assert(dOut.keySet == output)
    Map("x" -> dOut("out") * bwd(s.in("x")))
  }

  override def input : Set[String] = Set("x")
  override def output : Set[String] = Set("out")
  override def variables : Set[String] = Set()
}

object LayerMap {
  private def fwdRelu(x: INDArray) = x.mul(x.gte(0))
  private def bwdRelu(x: INDArray)= x.gte(0)

  /*
  yi = exp(xi)/sum(exp(x))
  dyi = (exp(xi) * sum(exp(x)) - exp(xi) * exp(xi)) / sum(exp(x))2
  dyi = exp(xi) / sum(exp(x)) * (sum(exp(x)) - exp(xi)) / sum(exp(x)) = pi * (1 - pi)
   */
  private def fwdSoftmax(x: INDArray) = Transforms.softmax(x)
  private def bwdSoftmax(x: INDArray) : INDArray = {
    val p = Transforms.softmax(x)
    p * (- p + 1)
  }

  /*
  y = exp(x) / (1 + exp(x))
  dy = exp(x) * (1 + exp(x)) - exp(x) * exp(x)  / (1+exp(x))^2
  dy = p(x) * (1 - p(x))
  */
  private def fwdLogit(x: INDArray) = Transforms.exp(x) / (Transforms.exp(x) + 1)
  private def bwdLogit(x: INDArray) : INDArray = {
    val p = fwdLogit(x)
    p * (-p + 1)
  }

  def relu = new LayerMap(fwdRelu, bwdRelu)
  def softmax = new LayerMap(fwdSoftmax, bwdSoftmax)
  def logit = new LayerMap(fwdLogit, bwdLogit)
}