@file:OptIn(FlowPreview::class)

package minibash.utils

import kotlinx.coroutines.FlowPreview
import kotlinx.coroutines.flow.*

object FlowUtils {

    fun <T> concat(vararg flows: Flow<T>?): Flow<T>? {
        val notNullFlows = flows.filterNotNull()

        return when (notNullFlows.size) {
            0 -> null
            1 -> notNullFlows.first()
            else -> notNullFlows.asFlow().flattenConcat()
        }
    }


    fun String.toCharFlow() = toList().asFlow()

    suspend fun Flow<Char>.joinToString() = toList().joinToString(separator = "")
}
