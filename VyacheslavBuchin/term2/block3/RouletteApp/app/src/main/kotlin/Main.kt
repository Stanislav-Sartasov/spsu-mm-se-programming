import loader.JarFileBotLoader
import java.io.FileNotFoundException

const val INIT_CASINO_BALANCE = 2281337.0
const val INIT_BOT_BALANCE = 1000.0
const val GAME_COUNT = 40
const val SIMULATION_COUNT = 10000
const val PREAMBLE = "This program simulates $SIMULATION_COUNT times $GAME_COUNT roulette games of loaded from given path bots\n" +
		"and prints average amount of money left\n" +
		"At the beginning of each simulation every bot has $INIT_BOT_BALANCE$\n"

fun main(args: Array<String>) {
	try {
		val path = args[0]
		val bots = JarFileBotLoader().load(path)
		println(PREAMBLE)

		if (bots.isEmpty()) {
			println("No bots were loaded :(")
			return
		}

		println("Following bots were loaded:")
		for (bot in bots)
			println(bot.name)
		println()

		RouletteApp(SIMULATION_COUNT, bots, INIT_BOT_BALANCE).run()
	} catch (e: NoSuchFileException) {
		println("There is no such file: ${e.file.name}")
	} catch (e: AccessDeniedException) {
		println("No access to file: ${e.file.name}")
	} catch (e: FileNotFoundException) {
		println("No such file: ${e.message}")
	} catch (e: ArrayIndexOutOfBoundsException) {
		println("Path to bot library should be passed as first argument (only compiled classes in jar archives are allowed)")
	}
}