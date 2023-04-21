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

	public boolean equals(Object other) {
		if (other instanceof Pair<?, ?>)
			return this.key.equals(((Pair<?, ?>) other).key)  && this.value.equals(((Pair<?, ?>) other).value);
		return false;
	}
}
