package lib.blackjack.base

class Card(_count: Int, _value: String) {
    val value: String

    var count: Int
        private set
        get() {
            if (field in 2..10)
                return field
            return 11
        }

    init {
        count = _count
        value = _value
    }
}