package test_mnist

import java.io.FileInputStream

import org.nd4j.linalg.factory.Nd4j
import org.nd4j.linalg.api.ndarray.INDArray
import org.nd4s.Implicits._


object TestApp extends App{
  def oneHot(in: INDArray, numValues: Int) : INDArray = {
    val res = Nd4j.zeros(in.length(), numValues)
    for(i <- 0 until numValues){
      res(->, i) = in.reshape(in.length).eq(i)
    }
    res.reshape((in.shape() :+ numValues):_*)
  }

  def makeStep(in: Map[String, INDArray], cn: CompNode, learnRate: Double) : Unit = {
    val (z, s) = cn.forward(in)
    val grad = cn.backward(cn.output.map{k => (k, Nd4j.ones(1))}.toMap, s)
    cn.update(grad.filterKeys(cn.variables.contains).mapValues(-_*learnRate))
  }
  def makeStep(in: Map[String, INDArray], dOut: Map[String, INDArray], cn: CompNode, learnRate: Double) : Map[String, INDArray] = {
    val (z, s) = cn.forward(in)
    val grad = cn.backward(dOut, s)
    cn.update(grad.filterKeys(cn.variables.contains).mapValues(-_*learnRate))
    z
  }

  def test3x2(numSamples: Int, numSteps: Int, learnRate: Double) : Boolean = {
    val x = Nd4j.randn(numSamples, 3)
    val t = Nd4j.randn(3, 2)
    val s = Nd4j.randn(1, 2)
    val y = x ** t + s.broadcast(numSamples, 2)
    val input = Map("x" -> x, "y" -> y)

    val lf = LayerLoss.l2loss
    val fc = LayerFC(Nd4j.randn(2, 1), Nd4j.randn(2, 3))
    val cg = CompGraph.link(fc, lf, Seq("out" -> "x"))

    val (l1, _) = cg.forward(input)
    for(i <- 0 to numSteps)makeStep(input, cg, learnRate)
    val (l2, _) = cg.forward(input)

    println(l1("loss"), l2("loss"))
    l2("loss").getDouble(0) / l1("loss").getDouble(0) < 1e-3 //improved more than 1000 times
  }


//  test3x2(50, 20, 0.01)

  val srcX = MNIST.readImages("../../dev_datasets/MNIST/train-images.idx3-ubyte")
  val srcY = MNIST.readLabels("../../dev_datasets/MNIST/train-labels.idx1-ubyte")

  val devX = srcX(0 -> 1000, ->)
  val devY = oneHot(srcY(0 -> 1000, ->), 10).reshape(1000, 10)

  val gBuilder = new CompGraphBuilder
  gBuilder.addInputs(Set("x", "y"))
  gBuilder.addOutputs(Set("cross_entropy", "p"))

  val fc1 = LayerFC(Nd4j.randn(10, 1), Nd4j.randn(10, 28*28))

  gBuilder.addNodes(Map("fc1" -> fc1, "softmax" -> LayerMap.softmax,
    "loss_ce" -> LayerLoss.softmaxCrossEntropyWithLogits))

  gBuilder.addLinks(Seq("x" -> "fc1.x", "fc1.out" -> "softmax.x", "fc1.out" -> "loss_ce.x",
                    "y" -> "loss_ce.y", "softmax.out" -> "p", "loss_ce.loss"->"cross_entropy"))

  val graph = gBuilder.graph

  val input = Map("x" -> devX, "y" -> devY)
  val dOutput = Map("p"->Nd4j.zerosLike(devY), "cross_entropy" -> Nd4j.ones(1))

  val l1 = graph.forward(input)._1("cross_entropy")
  for(i <- 1 to 1000){
    val r = makeStep(input, dOutput, graph, 0.2)
    if(i % 10 == 0)println(i, r("cross_entropy")(0,0).toDouble)
  }
  val res = graph.forward(input)._1
  val l2 = res("cross_entropy")
  val prob = res("p")

  println(l1, l2)


  println(prob(0->20, ->))
  println(srcY(0->20, ->))

  println("Done")
}
