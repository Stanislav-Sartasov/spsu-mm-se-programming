package hashtable.chained

import hashtable.AbstractHashTable
import hashtable.HashTable.Entry
import hashtable.MutableHashTable

public class MutableChainedHashTable<K, V> private constructor() : AbstractHashTable<K, V>(), MutableHashTable<K, V> {

    override var size: Int = 0
        private set

    private var capacity = INITIAL_CAPACITY

    private var chains = List(size = capacity) { mutableListOf<Entry<K, V>>() }


    override fun get(key: K): V? = key.coords.let { (i, j) -> get(i, j) }


    override val keys: List<K>
        get() = chains.flatMap { chain ->
            chain.map { it.key }
        }

    override val values: List<V>
        get() = chains.flatMap { chain ->
            chain.map { it.value }
        }

    override val entries: List<Entry<K, V>>
        get() = chains.flatten()


    override fun put(key: K, value: V): V? {
        val (i, j) = key.coords
        val prevValue = get(i, j)

        if (j != null) {
            chains[i][j] = Entry(key, value)
        } else {
            size += 1
            chains[i] += Entry(key, value)
            balance()
        }

        return prevValue
    }

    override fun remove(key: K): V? {
        val (i, j) = key.coords
        val removedValue = get(i, j)

        if (j != null) {
            size -= 1
            chains[i].removeAt(j)
        }

        return removedValue
    }

    private fun balance() {
        if (size > chains.size) {
            capacity *= 2
            chains = List<MutableList<Entry<K, V>>>(size = capacity) { mutableListOf() }.apply {
                for (entry in entries) {
                    get(entry.key.index) += entry
                }
            }
        }
    }


    override fun clear() {
        size = 0
        capacity = INITIAL_CAPACITY
        chains = List(size = capacity) { mutableListOf() }
    }


    private val K.coords get() = index to chainIndex

    private val K.index get() = hashCode() % capacity

    private val K.chainIndex: Int? get() = chains[index].indexOfFirst { it.key == this }.takeUnless { it == -1 }

    private fun get(i: Int, j: Int?): V? = if (j != null) chains[i][j].value else null


    public companion object {

        public fun <K, V> of(vararg entries: Pair<K, V>): MutableHashTable<K, V> =
            MutableChainedHashTable<K, V>().apply {
                for ((k, v) in entries) {
                    put(k, v)
                }
            }


        private const val INITIAL_CAPACITY: Int = 8
    }
}
