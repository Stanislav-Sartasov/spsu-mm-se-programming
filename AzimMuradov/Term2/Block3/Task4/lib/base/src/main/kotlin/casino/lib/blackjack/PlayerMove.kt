package casino.lib.blackjack

enum class PlayerMove {
    HIT,
    STAND;
}


fun PlayerMove.invert(): PlayerMove = if (this == PlayerMove.HIT) PlayerMove.STAND else PlayerMove.HIT
