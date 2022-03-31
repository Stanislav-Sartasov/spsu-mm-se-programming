package bmp.lib

import org.junit.jupiter.api.Nested
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import kotlin.test.assertFailsWith

internal class ImgFilterTest {

    @Test
    fun `filter with window`() {
        assertEquals(
            expected = FILTERED_RAW_IMG,
            actual = RAW_IMG.filterWithWindow(windowSize = 2) {
                it.first()
            }
        )
    }

    @Test
    fun convolve() {
        assertEquals(
            expected = FILTERED_RAW_IMG,
            actual = RAW_IMG.convolve(
                kernel = Kernel.fromMatrix(
                    listOf(
                        listOf(2, 0),
                        listOf(0, 0),
                    )
                ),
                postProcessing = { it / 2 }
            )
        )
    }

    @Nested
    inner class Transpose {

        @Test
        fun `valid matrix transposed`() {
            assertEquals(
                expected = List(size = 4) { x -> List(size = 3) { y -> "$y$x" } },
                actual = List(size = 3) { y -> List(size = 4) { x -> "$y$x" } }.transpose()
            )
        }

        @Test
        fun `valid empty matrix transposed`() {
            assertEquals(
                expected = listOf<List<Int>>(),
                actual = listOf<List<Int>>().transpose()
            )
        }

        @Test
        fun `not valid matrix fails to transpose`() {
            assertFailsWith<IllegalArgumentException>(
                message = "This two-dimensional list is not a valid matrix, so it cannot be transposed"
            ) {
                listOf(
                    listOf(1, 2, 3),
                    listOf(1, 2, 4),
                    listOf(1, 2)
                ).transpose()
            }
        }

        @Test
        fun `not valid empty matrix fails to transpose`() {
            assertFailsWith<IllegalArgumentException>(
                message = "This two-dimensional list is not a valid matrix, so it cannot be transposed"
            ) {
                listOf<List<Int>>(
                    listOf(),
                    listOf(),
                    listOf()
                ).transpose()
            }
        }
    }


    private companion object {

        private val RAW_IMG = RawImg(
            width = 4,
            height = 3,
            pixels = List(size = 3) { y ->
                List(size = 4) { x ->
                    Color(
                        b = (y * 10).toUByte(),
                        g = x.toUByte(),
                        r = (y * 10 + x).toUByte()
                    )
                }
            }
        )

        private val FILTERED_RAW_IMG = RawImg(
            width = 4,
            height = 3,
            pixels = listOf(
                listOf(
                    Color(0.toUByte(), 0.toUByte(), 0.toUByte()),
                    Color(0.toUByte(), 0.toUByte(), 0.toUByte()),
                    Color(0.toUByte(), 1.toUByte(), 1.toUByte()),
                    Color(0.toUByte(), 2.toUByte(), 2.toUByte()),
                ),
                listOf(
                    Color(0.toUByte(), 0.toUByte(), 0.toUByte()),
                    Color(0.toUByte(), 0.toUByte(), 0.toUByte()),
                    Color(0.toUByte(), 1.toUByte(), 1.toUByte()),
                    Color(0.toUByte(), 2.toUByte(), 2.toUByte()),
                ),
                listOf(
                    Color(10.toUByte(), 0.toUByte(), 10.toUByte()),
                    Color(10.toUByte(), 0.toUByte(), 10.toUByte()),
                    Color(10.toUByte(), 1.toUByte(), 11.toUByte()),
                    Color(10.toUByte(), 2.toUByte(), 12.toUByte()),
                ),
            )
        )
    }
}
