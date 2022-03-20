package hashtable.chained

import hashtable.AbstractHashTable
import hashtable.HashTable

public class ChainedHashTable<K, V> private constructor(
    hashTable: HashTable<K, V>,
) : AbstractHashTable<K, V>(), HashTable<K, V> by hashTable {

    public companion object {

        public fun <K, V> of(vararg entries: Pair<K, V>): HashTable<K, V> = ChainedHashTable(
            hashTable = MutableChainedHashTable.of(*entries)
        )
    }
}
