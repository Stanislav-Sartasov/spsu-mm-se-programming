package roulette

import roulette.enums.Colors
import roulette.enums.Dozens
import roulette.enums.Parity

@Suppress("DataClassPrivateConstructor")
data class SpinResult private constructor(
    val number: Int,
    val color: Colors,
    val dozen: Dozens,
    val parity: Parity,
) {
    constructor(number: Int) : this(
        number,
        Colors.fromInt(number),
        Dozens.fromInt(number),
        Parity.fromInt(number)
    )

}