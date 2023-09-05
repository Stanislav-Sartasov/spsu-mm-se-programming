package game.roulette.wheel

interface RouletteWheel {
	val size: Int
	val zeroCount: Int

	fun spin(): Int
}