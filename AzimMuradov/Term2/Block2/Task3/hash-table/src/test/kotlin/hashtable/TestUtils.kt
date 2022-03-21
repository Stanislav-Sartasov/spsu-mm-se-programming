package hashtable

import org.junit.jupiter.params.provider.Arguments
import kotlin.streams.asStream
import hashtable.HashTable.Entry as e
import hashtable.chained.ChainedHashTable.Companion.of as hashTableOf
import hashtable.chained.MutableChainedHashTable.Companion.of as mutableHashTableOf

internal object TestUtils {

    private const val A: String = "Alice"

    private const val B: String = "Bob"

    private const val C: String = "Carol"


    fun <A, B> List<Pair<A, B>>.asArguments() = asSequence().map { (a, b) ->
        Arguments.of(a, b)
    }.asStream()

    @JvmName(name = "triplesAsArguments")
    fun <A, B, C> List<Triple<A, B, C>>.asArguments() = asSequence().map { (a, b, c) ->
        Arguments.of(a, b, c)
    }.asStream()


    val hashTables = listOf(
        hashTableOf(),
        hashTableOf(A to 42),
        hashTableOf(A to 42, B to 17),
        hashTableOf(A to 42, B to 17, C to 17),
        hashTableOf(A to 42, B to 17, A to 43),
        hashTableOf(*('A'..'Z').map { "$it" to it.code }.toTypedArray())
    )


    val isEmptyReturns = listOf(true, false, false, false, false, false)

    val isNotEmptyReturns = isEmptyReturns.map { !it }

    val containsKeyArguments = listOf(A, B, C, "X")

    val containsKeyReturns = listOf(
        listOf(false, false, false, false),
        listOf(true, false, false, false),
        listOf(true, true, false, false),
        listOf(true, true, true, false),
        listOf(true, true, false, false),
        listOf(false, false, false, true)
    )

    val containsEntryArguments = listOf(
        e(A, 42), e(B, 17), e(C, 17), e("X", 'X'.code),
        e(A, 43), e(B, 18), e(C, 18), e("X", 'X'.code + 1)
    )

    val containsEntryReturns = listOf(
        listOf(false, false, false, false, false, false, false, false),
        listOf(true, false, false, false, false, false, false, false),
        listOf(true, true, false, false, false, false, false, false),
        listOf(true, true, true, false, false, false, false, false),
        listOf(false, true, false, false, true, false, false, false),
        listOf(false, false, false, true, false, false, false, false)
    )

    val countedEntries = listOf(
        mapOf(),
        mapOf(e(A, 42) to 1),
        mapOf(e(A, 42) to 1, e(B, 17) to 1),
        mapOf(e(A, 42) to 1, e(B, 17) to 1, e(C, 17) to 1),
        mapOf(e(A, 43) to 1, e(B, 17) to 1),
        mapOf(*('A'..'Z').map { e("$it", it.code) to 1 }.toTypedArray())
    )

    val maps = listOf(
        mapOf(),
        mapOf(A to 42),
        mapOf(A to 42, B to 17),
        mapOf(A to 42, B to 17, C to 17),
        mapOf(A to 42, B to 17, A to 43),
        mapOf(*('A'..'Z').map { "$it" to it.code }.toTypedArray())
    )

    val mutableMaps = listOf(
        mutableMapOf(),
        mutableMapOf(A to 42),
        mutableMapOf(A to 42, B to 17),
        mutableMapOf(A to 42, B to 17, C to 17),
        mutableMapOf(A to 42, B to 17, A to 43),
        mutableMapOf(*('A'..'Z').map { "$it" to it.code }.toTypedArray())
    )


    val equalAbstractHashTablesPairs = listOf(
        hashTableOf<String, Int>().let { it to it },
        hashTableOf<String, Int>() to hashTableOf(),
        hashTableOf<String, Int>() to mutableHashTableOf(),
        hashTableOf(A to 42) to hashTableOf(A to 42),
        hashTableOf(A to 42, B to 17) to hashTableOf(B to 17, A to 42),
        hashTableOf(A to 42, B to 17, C to 17) to hashTableOf(B to 17, A to 42, C to 17),
        hashTableOf(A to 42, B to 17, A to 43) to hashTableOf(A to 43, B to 17),
        hashTableOf(
            *('A'..'Z').map { "$it" to it.code }.toTypedArray()
        ) to hashTableOf(
            *('A'..'Z').reversed().map { "$it" to it.code }.toTypedArray()
        ),
    )

    val notEqualAbstractHashTablesPairs = listOf(
        hashTableOf<String, Int>() to "String",
        hashTableOf<String, Int>() to hashTableOf(A to 42),
        hashTableOf(A to 42) to hashTableOf(A to 43),
        hashTableOf(A to 42) to hashTableOf(B to 42),
        hashTableOf(A to 42, B to 17) to hashTableOf(A to 42, C to 17),
        hashTableOf(A to 42, B to 17, C to 17) to hashTableOf(A to 42, B to 17, C to 17, A to 43),
        hashTableOf(A to 42, B to 17, A to 43) to hashTableOf(A to 42, B to 17),
        hashTableOf(
            *('A'..'Z').map { "$it" to it.code }.toTypedArray()
        ) to hashTableOf(
            *('A'..'Y').map { "$it" to it.code }.toTypedArray()
        ),
    )
}
