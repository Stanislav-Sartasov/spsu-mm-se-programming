package casino.lib.shoe

import casino.lib.card.Card

interface Shoe {

    val dealt: List<Card>

    fun dealCard(): Card

    fun isNotEmpty(): Boolean
}
