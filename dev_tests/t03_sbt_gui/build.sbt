name := "t03_sbt_gui"

version := "0.1"

scalaVersion := "2.12.5"

libraryDependencies += "org.bytedeco" % "javacv-platform" % "1.4.1"
libraryDependencies  += "org.scalanlp" %% "breeze" % "0.13"
libraryDependencies  += "org.scalanlp" %% "breeze-natives" % "0.13"
libraryDependencies += "org.scalafx" %% "scalafx" % "8.0.144-R12"
resolvers += "Sonatype Releases" at "https://oss.sonatype.org/content/repositories/releases/"
