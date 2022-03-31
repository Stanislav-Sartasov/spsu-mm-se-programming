package casino.lib.blackjack

import casino.lib.blackjack.BlackJackConstants.BJ
import casino.lib.card.*

@JvmInline
value class Hand(val cards: List<Card>)


fun Hand.total(): Int {
    val aceCount = cards.count { it.rank == CardRank.ACE }

    val basicSum = cards.sumOf(Card::hardValue)
    val availableToFillWithAces = (BJ - basicSum).coerceAtLeast(minimumValue = 0)
    val aceAs11Sum = (availableToFillWithAces / 10).coerceAtMost(aceCount) * 10

    return basicSum + aceAs11Sum
}

fun Hand.isSoft(): Boolean = total() > cards.sumOf(Card::hardValue)

fun Hand.isHard(): Boolean = !isSoft()

fun Hand.isBJ(): Boolean = total() == BJ && cards.size == 2

fun Hand.isBust(): Boolean = total() > BJ

operator fun Hand.compareTo(other: Hand): Int {
    fun Boolean.toInt() = if (this) 1 else 0

    val bustCmp = -(isBust().toInt() - other.isBust().toInt())
    val bjCmp = isBJ().toInt() - other.isBJ().toInt()
    val totalCmp = total() - other.total()

    if (bustCmp != 0) return bustCmp
    if (bjCmp != 0) return bjCmp
    return totalCmp
}


private fun Card.hardValue() = when {
    rank.isNumeral() -> rank.ordinal + 1
    rank.isFace() -> 10
    else -> 1
}
