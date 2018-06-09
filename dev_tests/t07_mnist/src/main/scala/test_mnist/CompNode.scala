package test_mnist

import org.nd4j.linalg.factory.Nd4j
import org.nd4j.linalg.api.ndarray.INDArray

class CompState(var in: Map[String, INDArray])
class CompStateComposition(in: Map[String, INDArray], var subStates: Map[String, CompState]) extends CompState(in)

trait CompNode {
  def forward(in: Map[String, INDArray]) = (in.mapValues(Nd4j.zerosLike), new CompState(in))
  def backward(dOut: Map[String, INDArray], s: CompState) : Map[String, INDArray] = s.in.mapValues(Nd4j.zerosLike)

  def get(name: String) : INDArray = throw new UnsupportedOperationException
  def update(dv: Map[String, INDArray]) : Unit = {}
  def init(v: Map[String, INDArray]) : Unit = {}

  def input : Set[String] = Set()
  def output : Set[String] = Set()
  def variables : Set[String] = Set()
}