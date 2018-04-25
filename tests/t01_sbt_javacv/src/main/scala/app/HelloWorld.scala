package app

import javax.swing._

import org.bytedeco.javacpp.opencv_core._
import org.bytedeco.javacpp.opencv_imgcodecs._
import org.bytedeco.javacpp.opencv_imgproc._
import org.bytedeco.javacv._

object HelloWorld extends App {
  val src = imread("D:/Jupyter/DataSets/prv/u0001867_000025137.jpg")
  display(src, "Input")
  println("Hello World!")

  def display(mat: Mat, str: String): Unit = {
    val cnv = new CanvasFrame(str, 1)

    cnv.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE)

    val converter = new OpenCVFrameConverter.ToMat()
    cnv.showImage(converter.convert(mat))
  }
}
