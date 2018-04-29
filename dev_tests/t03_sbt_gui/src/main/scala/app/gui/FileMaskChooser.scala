package app.gui

import scalafx.Includes._
import scalafx.event.ActionEvent
import scalafx.scene.control.{Button, Label, TextField}
import scalafx.scene.layout.{HBox, VBox}
import scalafx.stage.{DirectoryChooser, Window}

class FileMaskChooser{
  private lazy val dscFileMask = new Label{
    text = "File mask:"
    minWidth = 150
    maxWidth = 150
  }
  private lazy val txtFileMask = new TextField{
    text = "\\w*.jpg"
    minWidth = 150
    maxWidth = 150
  }
  private lazy val fullLayout = new VBox{
    children = Seq(dscFileMask, txtFileMask)
    spacing = 10
  }

  def getFileMask = txtFileMask.text.value
  def setFileMask(fm: String) {txtFileMask.text = fm}

  def layout():VBox = {fullLayout}

}
