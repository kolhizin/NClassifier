package test_mnist

import org.nd4j.linalg.factory.Nd4j
import org.nd4j.linalg.api.ndarray.INDArray
import org.nd4s.Implicits._

class LayerFC(input_size: Int, output_size: Int) extends CompNode {
  private var beta = Nd4j.zeros(output_size, input_size)
  private var alpha = Nd4j.zeros(output_size, 1)

  private def updateA(dv: INDArray): Unit ={
    assert(dv.rank == alpha.rank)
    assert(dv.rows == alpha.rows)
    assert(dv.columns == alpha.columns)
    alpha += dv
  }
  private def updateB(dv: INDArray): Unit ={
    assert(dv.rank == beta.rank)
    assert(dv.rows == beta.rows)
    assert(dv.columns == beta.columns)
    beta += dv
  }
  private def initA(v: INDArray): Unit ={
    assert(v.rank == alpha.rank)
    assert(v.rows == alpha.rows)
    assert(v.columns == alpha.columns)
    alpha = v
  }
  private def initB(v: INDArray): Unit ={
    assert(v.rank == beta.rank)
    assert(v.rows == beta.rows)
    assert(v.columns == beta.columns)
    beta = v
  }

  //X is supposed to be batch, i.e. num_samples x input_size
  private def forwardY(X: INDArray) : INDArray = {
    X ** beta.transpose + alpha.transpose.broadcast(X.rows, alpha.length)
  }

  private def backwardA(dY: INDArray, X:INDArray) = Nd4j.sum(dY, 0).transpose
  private def backwardB(dY: INDArray, X:INDArray) = dY.transpose() ** X
  private def backwardX(dY: INDArray, X:INDArray) = dY ** beta

  override def get(name: String) : INDArray = name match {
    case "a" => alpha.dup
    case "b" => beta.dup
  }

  override def update(dv: Map[String, INDArray]) : Unit = {
    assert(dv.keySet.subsetOf(variables))
    if(dv.contains("a")) updateA(dv("a"))
    if(dv.contains("b")) updateB(dv("b"))
  }
  override def init(v: Map[String, INDArray]) : Unit = {
    assert(v.keySet.subsetOf(variables))
    if(v.contains("a")) initA(v("a"))
    if(v.contains("b")) initB(v("b"))
  }

  override def forward(in: Map[String, INDArray]) : (Map[String, INDArray], CompState) = {
    assert(in.keySet == input)
    (Map("out" -> forwardY(in("x"))), new CompState(in))
  }
  override def backward(dOut: Map[String, INDArray], s: CompState) : Map[String, INDArray] = {
    assert(s.in.keySet == input)
    assert(dOut.keySet == output)
    Map("x" -> backwardX(dOut("out"), s.in("x")),
      "a" -> backwardA(dOut("out"), s.in("x")),
      "b" -> backwardB(dOut("out"), s.in("x")))
  }

  override def input : Set[String] = Set("x")
  override def output : Set[String] = Set("out")
  override def variables : Set[String] = Set("a", "b")
}

object LayerFC{
  def apply(input_size: Int, output_size: Int): LayerFC = new LayerFC(input_size, output_size)
  def apply(a: INDArray, b: INDArray) : LayerFC = {
    assert(a.rank <= b.rank)
    assert(a.length == b.rows)
    val res = new LayerFC(b.columns, b.rows)
    res.initA(a.reshape(b.rows,1))
    res.initB(b)
    res
  }
  def apply(v: Map[String, INDArray]) : LayerFC = {
    assert(v.keySet == Set("a", "b"))
    apply(v("a"), v("b"))
  }
}
