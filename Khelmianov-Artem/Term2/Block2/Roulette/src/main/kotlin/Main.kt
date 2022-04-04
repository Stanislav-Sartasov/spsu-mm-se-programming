import bots.MartingalePlayer
import bots.RndDozenPlayer
import bots.RndNumberPlayer
import roulette.Game

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

    val bots = listOf(
        RndNumberPlayer("Rnd Number", startBalance),
        RndDozenPlayer("Rand Number", startBalance),
        MartingalePlayer("Martingale", startBalance)
    )
    for (i in 0 until games) {
        roulette.players.clear()
        bots.forEach { roulette.addPlayer(it) }
        roulette.play(turns)
        bots.forEachIndexed { index, aPlayer ->
            finalBalanceSum[index] += aPlayer.balance
            aPlayer.balance = startBalance
        }
    }

    println("Результаты:")
    bots.zip(finalBalanceSum) { bot, sum -> println("${bot.name}: ${sum / games}") }
}