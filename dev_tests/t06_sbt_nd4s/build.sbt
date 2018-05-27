name := "t06_sbt_nd4s"

version := "0.1"

scalaVersion := "2.11.12"

val nd4jVersion = "1.0.0-alpha"

libraryDependencies += "org.nd4j" % "nd4j-native-platform" % nd4jVersion
libraryDependencies += "org.nd4j" %% "nd4s" % nd4jVersion