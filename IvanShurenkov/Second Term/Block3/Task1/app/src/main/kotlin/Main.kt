import lib.blackjack.base.GameTable
import lib.blackjack.base.IPlayer
import lib.blackjack.base.state.GameInfo

fun main(args: Array<String>) {
    if (args.isEmpty()) {
        println("Arguments are empty")
        return
    }

    val bots: List<IPlayer> =
        BotLoader().loadBots(args.toList())

    if (bots.isEmpty()) {
        print("Arguments aren't contains path to jar with bots")
        return
    }

    val commonBamkrolls: Array<Int> = Array(4) { 0 }
    val players: List<IPlayer> = bots
    for (i in 1..50) {
        val standardBankroll: UInt = 100u
        val bankrolls: Array<UInt> = Array(players.size) { standardBankroll }

        val gameInfo = GameInfo(1u..10u, 8u)
        val table = GameTable(gameInfo)

        val casinoWon = table.playSession(players, bankrolls, 40)
        commonBamkrolls[0] += casinoWon

        for (i in players.indices)
            commonBamkrolls[i + 1] += bankrolls[i].toInt() - standardBankroll.toInt()
    }
    println("Average casino winnings are ${commonBamkrolls[0] / 150}")
    for (i in players.indices) {
        println("Average winning for ${players[i].name} are ${commonBamkrolls[i + 1] / 50}")
    }
    return
}
