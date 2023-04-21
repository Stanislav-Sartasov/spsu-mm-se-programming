public interface Map<K, V> {
	V get(K key);

	public void set(K key, V value);

	public boolean containsKey(K key);

	public void remove(K key);

	public int size();
}
