import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.Random;

import static org.junit.jupiter.api.Assertions.*;

class HashMapTest {
	HashMap<Integer, Integer> hashMap;

	@BeforeEach
	void setUp() {
		hashMap = new HashMap<>();
	}

	@Test
	void set() {
		hashMap.set(123, 12305);

		boolean found = false;
		for (var list : hashMap.hashTable) {
			if (list.contains(new Pair<>(123, 12305)))
				found = true;
		}

		assertTrue(found);
	}

	@Test
	void setWhenAlreadyExists() {
		hashMap.set(123, 12305);
		assertDoesNotThrow(() -> hashMap.set(123, 154));

		boolean foundFirst = false;
		boolean foundSecond = false;
		for (var list : hashMap.hashTable) {
			if (list.contains(new Pair<>(123, 12305))) {
				foundFirst = true;
			} else if (list.contains(new Pair<>(123, 154))) {
				foundSecond = true;
			}
		}

		assertTrue(foundFirst && !foundSecond);
	}


	@Test
	void getKeyExists() {
		hashMap.set(123, 12305);

		assertEquals(12305, hashMap.get(123));
	}

	@Test
	void getKetDoesNotExist() {
		assertNull(hashMap.get(123));
	}

	@Test
	void containsKeyExpectTrue() {
		hashMap.set(123, 12305);

		assertTrue(hashMap.containsKey(123));
	}

	@Test
	void containsKeyExpectFalse() {
		assertFalse(hashMap.containsKey(123));
	}

	@Test
	void removeExistentKey() {
		hashMap.set(123, 12305);
		hashMap.remove(123);

		assertFalse(hashMap.containsKey(123));
	}

	@Test
	void removeNonExistentKey() {
		assertDoesNotThrow(() -> hashMap.remove(123));
	}

	@Test
	void size() {
		assertEquals(hashMap.size, hashMap.size());
	}

	@Test
	void testRebalance() {
		ArrayList<Pair<Integer, Integer>> pairArray = new ArrayList<>();
		int initialSize = hashMap.INITIAL_SIZE;

		Random random = new Random();

		while (pairArray.size() < hashMap.INITIAL_SIZE * hashMap.MAX_BUCKET_SIZE) {
			Pair<Integer, Integer> pair = new Pair<>(random.nextInt(), random.nextInt());

			if (hashMap.containsKey(pair.first()))
				continue;
			hashMap.set(pair.first(), pair.second());
			pairArray.add(pair);
		}

		for (var pair : pairArray) {
			assertEquals(pair.second(), hashMap.get(pair.first()));
		}
	}
}