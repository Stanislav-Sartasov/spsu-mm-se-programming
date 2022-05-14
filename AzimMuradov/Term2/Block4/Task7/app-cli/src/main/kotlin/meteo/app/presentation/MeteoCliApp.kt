package meteo.app.presentation

import kotlinx.coroutines.*
import meteo.app.presentation.MeteoCliMessagesWizard.CLOSING_APP_MESSAGE
import meteo.app.presentation.MeteoCliMessagesWizard.GREETINGS_MESSAGE
import meteo.app.presentation.MeteoCliMessagesWizard.HELP_MESSAGE
import meteo.app.presentation.MeteoCliMessagesWizard.WRONG_COMMAND_MESSAGE
import meteo.presentation.mvi.MviStore
import meteo.presentation.mvi.MviView
import meteo.presentation.state.MeteoState
import meteo.presentation.wish.MeteoWish
import java.io.InputStream
import java.io.PrintStream

class MeteoCliApp(
    private val store: MviStore<MeteoWish, MeteoState>,
    private val view: MviView<MeteoState>,
    private val inputStream: InputStream,
    private val outputStream: PrintStream,
) {

    fun run() {
        outputStream.use { printer ->
            printer.println(GREETINGS_MESSAGE)

            scope.launch { store.state.collect(view::render) }

            store.consume(MeteoWish.Load)

            inputStream.bufferedReader().use { reader ->
                while (true) {
                    when (cmdMapper(cmd = reader.readLine())) {
                        UserCommand.Refresh -> store.consume(MeteoWish.Load)
                        UserCommand.Help -> printer.println(HELP_MESSAGE)
                        UserCommand.Exit -> break
                        else -> printer.println(WRONG_COMMAND_MESSAGE)
                    }
                }
            }

            printer.println(CLOSING_APP_MESSAGE)

            scope.cancel()
        }
    }


    companion object {

        private fun cmdMapper(cmd: String): UserCommand? = when (cmd) {
            in REFRESH -> UserCommand.Refresh
            in HELP -> UserCommand.Help
            in EXIT -> UserCommand.Exit
            else -> null
        }


        val REFRESH = setOf("refresh", "r", "обновить")

        val HELP = setOf("help", "h", "man", "помощь")

        val EXIT = setOf("quit", "q", "exit", "выход")


        private val scope = CoroutineScope(Dispatchers.Default)
    }
}
