package loadimg

import play.api.libs.json.{JsValue, Json}

class TexCoord2(var x: Double, var y: Double) {
  override def toString : String = {
    "(%1.3f, %1.3f)".formatLocal(java.util.Locale.US, x, y)
  }

  def +(rhs: TexCoord2) : TexCoord2 = {
    new TexCoord2(x + rhs.x, y + rhs.y)
  }
  def -(rhs: TexCoord2) : TexCoord2 = {
    new TexCoord2(x - rhs.x, y - rhs.y)
  }
  def *(rhs: TexCoord2) : TexCoord2 = {
    new TexCoord2(x * rhs.x, y * rhs.y)
  }
  def /(rhs: TexCoord2) : TexCoord2 = {
    new TexCoord2(x / rhs.x, y / rhs.y)
  }

  def toImageSpace(imgSize: TexCoord2) = new TexCoord2((1.0 + x) * 0.5 * imgSize.x, (1.0 - y) * 0.5 * imgSize.y)
}

object TexCoord2{
  implicit def apply(x: Double, y: Double) : TexCoord2 = new TexCoord2(x, y)
  implicit def apply(xy: (Double, Double)) : TexCoord2 = new TexCoord2(xy._1, xy._2)
  implicit def doubleToTexCoord(v: Double) : TexCoord2 = new TexCoord2(v, v)

  def orthogonal(coord: TexCoord2) : TexCoord2 = new TexCoord2(-coord.y, coord.x)

  def fromJSON(data: JsValue): TexCoord2 = {
    new TexCoord2((data \ "X").as[Double], (data \ "Y").as[Double])
  }
  def toJSON(data: TexCoord2) : JsValue = {
    Json.obj("X" -> data.x, "Y" -> data.y)
  }
  def toJSON(data: TexCoord2, isEmpty: Boolean) : JsValue = {
    Json.obj("X" -> data.x, "Y" -> data.y, "IsEmpty" -> isEmpty)
  }
}
