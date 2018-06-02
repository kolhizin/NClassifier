package test_nd4s


import org.nd4j.linalg.factory.Nd4j
import org.nd4j.linalg.api.ndarray.INDArray

class CompState(var in: Map[String, INDArray])

trait CompNode {
  def forward(in: Map[String, INDArray]) : (Map[String, INDArray], CompState) = (in, new CompState(in))
  def backward(dOut: Map[String, INDArray], s: CompState) : Map[String, INDArray] = dOut

  def get(name: String) : INDArray = throw new UnsupportedOperationException
  def update(dv: Map[String, INDArray]) : Unit = {}
  def init(v: Map[String, INDArray]) : Unit = {}

  def input : Set[String] = Set()
  def output : Set[String] = Set()
  def variables : Set[String] = Set()
}