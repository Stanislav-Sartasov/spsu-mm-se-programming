package minibash.utils

import minibash.utils.StringUtils.createErrorMessage
import minibash.utils.StringUtils.createNoArgumentsErrorMessage
import minibash.utils.StringUtils.createTooManyArgumentsErrorMessage
import minibash.utils.StringUtils.ln
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import java.lang.System.lineSeparator as sep

internal class StringUtilsTest {

    @Test
    fun `add line separator`() {
        assertEquals(
            expected = "hello${sep()}",
            actual = "hello".ln()
        )
    }

    @Test
    fun `create error message`() {
        assertEquals(
            expected = "cmd: Exception \"exception\"${sep()}",
            actual = createErrorMessage(cmd = "cmd", e = Exception("exception"))
        )
    }

    @Test
    fun `create error message from exception with null message`() {
        assertEquals(
            expected = "cmd: Exception${sep()}",
            actual = createErrorMessage(cmd = "cmd", e = Exception())
        )
    }

    @Test
    fun `create 'too many arguments' error message`() {
        assertEquals(
            expected = "cmd: too many arguments${sep()}",
            actual = createTooManyArgumentsErrorMessage(cmd = "cmd")
        )
    }

    @Test
    fun `create 'no arguments' error message`() {
        assertEquals(
            expected = "cmd: no arguments${sep()}",
            actual = createNoArgumentsErrorMessage(cmd = "cmd")
        )
    }
}
