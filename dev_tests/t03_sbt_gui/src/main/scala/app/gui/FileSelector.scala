package app.gui

import scalafx.Includes._
import scalafx.event.ActionEvent
import scalafx.scene.control.{Button, Label, TextField}
import scalafx.scene.layout.{HBox, VBox}
import scalafx.stage.{DirectoryChooser, Window}

import scala.util.matching.Regex

class FileSelector(dir: => String, mask: => String) {
  private var files: List[java.io.File] = List()
  private lazy val lblDesc = new Label{
    text = "no files"
    minWidth = 300
    maxWidth = 300
  }
  private lazy val btnUpdate = new Button{
    text = "Update"
    onAction = (e: ActionEvent) => {
      updateFiles()
    }
  }

  private lazy val fullLayout = new VBox{
    minWidth = 150
    spacing = 10
    children = Seq(btnUpdate, lblDesc)
  }
  private def updateFiles() : Unit = {
    val fd = new java.io.File(dir)
    val re = mask
    val res = fd.listFiles().filter(_.getName.matches(re))
    files = res.toList
    val stat = "Files: " + res.length.toString + "\n"
    val example = res.take(3).map(_.getName).mkString("\n")
    val end = if(res.length > 3){"\n..."}else{""}
    val txt = stat + example + end
    lblDesc.text = txt
  }
  def layout():VBox = {fullLayout}
}
