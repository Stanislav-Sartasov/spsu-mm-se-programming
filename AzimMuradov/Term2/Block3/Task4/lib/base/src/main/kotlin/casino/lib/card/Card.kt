package casino.lib.card

@JvmInline
value class Card private constructor(private val value: Int) {

    val suite: CardSuite get() = CardSuite.values()[value / CardRank.values().size]

    val rank: CardRank get() = CardRank.values()[value % CardRank.values().size]


    operator fun component1(): CardSuite = suite

    operator fun component2(): CardRank = rank


    override fun toString(): String {
        val suiteCodePart = when (suite) {
            CardSuite.SPADES -> "1f0a"
            CardSuite.HEARTS -> "1f0b"
            CardSuite.DIAMONDS -> "1f0c"
            CardSuite.CLUBS -> "1f0d"
        }
        val rankCodePart = when (rank) {
            CardRank.QUEEN, CardRank.KING -> rank.ordinal + 2
            else -> rank.ordinal + 1
        }.toString(radix = 16)

        return String(Character.toChars("$suiteCodePart$rankCodePart".toInt(radix = 16)))
    }


    companion object {

        fun get(suite: CardSuite, rank: CardRank): Card = Card(
            value = suite.ordinal * CardRank.values().size + rank.ordinal
        )

        val deck: List<Card> = List(size = CardSuite.values().size * CardRank.values().size, init = ::Card)
    }
}
