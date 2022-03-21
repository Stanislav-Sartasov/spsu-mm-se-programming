package hashtable

public fun <K, V> HashTable<K, V>.isEmpty(): Boolean =
    entries.isEmpty()

public fun <K, V> HashTable<K, V>.isNotEmpty(): Boolean =
    entries.isNotEmpty()

public operator fun <K, V> HashTable<K, V>.contains(key: K): Boolean =
    get(key) != null

public operator fun <K, V> HashTable<K, V>.contains(entry: HashTable.Entry<K, V>): Boolean =
    get(entry.key) == entry.value

public operator fun <K, V> HashTable<K, V>.iterator(): Iterator<HashTable.Entry<K, V>> =
    entries.iterator()

public operator fun <K, V> MutableHashTable<K, V>.set(key: K, value: V) {
    put(key, value)
}


public fun <K, V> HashTable<K, V>.toMap(): Map<K, V> =
    entries.associate { it.toPair() }

public fun <K, V> HashTable<K, V>.toMutableMap(): MutableMap<K, V> =
    entries.associateTo(mutableMapOf()) { it.toPair() }


public fun <K, V> HashTable.Entry<K, V>.toPair(): Pair<K, V> = key to value

public fun <K, V> Pair<K, V>.toHashTableEntry(): HashTable.Entry<K, V> = HashTable.Entry(first, second)
