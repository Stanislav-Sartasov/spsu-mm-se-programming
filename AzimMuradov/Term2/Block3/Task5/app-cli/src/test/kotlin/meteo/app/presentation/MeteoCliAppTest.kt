package meteo.app.presentation

import io.mockk.mockk
import meteo.app.presentation.MeteoCliMessagesWizard.CLOSING_APP_MESSAGE
import meteo.app.presentation.MeteoCliMessagesWizard.GREETINGS_MESSAGE
import meteo.app.presentation.MeteoCliMessagesWizard.HELP_MESSAGE
import meteo.app.presentation.MeteoCliMessagesWizard.WRONG_COMMAND_MESSAGE
import meteo.presentation.mvi.MviStore
import meteo.presentation.mvi.MviView
import meteo.presentation.state.MeteoState
import meteo.presentation.wish.MeteoWish
import org.junit.jupiter.api.Test
import java.io.*
import kotlin.test.assertEquals

internal class MeteoCliAppTest {

    @Test
    fun `run app`() {
        val mockedStore = mockk<MviStore<MeteoWish, MeteoState>>(relaxUnitFun = true)
        val mockedView = mockk<MviView<MeteoState>>(relaxUnitFun = true)

        val inputStream = ByteArrayInputStream(
            buildString {
                appendLine("refresh")
                appendLine("help")
                appendLine("not a command")
                appendLine("exit")
            }.toByteArray()
        )
        val byteArrayOutputStream = ByteArrayOutputStream()
        val outputStream = PrintStream(byteArrayOutputStream, true)

        val app = MeteoCliApp(mockedStore, mockedView, inputStream, outputStream)

        app.run()

        assertEquals(
            expected = buildString {
                appendLine(GREETINGS_MESSAGE)
                appendLine(HELP_MESSAGE)
                appendLine(WRONG_COMMAND_MESSAGE)
                appendLine(CLOSING_APP_MESSAGE)
            },
            actual = byteArrayOutputStream.toString()
        )
    }
}
