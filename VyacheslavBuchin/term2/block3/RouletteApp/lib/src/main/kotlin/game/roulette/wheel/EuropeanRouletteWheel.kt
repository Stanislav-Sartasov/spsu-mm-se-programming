package game.roulette.wheel

import kotlin.random.Random

class EuropeanRouletteWheel : RouletteWheel {

	private val values: IntArray = intArrayOf(
		0, 32, 15, 19, 4, 21, 2, 25, 17, 34, 6, 27, 13, 36,
		11, 30, 8, 23, 10, 5, 24, 16, 33, 1, 20, 14, 31, 9,
		22, 18, 29, 7, 28, 12, 35, 3, 26
	)

	override val zeroCount: Int
		get() = 1

	override val size: Int
		get() = values.size

	override fun spin() = values[Random.nextInt(0, values.size)]
}