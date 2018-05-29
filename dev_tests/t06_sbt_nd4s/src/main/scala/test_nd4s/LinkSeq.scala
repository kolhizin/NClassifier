package test_nd4s

class LinkSeq(a: CompNode, b: CompNode,
              outInMapping: Map[String, String],
              aVarMap: Map[String, String],
              bVarMap: Map[String, String]) extends CompNode {
  assert(a.output == outInMapping.keySet)
  assert(b.input == outInMapping.values.toSet)
  assert(a.variables == aVarMap.keySet)
  assert(b.variables == bVarMap.keySet)
  assert(aVarMap.values.toSet.intersect(bVarMap.values.toSet).isEmpty)
  assert(aVarMap.values.toSet.count{x => true} == aVarMap.count{x => true})
  assert(bVarMap.values.toSet.count{x => true} == bVarMap.count{x => true})

  def forward(in: Map[String, INDArray]) : Map[String, INDArray] = in
  def backward(dOut: Map[String, INDArray], in: Map[String, INDArray]) : Map[String, INDArray] = dOut

  def get(name: String) : INDArray = throw new UnsupportedOperationException
  def update(dv: Map[String, INDArray]) : Unit = {}
  def init(v: Map[String, INDArray]) : Unit = {}

  override def input = a.input
  override def output = b.output
  override def variables: Set[String] = aVarMap.values.toSet.union(bVarMap.values.toSet)
}
