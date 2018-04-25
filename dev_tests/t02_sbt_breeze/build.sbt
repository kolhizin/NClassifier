name := "t02_sbt_breeze"

version := "0.1"

scalaVersion := "2.12.5"

libraryDependencies  ++= Seq(
  "org.scalanlp" %% "breeze" % "0.13",
  "org.scalanlp" %% "breeze-natives" % "0.13",
)

resolvers += "Sonatype Releases" at "https://oss.sonatype.org/content/repositories/releases/"