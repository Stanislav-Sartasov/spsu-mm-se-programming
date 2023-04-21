package hashtable.chained

import hashtable.*

public fun <K, V> HashTable<K, V>.toHashTable(): HashTable<K, V> = ChainedHashTable.of(
    *entries.map { it.toPair() }.toTypedArray()
)

public fun <K, V> HashTable<K, V>.toMutableHashTable(): MutableHashTable<K, V> = MutableChainedHashTable.of(
    *entries.map { it.toPair() }.toTypedArray()
)

public fun <K, V> Map<K, V>.toHashTable(): HashTable<K, V> = ChainedHashTable.of(
    *entries.map { it.toPair() }.toTypedArray()
)

public fun <K, V> Map<K, V>.toMutableHashTable(): MutableHashTable<K, V> = MutableChainedHashTable.of(
    *entries.map { it.toPair() }.toTypedArray()
)
