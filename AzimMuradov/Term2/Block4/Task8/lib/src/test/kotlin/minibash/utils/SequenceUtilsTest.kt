package minibash.utils

import minibash.TestUtils.asArguments
import minibash.utils.SequenceUtils.concat
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertContentEquals

internal class SequenceUtilsTest {

    @ParameterizedTest
    @MethodSource("sequencesWithConcatenatedSequences")
    fun `concatenate nullable sequences`(sequences: Array<Sequence<Int>?>, concatenated: Sequence<Int>?) {
        assertContentEquals(expected = concatenated, actual = concat(*sequences))
    }


    private companion object {

        @JvmStatic
        fun sequencesWithConcatenatedSequences() = listOf(
            arrayOf<Sequence<Int>?>() to null,
            arrayOf<Sequence<Int>?>(null) to null,
            arrayOf<Sequence<Int>?>(null, null) to null,
            arrayOf<Sequence<Int>?>(null, null, null) to null,
            arrayOf(sequenceOf<Int>()) to sequenceOf(),
            arrayOf(sequenceOf(1)) to sequenceOf(1),
            arrayOf(sequenceOf(1), null) to sequenceOf(1),
            arrayOf(null, sequenceOf(1), null) to sequenceOf(1),
            arrayOf(sequenceOf(1), sequenceOf(2)) to sequenceOf(1, 2),
            arrayOf(sequenceOf(1), sequenceOf(2), null) to sequenceOf(1, 2),
            arrayOf(sequenceOf(1), sequenceOf(2, 3), sequenceOf(4)) to sequenceOf(1, 2, 3, 4),
        ).asArguments()
    }
}
