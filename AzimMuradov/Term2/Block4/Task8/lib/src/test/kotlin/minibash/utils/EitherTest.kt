package minibash.utils

import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

internal class EitherTest {

    @Test
    fun `create left`() {
        val hello = "Hello"
        assertEquals(expected = Either.Left(hello), actual = hello.left())
    }

    @Test
    fun `create right`() {
        val hello = "Hello"
        assertEquals(expected = Either.Right(hello), actual = hello.right())
    }

    @Test
    fun `fold left`() {
        val x = Either.Left(value = "17")
        assertEquals(expected = 17, actual = x.fold(onLeft = { it.toInt() }, onRight = { 0 }))
    }

    @Test
    fun `fold right`() {
        val x = Either.Right(value = "42")
        assertEquals(expected = 42, actual = x.fold(onLeft = { 0 }, onRight = { it.toInt() }))
    }
}
