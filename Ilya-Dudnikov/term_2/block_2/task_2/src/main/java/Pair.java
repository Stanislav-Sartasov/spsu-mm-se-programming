public class Pair<K, V> {
	public K key;
	private V value;

	public Pair(K key, V value) {
		this.key = key;
		this.value = value;
	}

	public K first() {
		return key;
	}

	public V second() {
		return value;
	}

	public boolean equals(Pair<K, V> other) {
		return this.key == other.key && this.value == other.value;
	}
}
