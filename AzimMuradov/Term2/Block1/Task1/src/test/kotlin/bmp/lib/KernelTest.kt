package bmp.lib

import org.junit.jupiter.api.Test
import kotlin.test.assertEquals
import kotlin.test.assertFailsWith

internal class KernelTest {

    @Test
    fun `create Kernel from matrix`() {
        val actualKernel = Kernel.fromMatrix(
            listOf(
                listOf(1, 2, 3),
                listOf(4, 5, 6),
                listOf(7, 8, 9),
            )
        )
        assertEquals(
            expected = 3,
            actual = actualKernel.size
        )
        assertEquals(
            expected = listOf(1.0, 2.0, 3.0, 4.0, 5.0, 6.0, 7.0, 8.0, 9.0),
            actual = actualKernel.elements
        )
    }

    @Test
    fun `fail to create Kernel from empty matrix`() {
        assertFailsWith<IllegalArgumentException>(
            message = "Wrong kernel definition: matrix should not be empty"
        ) {
            Kernel.fromMatrix(listOf())
        }
    }

    @Test
    fun `fail to create Kernel from wrong matrix`() {
        assertFailsWith<IllegalArgumentException>(
            message = "Wrong kernel definition: matrix should have the same number of rows and columns"
        ) {
            Kernel.fromMatrix(
                listOf(
                    listOf(1, 2, 3),
                    listOf(4, 5, 6),
                    listOf(7, 8),
                )
            )
        }
    }
}
