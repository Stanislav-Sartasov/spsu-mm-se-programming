package dynamicarray

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test

internal class DynamicArrayTest {

    lateinit var array: DynamicArray<Int>

    @BeforeEach
    fun setUp() {
        array = DynamicArray<Int>()
        for (i in 0..9)
            array.add(i)
    }

    @Test
    fun getSize() {
        assertEquals(10, array.size)
    }

    @Test
    fun get() {
        assertEquals(0, array[0])
        assertEquals(9, array[array.size - 1])

        assertThrows(IllegalArgumentException::class.java) { array[array.size] }
        assertThrows(IllegalArgumentException::class.java) { array[-1] }
    }

    @Test
    fun set() {
        array[2] = 42
        assertEquals(42, array[2])
    }

    @Test
    fun add() {
        array.add(123)
        assertEquals(123, array[array.size - 1])

        assertThrows(IllegalArgumentException::class.java) { array.add(0, 42) }
    }

    @Test
    fun remove() {
        array.remove(1)
        assertEquals(2, array[1])
    }

    @Test
    fun indexOf() {
        assertEquals(5, array.indexOf(5))
        assertEquals(-1, array.indexOf(50))
    }

    @Test
    fun isEmpty() {
        assertFalse(array.isEmpty())
        assertTrue(DynamicArray<Int>().isEmpty())
    }

    @Test
    fun isNotEmpty() {
        assertTrue(array.isNotEmpty())
        assertFalse(DynamicArray<Int>().isNotEmpty())
    }

    @Test
    fun contains() {
        assertTrue(array.contains(3))
        assertFalse(array.contains(123))
    }
}