package hashtable.chained

import org.junit.jupiter.params.provider.Arguments
import kotlin.streams.asStream
import hashtable.HashTable.Entry as e
import hashtable.chained.ChainedHashTable.Companion.of as hashTableOf
import hashtable.chained.MutableChainedHashTable.Companion.of as mutableHashTableOf

internal object TestUtils {

    private const val A: String = "Alice"

    private const val B: String = "Bob"

    private const val C: String = "Carol"


    fun <A> List<A>.asArguments() = asSequence().map { Arguments.of(it) }.asStream()

    @JvmName(name = "pairsAsArguments")
    fun <A, B> List<Pair<A, B>>.asArguments() = asSequence().map { (a, b) ->
        Arguments.of(a, b)
    }.asStream()

    @JvmName(name = "tuplesAsArguments")
    fun List<List<*>>.asArguments() = asSequence().map {
        Arguments.of(*it.toTypedArray())
    }.asStream()


    val chainedHashTables = listOf(
        hashTableOf(),
        hashTableOf(A to 42),
        hashTableOf(A to 42, B to 17),
        hashTableOf(A to 42, B to 17, C to 17),
        hashTableOf(A to 42, B to 17, A to 43),
        hashTableOf(*('A'..'Z').map { "$it" to it.code }.toTypedArray())
    )

    val mutableChainedHashTables = listOf(
        mutableHashTableOf(),
        mutableHashTableOf(A to 42),
        mutableHashTableOf(A to 42, B to 17),
        mutableHashTableOf(A to 42, B to 17, C to 17),
        mutableHashTableOf(A to 42, B to 17, A to 43),
        mutableHashTableOf(*('A'..'Z').map { "$it" to it.code }.toTypedArray())
    )


    val sizes = listOf(0, 1, 2, 3, 2, 26)

    val getArguments = listOf(A, B, C, "X")

    val getReturns = listOf(
        listOf(null, null, null, null),
        listOf(42, null, null, null),
        listOf(42, 17, null, null),
        listOf(42, 17, 17, null),
        listOf(43, 17, null, null),
        listOf(null, null, null, 'X'.code)
    )

    val keys = listOf(
        setOf(),
        setOf(A),
        setOf(A, B),
        setOf(A, B, C),
        setOf(A, B),
        ('A'..'Z').mapTo(mutableSetOf(), Char::toString)
    )

    val values = listOf(
        listOf(),
        listOf(42),
        listOf(42, 17),
        listOf(42, 17, 17),
        listOf(43, 17),
        ('A'..'Z').map(Char::code)
    )

    val entries = listOf(
        setOf(),
        setOf(e(A, 42)),
        setOf(e(A, 42), e(B, 17)),
        setOf(e(A, 42), e(B, 17), e(C, 17)),
        setOf(e(A, 43), e(B, 17)),
        ('A'..'Z').mapTo(mutableSetOf()) { e("$it", it.code) }
    )

    val maps = listOf(
        mapOf(),
        mapOf(A to 42),
        mapOf(A to 42, B to 17),
        mapOf(A to 42, B to 17, C to 17),
        mapOf(A to 42, B to 17, A to 43),
        mapOf(*('A'..'Z').map { "$it" to it.code }.toTypedArray())
    )
}
