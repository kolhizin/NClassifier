# GUI

**ScalaFX** is hard to understand due to non-existent documentation. Some things seems like easy, some are pretty complicated. Developed GUI in C# instead.

# Image processing

**OpenCV** (through javacv) support is oustandingly awful! It is easier to write native code than to try something in Scala. Won't use OpenCV as of now.

# Linear Algebra

**Breeze** is actually pretty nice (except for Vector vs Matrix), but lacks Tensor support. This makes Breeze unusable for purpose of image processing (convolution would require use of 3-/4-Tensors or even higher).

**ND4J/ND4S** is in progress of testing. But as of now (May'18) it does not support Scala 2.12, which in turn leads to fallback to Java-8 instead of Java-10.

Does not work with 32-bit JVM. Only 64-bit.

**MXNet** seems to be very promising for DL, but is not yet supported on Windows (May'18). But things are changing quickly.
