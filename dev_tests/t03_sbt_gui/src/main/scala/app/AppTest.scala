package app

import java.io.{ByteArrayInputStream, File, FileInputStream}

import org.bytedeco.javacpp.opencv_core._
import org.bytedeco.javacpp.opencv_imgcodecs._
import org.bytedeco.javacpp.opencv_imgproc._
import org.bytedeco.javacv._
import scalafx.Includes._
import scalafx.application.JFXApp
import scalafx.scene.Scene
import scalafx.scene.control.{Button, Label, TextField}
import scalafx.scene.layout.{HBox, VBox}
import app.gui._
import scalafx.event.ActionEvent
import scalafx.scene.image.{Image, ImageView}
import scalafx.scene.input.MouseEvent

object AppTest extends JFXApp {
  stage = new JFXApp.PrimaryStage {
    title = "Image viewer app"
    width = 640
    height = 480

    lazy val ctlDirChooser = new DirChooser(stage)
    lazy val ctlFileMaskChooser = new FileMaskChooser
    lazy val ctlFileSelector = new FileSelector(ctlDirChooser.getDirName.get, ctlFileMaskChooser.getFileMask)

    //val img = imread("D:/Programming/NClassifier/dev_datasets/faceimages/20171222_163631.jpg")
    val f = new FileInputStream("D:/Programming/NClassifier/dev_datasets/faceimages/20171222_163631.jpg")
    val img = new Image(f)

    scene = new Scene {
      content = new VBox{
          children = Seq(
            new HBox {
              children = Seq(ctlDirChooser.layout, ctlFileMaskChooser.layout, ctlFileSelector.layout)
            },
            new ImageView(img){
              fitHeight = 400
              fitWidth = 400
              preserveRatio = true
              var mousePressed = false
              var x1, x2, y1, y2 = 0.0
              onMousePressed = (e: MouseEvent) => {
                x1 = e.x
                y1 = e.y
                mousePressed = true;
                println("mouse pressed")
              }
              onMouseReleased = (e: MouseEvent) => {
                mousePressed = false;
                x2 = e.x
                y2 = e.y
                println("mouse released")
                println(x1,y1,x2,y2)
              }
            }
          )
      }
    }
  }
}

/*
App design:
1) choose directory
2) [optional] specify mask (like *.jpg)
3) navigate through files (after end goto #1 or start from beginning)
*/
/*
object AppTest extends App {
  //val x = new Button
  //val x = new File("D:/Programming/NClassifier/dev_datasets/faceimages/")
  //val y = x.listFiles()
  //println(y.deep)
  //val src = imread("D:/Programming/NClassifier/dev_datasets/faceimages/20171222_163619.jpg")

  //display(src, "Input")
  //println(src.size.width, src.size.height)

  def display(mat: Mat, str: String): Unit = {
    val cnv = new CanvasFrame(str, 1)

    cnv.setDefaultCloseOperation(WindowConstants.EXIT_ON_CLOSE)

    val converter = new OpenCVFrameConverter.ToMat()
    cnv.showImage(converter.convert(mat))
  }
}
*/