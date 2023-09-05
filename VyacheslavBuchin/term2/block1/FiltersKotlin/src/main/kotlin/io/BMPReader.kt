package io

import bmp.BMPFile
import java.io.InputStream

class BMPReader(private val inputStream: InputStream) {

	fun readBMP() = BMPFile.from(inputStream.readBytes())
}