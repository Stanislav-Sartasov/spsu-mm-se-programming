package deanery.set

import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertEquals


class SetTest {

    @ParameterizedTest
    @MethodSource("sets")
    fun `test add`(set: ConcurrentSet<Int>) {
        set.add(1)
        assertEquals(expected = 1, actual = set.count)

        set.add(4)
        assertEquals(expected = 2, actual = set.count)

        set.add(3)
        assertEquals(expected = 3, actual = set.count)

        set.add(1)
        assertEquals(expected = 3, actual = set.count)

        set.add(4)
        assertEquals(expected = 3, actual = set.count)
    }

    @ParameterizedTest
    @MethodSource("sets")
    fun `test remove`(s: ConcurrentSet<Int>) {
        val set = s.apply {
            (1..5).forEach(::add)
        }

        set.remove(1)
        assertEquals(expected = 4, actual = set.count)

        set.remove(4)
        assertEquals(expected = 3, actual = set.count)

        set.remove(3)
        assertEquals(expected = 2, actual = set.count)

        set.remove(1)
        assertEquals(expected = 2, actual = set.count)

        set.remove(4)
        assertEquals(expected = 2, actual = set.count)

        set.remove(100)
        assertEquals(expected = 2, actual = set.count)

        set.remove(2)
        assertEquals(expected = 1, actual = set.count)

        set.remove(5)
        assertEquals(expected = 0, actual = set.count)

        set.remove(500)
        assertEquals(expected = 0, actual = set.count)
    }

    @ParameterizedTest
    @MethodSource("sets")
    fun `test contains`(s: ConcurrentSet<Int>) {
        val set = s.apply {
            (1..5).forEach(::add)
        }

        (1..5).forEach {
            assertEquals(expected = true, actual = it in set)
        }

        assertEquals(expected = false, actual = 0 in set)

        assertEquals(expected = false, actual = -100 in set)

        assertEquals(expected = false, actual = 100 in set)
    }


    companion object {

        @JvmStatic
        private fun sets(): List<ConcurrentSet<Int>> = listOf(LazySet(), NonBlockingSet())
    }
}
