package app.gui

import scalafx.Includes._
import scalafx.event.ActionEvent
import scalafx.scene.control.{Button, Label, TextField}
import scalafx.scene.layout.{HBox, VBox}
import scalafx.stage.{DirectoryChooser, Window}

class DirChooser(ownerWindow: Window) {
  private var dirNameStr : Option[String] = None
  private lazy val dscDirName = new Label{
    text = "Working directory:"
  }
  private lazy val lblDirName = new Label{
    text = "<select dir>"
    minWidth = 300
    maxWidth = 300
  }
  private lazy val btnChooseDir = new Button {
    text = "Choose Dir"
    onAction = (e: ActionEvent) => {
      val dc = new DirectoryChooser()
      val res = dc.showDialog(ownerWindow)
      if(res != null){
        setDirName(res.toString())
      }
    }
  }
  private lazy val fullLayout = new VBox{
    children = Seq(dscDirName, lblDirName, btnChooseDir)
    spacing = 10
  }

  def layout():VBox = {fullLayout}
  def getDirName():Option[String] = {
    dirNameStr
  }
  def setDirName(str: String){
    dirNameStr = Some(str)
    lblDirName.text = dirNameStr.getOrElse("<select dir>")
  }

}
