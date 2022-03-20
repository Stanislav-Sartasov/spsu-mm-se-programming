package hashtable

public interface HashTable<K, out V> {

    public val size: Int

    public operator fun get(key: K): V?


    public val keys: List<K>

    public val values: List<V>

    public val entries: List<Entry<K, V>>


    public data class Entry<out K, out V>(val key: K, val value: V)
}
