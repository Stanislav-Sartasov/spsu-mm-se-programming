package filter

import bmp.BMPFile
import bmp.Color

class GrayscaleFilter : Filter {
	override fun applyTo(image: BMPFile) {
		for (row in image.colorMap) {
			for (i in row.indices) {
				val rgb = row[i].rgb()
				val color = ((rgb[0] + rgb[1] + rgb[2]) / 3)

				row[i] = Color(color, color, color, row[i].alpha)
			}
		}
	}
}