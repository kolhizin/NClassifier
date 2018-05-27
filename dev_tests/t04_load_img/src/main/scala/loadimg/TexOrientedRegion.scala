package loadimg

import play.api.libs.json.{JsValue, Json}

class TexOrientedRegion(var center: TexCoord2, var dir: TexCoord2, var size: TexCoord2) {
  override def toString: String = {
    "TexOrientedRegion(center=%s, dir=%s, size=%s)".format(center.toString, dir.toString, size.toString)
  }

  def apply(coord: TexCoord2) : TexCoord2 = {
    val normal = TexCoord2.orthogonal(dir)
    center + coord.x * size.x * normal + coord.y * size.y * dir
  }
  def apply(coords: Seq[TexCoord2]) : Seq[TexCoord2] = {
    val normal = TexCoord2.orthogonal(dir)
    val ds = size.y * dir
    val ns = size.x * normal
    coords.map(coord => center + coord.x * ns + coord.y * ds)
  }
}

object TexOrientedRegion{
  implicit def TexOrientedRegion(center: TexCoord2, dir: TexCoord2, size: TexCoord2) = new TexOrientedRegion(center, dir, size)

  def fromJSON(data: JsValue): TexOrientedRegion = {
    val w : Double = (data \ "w").as[Double]
    val h : Double = (data \ "h").as[Double]
    TexOrientedRegion(TexCoord2.fromJSON((data \ "center").get), TexCoord2.fromJSON((data \ "dir").get), TexCoord2(w, h))
  }

  def toJSON(data: TexOrientedRegion): JsValue = {
    Json.obj("center" -> TexCoord2.toJSON(data.center, false),
      "dir" -> TexCoord2.toJSON(data.dir, false),
      "w" -> data.size.x, "h" -> data.size.y
    )
  }

  def get4Corners = Array[TexCoord2](TexCoord2(-1.0,-1.0), TexCoord2(1.0,-1.0), TexCoord2(1.0,1.0), TexCoord2(-1.0,1.0))
  def get2Corners = Array[TexCoord2](TexCoord2(-1.0,-1.0), TexCoord2(1.0,1.0))
}
