package stream

import io.mockk.every
import lib.weather.stream.ErrorStream
import lib.weather.stream.OutputStream
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

import io.mockk.mockk
import io.mockk.verify
import lib.weather.date.CloudCoverage
import lib.weather.date.Weather
import lib.weather.stream.Action
import lib.weather.stream.Command

class TestStreams {
    @Test
    fun `Test Error Stream print`() {
        ErrorStream().print("some")
    }

    @Test
    fun `Test Output Stream print`() {
        OutputStream().print("some")

        val weather = mockk<Weather>()
        every { weather.toString() } answers { callOriginal() }
        OutputStream().print(weather)
        verify { weather.toString() }
    }

    @Test
    fun `Test command and action`() {
        val command = Command()
        assertEquals(Action.NOTHING, command.action)
        assertEquals(emptyList(), command.commandArgs)
        command.action = Action.ADD
        command.commandArgs = listOf("1", "2")

        assertEquals(Action.ADD, command.action)
        assertEquals(listOf("1", "2"), command.commandArgs)
    }
}