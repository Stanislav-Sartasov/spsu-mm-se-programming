import java.util.ArrayList;

public class HashMap<K, V> implements Map<K, V> {
	private final int INITIAL_SIZE = 1091;
	private final int MAX_BUCKET_SIZE = 40;

	private ArrayList<Pair<K, V>>[] hashTable;
	private int size;

	public HashMap() {
		hashTable = new ArrayList[INITIAL_SIZE];
		for (int i = 0; i < INITIAL_SIZE; i++)
			hashTable[i] = new ArrayList<>();
		size = INITIAL_SIZE;
	}

	@Override
	public V get(K key) {
		for (var pair : hashTable[key.hashCode() % size]) {
			if (key == pair.first())
				return pair.second();
		}
		return null;
	}

	@Override
	public void set(K key, V value) {
		boolean found = false;

		for (var pair : hashTable[key.hashCode() % size]) {
			if (key == pair.first()) {
				pair = new Pair<>(key, value);
				found = true;
			}
		}

		if (!found)
			hashTable[key.hashCode() % size].add(new Pair<>(key, value));

		if (hashTable[key.hashCode() % size].size() > MAX_BUCKET_SIZE)
			rebalance();
	}

	@Override
	public boolean containsKey(K key) {
		for (var pair : hashTable[key.hashCode() % size]) {
			if (key == pair.first())
				return true;
		}

		return false;
	}

	@Override
	public void remove(K key) {
		V value = get(key);

		if (value == null)
			return;
		hashTable[key.hashCode() % size].remove(new Pair<>(key, value));
	}

	@Override
	public int size() {
		return size;
	}

	private boolean isPrime(int num) {
		for (int i = 2; i * i <= num; i++) {
			if (num % i == 0)
				return false;
		}
		return true;
	}

	private int findNextSize() {
		int newSize = 2 * size + 1;
		while (!isPrime(newSize))
			newSize += 2;
		return newSize;
	}

	private void rebalance() {
		int newSize = findNextSize();
		var tmpHashTable = hashTable.clone();
		hashTable = new ArrayList[newSize];

		for (int i = 0; i < size; i++) {
			for (var pair : tmpHashTable[i]) {
				set(pair.first(), pair.second());
			}
		}

		size = newSize;
	}
}
