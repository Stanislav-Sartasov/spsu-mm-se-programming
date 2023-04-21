package hashtable

import hashtable.TestUtils.asArguments
import org.junit.jupiter.api.Assertions.assertFalse
import org.junit.jupiter.api.Assertions.assertTrue
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource

internal class AbstractHashTableTest {

    @ParameterizedTest
    @MethodSource("equalAbstractHashTablesPairs")
    fun `equal hash tables`(a: Any, b: Any) {
        assertTrue { a == b }
    }

    @ParameterizedTest
    @MethodSource("notEqualAbstractHashTablesPairs")
    fun `not equal hash tables`(a: Any, b: Any) {
        assertFalse { a == b }
    }

    @ParameterizedTest
    @MethodSource("equalAbstractHashTablesPairs")
    fun `equal hash codes`(a: Any, b: Any) {
        assertTrue { a.hashCode() == b.hashCode() }
    }

    @ParameterizedTest
    @MethodSource("notEqualAbstractHashTablesPairs")
    fun `not equal hash codes`(a: Any, b: Any) {
        assertFalse { a.hashCode() == b.hashCode() }
    }


    private companion object {

        @JvmStatic
        private fun equalAbstractHashTablesPairs() = TestUtils.equalAbstractHashTablesPairs.asArguments()

        @JvmStatic
        private fun notEqualAbstractHashTablesPairs() = TestUtils.notEqualAbstractHashTablesPairs.asArguments()
    }
}
