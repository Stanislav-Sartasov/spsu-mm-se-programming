package dynamicArray

import org.junit.jupiter.api.Test

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.assertThrows

internal class DynamicArrayTest {
    @Test
    fun add() {
        val da = DynamicArray("Hello", "World")
        da.add("Test")
        assertEquals(da.toList(), listOf("Hello", "World", "Test"))
    }

    @Test
    fun removeAt() {
        val da = DynamicArray("Hello", "World", "Test")
        val item = da.removeAt(1)
        assertEquals(item, "World")
        assertEquals(da.toList(), listOf("Hello", "Test"))
    }

    @Test
    fun removeLast() {
        val da = DynamicArray(1, 3, 4, 553535)
        val item = da.removeLast()
        assertEquals(item, 553535)
        assertEquals(da.toList(), listOf(1, 3, 4))
    }

    @Test
    fun addAt() {
        val da = DynamicArray(DynamicArray("Hello"), DynamicArray(1), DynamicArray())
        da.addAt(1, DynamicArray(100.123))
        assertEquals(da, DynamicArray(DynamicArray("Hello"), DynamicArray(100.123), DynamicArray(1), DynamicArray()))
    }

    @Test
    fun `can iterate through dynamic array`() {
        val da = DynamicArray<Int>()
        for (i in 5..10) {
            da.add(i)
        }

        var s = 0
        for (elem in da)
            s += elem

        assertEquals(5 + 6 + 7 + 8 + 9 + 10, s)
    }

    @Test
    fun `cant get by index from out of bounds`() {
        assertThrows<IndexOutOfBoundsException> {
            val da = DynamicArray(1, 2, 3)
            da[5]
        }
    }

    @Test
    fun `cant set by index into out of bounds`() {
        assertThrows<IndexOutOfBoundsException> {
            val da = DynamicArray(1, 2, 3)
            da[5] = 1392
        }
    }

    @Test
    fun `can add 10000 items into`() {
        val da = DynamicArray<Int>()
        for (i in 1..10000)
            da.add(i)

        assertEquals((1..10000).toList(), da.toList())
    }

    @Test
    fun `can set items using set operation`() {
        val da = DynamicArray<Int>()
        for (i in 1..10)
            da.add(0)

        for (i in 1..10)
            da[i - 1] = i

        assertEquals((1..10).toList(), da.toList())
    }

    @Test
    fun `find returns item index or -1`() {
        val da = DynamicArray(1, 2, 3, 3)
        assertEquals(2, da.find(3))
        assertEquals(-1, da.find(5))
    }
}