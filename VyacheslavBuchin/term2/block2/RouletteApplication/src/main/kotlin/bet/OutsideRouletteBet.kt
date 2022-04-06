package bet

enum class OutsideRouletteBet(wonValues: List<Int>) : Bet {
	EVEN((1..36).filter { it % 2 == 0 }),
	ODD((1..36).filter { it % 2 == 1 }),
	P12((1..12).toList()),
	M12((13..24).toList()),
	D12((25..36).toList()),
	RED(listOf(1, 3, 5, 7, 9, 12, 14, 16, 18, 19, 21, 23, 25, 27, 30, 32, 34, 36)),
	BLACK(listOf(2, 4, 6, 8, 10, 11, 13, 15, 17, 20, 22, 24, 26, 28, 29, 31, 33, 35));

	protected val wonValues: Set<Int> = wonValues.toSet()

	override fun isWon(value: Int) = wonValues.contains(value)

	override fun wonValuesCount() = wonValues.size

}
