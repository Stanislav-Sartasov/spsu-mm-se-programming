package meteo.app.presentation

import io.mockk.every
import io.mockk.mockk
import kotlinx.coroutines.flow.MutableStateFlow
import meteo.app.presentation.MeteoCliMessagesWizard.CLOSING_APP_MESSAGE
import meteo.app.presentation.MeteoCliMessagesWizard.GREETINGS_MESSAGE
import meteo.app.presentation.MeteoCliMessagesWizard.HELP_MESSAGE
import meteo.app.presentation.MeteoCliMessagesWizard.WRONG_COMMAND_MESSAGE
import meteo.ln
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

        every { mockedStore.state } returns MutableStateFlow(MeteoState.Uninitialised)

        val mockedView = mockk<MviView<MeteoState>>(relaxUnitFun = true)

        val inputStream = ByteArrayInputStream(
            buildString {
                append("refresh".ln())
                append("help".ln())
                append("not a command".ln())
                append("exit".ln())
            }.toByteArray()
        )
        val byteArrayOutputStream = ByteArrayOutputStream()
        val outputStream = PrintStream(byteArrayOutputStream, true)

        val app = MeteoCliApp(mockedStore, mockedView, inputStream, outputStream)

        app.run()

        assertEquals(
            expected = buildString {
                append(GREETINGS_MESSAGE.ln())
                append(HELP_MESSAGE.ln())
                append(WRONG_COMMAND_MESSAGE.ln())
                append(CLOSING_APP_MESSAGE.ln())
            },
            actual = byteArrayOutputStream.toString()
        )
    }
}
