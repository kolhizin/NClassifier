package loadimg

import play.api.libs.json._
import javax.swing._
import loadimg.LoadImg.{img, region}
import org.bytedeco.javacpp.opencv_core
import org.bytedeco.javacpp.opencv_core._
import org.bytedeco.javacpp.opencv_imgcodecs._
import org.bytedeco.javacpp.opencv_imgproc._
import org.bytedeco.javacv._



object LoadImg extends App{
  def display(mat: Mat, str: String): Unit = {
    val cnv = new CanvasFrame(str, 1)

    cnv.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE)

    val converter = new OpenCVFrameConverter.ToMat()
    cnv.showImage(converter.convert(mat))
  }

  def loadJSON(fname: String): JsValue = {
    val src = io.Source.fromFile(fname)
    val res = Json.parse(src.mkString)
    res
  }
  def parseSample(sample: JsValue):Map[String, Array[TexOrientedRegion]] = {
    sample.as[Map[String, JsValue]].filter{case (k, v) => (v \ "isValidObs").asOpt[Boolean].getOrElse(true)}
      .mapValues(x => x("regions").as[Array[JsValue]]
                  .map(y => TexOrientedRegion.fromJSON(y)))
  }
  def loadIMG(fname: String, w: Int = -1, h: Int = -1) : opencv_core.Mat = {
    val img0 = imread(fname)
    val fw = if(w == -1) img0.size.width else w
    val fh = if(h == -1) img0.size.height else h
    val ft = img0.`type`
    val img = opencv_core.Mat.zeros(fh, fw, ft).asMat
    org.bytedeco.javacpp.opencv_imgproc.resize(img0, img, new opencv_core.Size(fw, fh))
    img
  }
  def extractRegion(img: opencv_core.Mat, region: TexOrientedRegion) : opencv_core.Mat = {
    val imgSize = TexCoord2(img.size().width(), img.size().height())
    val rr = region(TexOrientedRegion.get4Corners).map(_.toImageSpace(imgSize))
    val minx = rr.map(_.x).reduceLeft(math.min).toInt
    val maxx = rr.map(_.x).reduceLeft(math.max).toInt
    val miny = rr.map(_.y).reduceLeft(math.min).toInt
    val maxy = rr.map(_.y).reduceLeft(math.max).toInt
    val fw = maxx - minx
    val fh = maxy - miny
    val res = opencv_core.Mat.zeros(fh, fw, img.`type`).asMat
    img.colRange(minx, maxx).rowRange(miny, maxy).copyTo(res)
    res
  }
  def makeKernel() : opencv_core.Mat = {
    opencv_core.multiply(opencv_core.Mat.ones(16, 16, CV_32F).asMat, 1.0 / 256.0).asMat
  }

  def makeKernel(img: opencv_core.Mat) : opencv_core.Mat = {
    //opencv_core.cop
    println(img.rows, img.cols)
    val res = opencv_core.Mat.ones(img.rows, img.cols, img.`type`).asMat
    img.copyTo(res)
    res.convertTo(res, CV_32FC3)
    val m = opencv_core.mean(res)
    val rf = opencv_core.multiply(res, 3.0 / (img.rows * img.cols * (m.red() + m.green() + m.blue()))).asMat
    rf
  }

  val path = "D:/Programming/NClassifier/dev_datasets/faceimages/"
  val src = loadJSON(path + "eye.json")
  val res = parseSample(src)

  val fname = res.head._1
  val region = res.head._2(0)

  val img = loadIMG(path + fname, 256, 512)

  display(img, "Input")

  //println(TexOrientedRegion.fromJSON(r0))
  println("Hello, world!")
}
