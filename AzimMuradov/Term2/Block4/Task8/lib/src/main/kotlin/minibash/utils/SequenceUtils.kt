package minibash.utils

object SequenceUtils {

    fun <T> concat(vararg sequences: Sequence<T>?): Sequence<T>? {
        val notNullFlows = sequences.filterNotNull()

        return when (notNullFlows.size) {
            0 -> null
            1 -> notNullFlows.first()
            else -> notNullFlows.asSequence().flatten()
        }
    }
}
