package filter

import bmp.BMPFile

interface Filter {
	fun applyTo(image: BMPFile)
}