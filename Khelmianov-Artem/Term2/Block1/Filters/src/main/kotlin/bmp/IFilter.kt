package bmp.bmp

import bmp.BMPImage

interface IFilter {
    fun apply(image: BMPImage)
}
