package test_mnist
import org.nd4j.linalg.factory.Nd4j
import org.nd4j.linalg.api.ndarray.INDArray
import org.nd4j.linalg.ops.transforms.Transforms
import org.nd4s.Implicits._

class LayerStat(fwd: (INDArray, INDArray) => INDArray) extends CompNode {
  override def forward(in: Map[String, INDArray]) : (Map[String, INDArray], CompState) = {
    assert(in.keySet == input)
    (Map("stat" -> fwd(in("x"), in("y"))), new CompState(in))
  }
  //default backward is zero
  override def input : Set[String] = Set("x", "y")
  override def output : Set[String] = Set("stat")
  override def variables : Set[String] = Set()
}

object LayerStat {
  private def getAccuracy(x: INDArray, y: INDArray): INDArray = {
    (x === y).mean(0)
  }
  private def getConfusionMatrix(x: INDArray, y: INDArray): INDArray = {
    val vMin = scala.math.min(x.min(0)(0).toInt, y.min(0)(0).toInt)
    val vMax = scala.math.max(x.max(0)(0).toInt, y.max(0)(0).toInt)
    val res = Nd4j.zeros(vMax-vMin+1, vMax-vMin+1)
    for(i <- vMin to vMax; j <- vMin to vMax){
      res(i, j) = (x.eq(i) * y.eq(j)).sum(0)
    }
    res
  }
  def accuracy = new LayerStat(getAccuracy)
  def confusionMatrix = new LayerStat(getConfusionMatrix)
}