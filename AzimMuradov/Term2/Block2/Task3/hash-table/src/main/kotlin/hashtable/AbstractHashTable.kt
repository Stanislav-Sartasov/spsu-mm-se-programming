package hashtable

public abstract class AbstractHashTable<K, out V> : HashTable<K, V> {

    final override fun equals(other: Any?): Boolean {
        if (this === other) return true
        if (other !is HashTable<*, *>) return false

        if (entries.toSet() != other.entries.toSet()) return false

        return true
    }

    final override fun hashCode(): Int = entries.hashCode()

    override fun toString(): String = entries.joinToString(
        prefix = "[", postfix = "]",
        transform = { (k, v) -> "{ $k -> $v }" }
    )
}
