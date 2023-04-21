import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class PairTest {
	@Test
	void first() {
		Pair<Integer, Integer> pair = new Pair<>(1, 123);

		assertEquals(pair.first(), 1);
	}

	@Test
	void second() {
		Pair<Integer, Integer> pair = new Pair<>(1, 123);

		assertEquals(pair.second(), 123);
	}

	@Test
	void testEqualsIntegersExpectTrue() {
		Pair<Integer, Integer> firstPair = new Pair<>(1, 2);
		Pair<Integer, Integer> secondPair = new Pair<>(1, 2);

		assertTrue(firstPair.equals(secondPair));
	}

	@Test
	void testEqualsIntegersExpectFalse() {
		Pair<Integer, Integer> firstPair = new Pair<>(1, 2);
		Pair<Integer, Integer> secondPair = new Pair<>(1, 3);

		assertFalse(firstPair.equals(secondPair));
	}

	@Test
	void testEqualsStringsExpectTrue() {
		Pair<String, String> firstPair = new Pair<>("Vladimir", "Putin");
		Pair<String, String> secondPair = new Pair<>("Vladimir", "Putin");

		assertTrue(firstPair.equals(secondPair));
	}

	@Test
	void testEqualsStringsExpectFalse() {
		Pair<String, String> firstPair = new Pair<>("Vladimir", "Putin");
		Pair<String, String> secondPair = new Pair<>("Vladimir", "Vladimirovich");

		assertFalse(firstPair.equals(secondPair));
	}

	@Test
	void testEqualsIntegersAndStringsExpectTrue() {
		Pair<Integer, String> firstPair = new Pair<>(1, "Putin");
		Pair<Integer, String> secondPair = new Pair<>(1, "Putin");

		assertTrue(firstPair.equals(secondPair));
	}

	@Test
	void testEqualsIntegersAndStringsExpectFalse() {
		Pair<Integer, String> firstPair = new Pair<>(1, "Putin");
		Pair<Integer, String> secondPair = new Pair<>(2, "Putin");

		assertFalse(firstPair.equals(secondPair));
	}

	@Test
	void testEqualsWithDifferentTypesExpectFalse() {
		Pair<Integer, String> firstPair = new Pair<>(1, "Putin");
		String second = "Putin";

		assertFalse(firstPair.equals(second));
	}
}