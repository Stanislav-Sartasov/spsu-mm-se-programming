package bmp.lib

import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import kotlin.test.assertFailsWith

internal class ColorTest {

    @Test
    fun `create Color from list`() {
        assertEquals(
            expected = Color(b = 35u.toUByte(), g = 42u.toUByte(), r = 208u.toUByte()),
            actual = listOf(35u, 42u, 208u).map(UInt::toUByte).toColor()
        )
    }

    @Test
    fun `fail to create Color from short list`() {
        assertFailsWith<IndexOutOfBoundsException> {
            listOf(35u, 42u).map(UInt::toUByte).toColor()
        }
    }

    @Test
    fun `map Color to list`() {
        assertEquals(
            expected = listOf(35u, 42u, 208u).map(UInt::toUByte),
            actual = Color(b = 35u.toUByte(), g = 42u.toUByte(), r = 208u.toUByte()).toList()
        )
    }
}
