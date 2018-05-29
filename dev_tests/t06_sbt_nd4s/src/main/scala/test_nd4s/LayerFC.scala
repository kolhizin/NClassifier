package test_nd4s

import org.nd4j.linalg.factory.Nd4j
import org.nd4j.linalg.api.ndarray.INDArray
import org.nd4s.Implicits._

class LayerFC(input_size: Int, output_size: Int) extends CompNode {
  private var beta = Nd4j.zeros(output_size, input_size)
  private var alpha = Nd4j.zeros(output_size, 1)

  def update_a(dv: INDArray): Unit ={
    assert(dv.rank == alpha.rank)
    assert(dv.rows == alpha.rows)
    assert(dv.columns == alpha.columns)
    alpha += dv
  }
  def update_b(dv: INDArray): Unit ={
    assert(dv.rank == beta.rank)
    assert(dv.rows == beta.rows)
    assert(dv.columns == beta.columns)
    beta += dv
  }
  def init_a(v: INDArray): Unit ={
    assert(v.rank == alpha.rank)
    assert(v.rows == alpha.rows)
    assert(v.columns == alpha.columns)
    alpha = v
  }
  def init_b(v: INDArray): Unit ={
    assert(v.rank == beta.rank)
    assert(v.rows == beta.rows)
    assert(v.columns == beta.columns)
    beta = v
  }

  //X is supposed to be batch, i.e. num_samples x input_size
  def forward_x(X: INDArray) : INDArray = {
    X ** beta.transpose + alpha.transpose.broadcast(X.rows, alpha.length)
  }

  def backward_a(dY: INDArray, X:INDArray) = Nd4j.sum(dY, 0).transpose
  def backward_b(dY: INDArray, X:INDArray) = dY.transpose() ** X
  def backward_x(dY: INDArray, X:INDArray) = dY ** beta

  override def get(name: String) : INDArray = name match {
    case "a" => alpha.dup
    case "b" => beta.dup
  }

  override def update(dv: Map[String, INDArray]) : Unit = {
    assert(dv.keySet.subsetOf(LayerFC.variables))
    if(dv.contains("a")) update_a(dv("a"))
    if(dv.contains("b")) update_b(dv("b"))
  }
  override def init(v: Map[String, INDArray]) : Unit = {
    assert(v.keySet.subsetOf(LayerFC.variables))
    if(v.contains("a")) init_a(v("a"))
    if(v.contains("b")) init_b(v("b"))
  }

  override def forward(in: Map[String, INDArray]) : Map[String, INDArray] = {
    assert(in.keySet == LayerFC.input)
    Map("y" -> forward_x(in("x")))
  }
  override def backward(dOut: Map[String, INDArray], in: Map[String, INDArray]) : Map[String, INDArray] = {
    assert(in.keySet == LayerFC.input)
    assert(dOut.keySet == LayerFC.output)
    Map("x" -> backward_x(dOut("y"), in("x")),
      "a" -> backward_a(dOut("y"), in("x")),
      "b" -> backward_b(dOut("y"), in("x")))
  }


  override def input : Set[String] = LayerFC.input
  override def output : Set[String] = LayerFC.output
  override def variables : Set[String] = LayerFC.variables
}

object LayerFC{
  val input = Set("x")
  val output = Set("y")
  val variables = Set("a", "b")

  def apply(input_size: Int, output_size: Int): LayerFC = new LayerFC(input_size, output_size)
  def apply(a: INDArray, b: INDArray) : LayerFC = {
    assert(a.rank <= b.rank)
    assert(a.length == b.rows)
    val res = new LayerFC(b.columns, b.rows)
    res.init_a(a.reshape(b.rows,1))
    res.init_b(b)
    res
  }
  def apply(v: Map[String, INDArray]) : LayerFC = {
    assert(v.keySet == variables)
    apply(v("a"), v("b"))
  }
}