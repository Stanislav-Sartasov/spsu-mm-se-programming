import lib.blackjack.bots.RandomBot
import lib.blackjack.bots.BaseBot
import lib.blackjack.base.GameTable
import lib.blackjack.base.GameInfo
import lib.blackjack.base.IPlayer
import lib.blackjack.bots.SimpleBot

fun main() {
    val players: List<IPlayer> = listOf(RandomBot, BaseBot, SimpleBot)
    val standardBankroll: UInt = 100u
    val bankrolls: Array<UInt> = Array(players.size) { standardBankroll }

    val gameInfo = GameInfo(1u..10u, 8u)
    val table = GameTable(gameInfo)

    val casinoWon = table.playSession(players, bankrolls, 40)
    if (casinoWon >= 0)
        println("Casino won $casinoWon")
    else
        println("Casino lost $casinoWon")

    for (i in players.indices) {
        if (bankrolls[i] < standardBankroll)
            println("${players[i].name} lost ${standardBankroll - bankrolls[i]}")
        else
            println("${players[i].name} won ${bankrolls[i] - standardBankroll}")
    }
}
