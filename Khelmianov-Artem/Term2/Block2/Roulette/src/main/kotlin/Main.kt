import roulette.APlayer
import roulette.Game
import roulette.bots.MartingalePlayer
import roulette.bots.RndDozenPlayer
import roulette.bots.RndNumberPlayer

fun main() {
    val startBalance = 1000
    val games = 100
    val turns = 40
    val roulette = Game()
    val finalBalanceSum = MutableList(3) { 0 }

    println("Симуляция $games игр в рулетку по $turns ставок используя следующих ботов с начальным балансом $startBalance:")
    println("RndNumber - ставит 0-5% изначального баланса на случайное число")
    println("RndDozen - ставит 5% изначального баланса на случайную дюжину")
    println("Martingale - ставит 2$ на чётное, следующая ставка: 2$ на случайную чётность при выигрыше, предыдущая * 2 при проигрыше")

    var bots: List<APlayer> = listOf()
    for (i in 0 until games) {
        bots = listOf(
            RndNumberPlayer("Rnd Number", startBalance),
            RndDozenPlayer("Rnd Dozen", startBalance),
            MartingalePlayer("Martingale", startBalance)
        )
        roulette.removeAllPlayers()
        bots.forEach { roulette.addPlayer(it) }
        roulette.play(turns)
        bots.forEachIndexed { index, aPlayer ->
            finalBalanceSum[index] += aPlayer.balance
        }
    }

    println("Результаты:")
    bots.zip(finalBalanceSum) { bot, sum -> println("${bot.name}: ${sum / games}") }
}