package pixel

import kotlin.math.abs
import kotlin.math.min

data class Pixel(val blue: Int, val green: Int, val red: Int, val alpha: Int) {

	operator fun plus(element: Pixel): Pixel {
		return Pixel(
			blue + element.blue, green + element.green,
			red + element.red, alpha + element.alpha
		)
	}

	operator fun times(element: Int): Pixel {
		return Pixel(
			blue * element, green * element,
			red * element, alpha * element
		)
	}

	operator fun div(element: Int): Pixel {
		return Pixel(
			blue / element, green / element,
			red / element, alpha / element
		)
	}

}

fun List<Pixel>.sum(): Pixel {
	var res = Pixel(0, 0, 0, 0)
	for (elem in this) {
		res += elem
	}
	return res
}

fun abs(element: Pixel): Pixel {
	return Pixel(
		min(255, abs(element.blue)), min(255, abs(element.green)),
		min(255, abs(element.red)), min(255, abs(element.alpha))
	)
}