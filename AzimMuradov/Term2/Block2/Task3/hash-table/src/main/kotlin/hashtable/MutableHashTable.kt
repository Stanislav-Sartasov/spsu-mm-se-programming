package hashtable

public interface MutableHashTable<K, V> : HashTable<K, V> {

    public fun put(key: K, value: V): V?

    public fun remove(key: K): V?


    public fun clear()
}
