package test_mnist

import org.nd4j.linalg.api.ndarray.INDArray
import org.nd4j.linalg.factory.Nd4j

object MNIST {
  private def byteAsInt(b: Byte):Int = if (b < 0) 256 + b else b
  private def bytesAsInt(bs: Array[Byte]):Int = {
    byteAsInt(bs(3))+(byteAsInt(bs(2))<<8)+(byteAsInt(bs(1))<<16)+(byteAsInt(bs(0))<<24)
  }
  private def readInt(file : java.io.FileInputStream): Int = {
    val tmpArray = new Array[Byte](4)
    file.read(tmpArray)
    bytesAsInt(tmpArray)
  }
  private def readBuffer(file : java.io.FileInputStream, numBytes: Int) : Array[Byte] = {
    val res = new Array[Byte](numBytes)
    file.read(res)
    res
  }

  def readImages(fname: String) : INDArray = {
    val f = new java.io.FileInputStream(fname)
    val magicNumber = readInt(f)
    val sampleSize = readInt(f)
    val rowNumber = readInt(f)
    val colNumber = readInt(f)
    val obsSize = rowNumber * colNumber
    Nd4j.create(readBuffer(f, sampleSize * obsSize).map(byteAsInt(_).toDouble / 255.0)).reshape(sampleSize, obsSize)
  }

  def readLabels(fname: String) : INDArray = {
    val f = new java.io.FileInputStream(fname)
    val magicNumber = readInt(f)
    val sampleSize = readInt(f)
    Nd4j.create(readBuffer(f, sampleSize).map(byteAsInt(_).toDouble)).reshape(sampleSize, 1)
  }
}
