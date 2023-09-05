import roulette.APlayer
import roulette.Game
import roulette.PluginLoader

fun main(args: Array<String>) {
    val startBalance = 1000
    val games = 100
    val turns = 40
    val roulette = Game()
    val pluginPath = when (args.size) {
        0 -> "libs/"
        1 -> args[0]
        else -> throw IllegalArgumentException("Too many parameters")
    }

    val botsClasses = PluginLoader.loadBotsFromDirectory(pluginPath)
    val finalBalanceSum = MutableList(botsClasses.size) { 0 }

    @Suppress("Unchecked_cast")
    val createBots = {
        botsClasses.map {
            it.getConstructor(String::class.java, Int::class.java).newInstance(it.simpleName, startBalance)
        } as List<APlayer>
    }

    println("Симуляция $games игр в рулетку по $turns ставок используя следующих ботов с начальным балансом $startBalance:")
    var bots = createBots()
    bots.forEach { println(it.name) }

    for (i in 0 until games) {
        bots.forEach { roulette.addPlayer(it) }
        roulette.play(turns)
        bots.forEachIndexed { index, aPlayer ->
            finalBalanceSum[index] += aPlayer.balance
        }
        roulette.removeAllPlayers()
        bots = createBots()
    }

    println("\nРезультаты:")
    bots.zip(finalBalanceSum) { bot, sum -> println("${bot.name}: ${sum / games}") }
}