package minibash

import org.junit.jupiter.params.provider.Arguments
import org.junit.jupiter.params.provider.Arguments.arguments

object TestUtils {

    operator fun <A, B> List<A>.times(rhs: List<B>): List<Pair<A, B>> = flatMap { a -> rhs.map { b -> a to b } }

    @JvmName(name = "pairsAsArguments")
    fun <A, B> List<Pair<A, B>>.asArguments(): List<Arguments> = map { (a, b) -> arguments(a, b) }

    @JvmName(name = "triplesAsArguments")
    fun <A, B, C> List<Triple<A, B, C>>.asArguments(): List<Arguments> = map { (a, b, c) -> arguments(a, b, c) }

    @JvmName(name = "listsAsArguments")
    fun List<List<*>>.asArguments(n: Int): List<Arguments> = map { arguments(*it.take(n).toTypedArray()) }

    fun <T> Sequence<T>.startsWith(sequence: Sequence<T>): Boolean = (this zip sequence).all { (a, b) -> a == b }
}
