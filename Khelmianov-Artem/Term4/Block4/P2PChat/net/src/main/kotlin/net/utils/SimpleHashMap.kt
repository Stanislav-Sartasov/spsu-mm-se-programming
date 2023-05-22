package net.utils

import kotlinx.coroutines.flow.MutableStateFlow
import kotlinx.coroutines.flow.StateFlow
import kotlinx.coroutines.flow.update
import java.util.concurrent.ConcurrentHashMap

internal class SimpleHashMap<K : Any, V : Any> : ConcurrentHashMap<K, V>() {
    private val _flow = MutableStateFlow(super.keys.toList())
    val flow: StateFlow<List<K>> = _flow

    override fun put(key: K, value: V): V? = super.put(key, value)
        .also { _flow.update { super.keys.toList() } }

    override fun remove(key: K): V? = super.remove(key)
        .also { _flow.update { super.keys.toList() } }
}
