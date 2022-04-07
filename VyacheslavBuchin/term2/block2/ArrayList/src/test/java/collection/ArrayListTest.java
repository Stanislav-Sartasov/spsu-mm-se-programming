package collection;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import org.junit.jupiter.params.ParameterizedTest;
import org.junit.jupiter.params.provider.Arguments;
import org.junit.jupiter.params.provider.MethodSource;

import java.util.Arrays;
import java.util.List;
import java.util.stream.Stream;

import static org.junit.jupiter.api.Assertions.*;

class ArrayListTest {

	private ArrayList<Integer> testList;
	private java.util.ArrayList<Integer> referenceList;
	private static final int[] values = { 0, -1, 1, Integer.MAX_VALUE, Integer.MIN_VALUE };
	private static final int[] inBoundsIndexes = { 0, 50, 99 };
	private static final int[] outOfBoundsIndexes = { -1, 101, 1000 };

	private static Stream<Arguments> values() {
		return Arrays.stream(values).mapToObj(Arguments::of);
	}

	private static Stream<Arguments> inBoundsIndexes() {
		return Arrays.stream(inBoundsIndexes).mapToObj(Arguments::of);
	}

	private static Stream<Arguments> outOfBoundsIndexes() {
		return Arrays.stream(outOfBoundsIndexes).mapToObj(Arguments::of);
	}

	private static Stream<Arguments> inBoundsIndexedValues() {
		var argList = new java.util.ArrayList<Arguments>();
		for (var index : inBoundsIndexes) {
			for (var value : values) {
				argList.add(Arguments.of(index, value));
			}
		}
		return argList.stream();
	}

	private static Stream<Arguments> outOfBoundsIndexedValues() {
		var a = new java.util.ArrayList<Arguments>();
		for (var index : outOfBoundsIndexes) {
			for (var value : values) {
				a.add(Arguments.of(index, value));
			}
		}
		return a.stream();
	}

	private static Stream<Arguments> rangesInBounds() {
		return Stream.of(
				Arguments.of(0, 100),
				Arguments.of(5, 15)
		);
	}

	private static Stream<Arguments> rangesOutOfBounds() {
		return Stream.of(
				Arguments.of(0, 115),
				Arguments.of(-3, 10),
				Arguments.of(228, 1337)
		);
	}

	@BeforeEach
	void setUp() {
		testList = new ArrayList<>();
		referenceList = new java.util.ArrayList<>();
		for (int i = 0; i < 100; i++) {
			testList.add(i);
			referenceList.add(i);
		}
	}

	@Test
	void sizeTest() {
		assertEquals(referenceList.size(), testList.size());
	}

	@Test
	void isEmptyTest() {
		assertEquals(referenceList.isEmpty(), testList.isEmpty());
	}

	@ParameterizedTest
	@MethodSource("values")
	void containsTest(int value) {
		assertEquals(referenceList.contains(value), testList.contains(value));
	}

	@Test
	void toArrayTest() {
		assertArrayEquals(referenceList.toArray(), testList.toArray());
	}

	@ParameterizedTest
	@MethodSource("inBoundsIndexedValues")
	void addTestWithIndexesInBounds(int index, int value) {
		referenceList.add(index, value);
		testList.add(index, value);
		assertArrayEquals(referenceList.toArray(), testList.toArray());
	}

	@ParameterizedTest
	@MethodSource("outOfBoundsIndexedValues")
	void addTestWithIndexesOutOfBounds(int index, int value) {
		assertThrows(IndexOutOfBoundsException.class, () -> referenceList.add(index, value));
		assertThrows(IndexOutOfBoundsException.class, () -> testList.add(index, value));
	}

	@ParameterizedTest
	@MethodSource("inBoundsIndexes")
	void removeInBoundsIndexedTest(int index) {
		referenceList.remove(index);
		testList.remove(index);
		assertArrayEquals(referenceList.toArray(), testList.toArray());
	}

	@ParameterizedTest
	@MethodSource("outOfBoundsIndexes")
	void removeOutOfIndexedTest(int index) {
		assertThrows(IndexOutOfBoundsException.class, () -> referenceList.remove(index));
		assertThrows(IndexOutOfBoundsException.class, () -> testList.remove(index));
	}

	@ParameterizedTest
	@MethodSource("values")
	void removeObjectTest(int value) {
		assertEquals(referenceList.remove((Object) value), testList.remove((Object) value));
		assertArrayEquals(referenceList.toArray(), testList.toArray());
	}

	@Test
	void containsAllTest() {
		assertTrue(testList.containsAll(testList));
		assertTrue(testList.containsAll(referenceList));
		assertTrue(referenceList.containsAll(testList));
	}

	@Test
	void retainAllTest() {
		var tempArray = testList.toArray();
		testList.retainAll(referenceList);
		assertArrayEquals(tempArray, testList.toArray());
	}

	@ParameterizedTest
	@MethodSource("inBoundsIndexes")
	void getInBoundsTest(int index) {
		assertEquals(referenceList.get(index), testList.get(index));
	}

	@ParameterizedTest
	@MethodSource("outOfBoundsIndexes")
	void getOutOfBoundsTest(int index) {
		assertThrows(IndexOutOfBoundsException.class, () -> referenceList.get(index));
		assertThrows(IndexOutOfBoundsException.class, () -> testList.get(index));
	}

	@ParameterizedTest
	@MethodSource("inBoundsIndexedValues")
	void setInBoundsTest(int index, int value) {
		referenceList.set(index, value);
		testList.set(index, value);
		assertArrayEquals(referenceList.toArray(), testList.toArray());
	}

	@ParameterizedTest
	@MethodSource("outOfBoundsIndexedValues")
	void setOutOfBoundsTest(int index, int value) {
		assertThrows(IndexOutOfBoundsException.class, () -> referenceList.set(index, value));
		assertThrows(IndexOutOfBoundsException.class, () -> testList.set(index, value));
	}

	@Test
	void addAllTest() {
		var appendingList = List.of(228, 1337, 5553535);
		referenceList.addAll(appendingList);
		testList.addAll(appendingList);
		assertArrayEquals(referenceList.toArray(), testList.toArray());
	}

	@Test
	void removeAllTest() {
		var removingList = List.of(228, 1337, 5553535, 1, 2, 3, 99);
		referenceList.removeAll(removingList);
		testList.removeAll(removingList);
		assertArrayEquals(referenceList.toArray(), testList.toArray());
	}

	@Test
	void toArrayTypedTest() {
		var referenceArray = new Number[referenceList.size()];
		var testArray = new Number[testList.size()];
		referenceList.toArray(referenceArray);
		testList.toArray(testArray);
		assertArrayEquals(referenceArray, testArray);
	}

	@ParameterizedTest
	@MethodSource("rangesInBounds")
	void subListInBoundsTest(int from, int to) {
		var referenceArray = referenceList.subList(from, to).toArray();
		var testArray = testList.subList(from, to).toArray();
		assertArrayEquals(referenceArray, testArray);
	}

	@ParameterizedTest
	@MethodSource("rangesOutOfBounds")
	void subListOutOfBoundsTest(int from, int to) {
		assertThrows(
				IndexOutOfBoundsException.class,
				() -> referenceList.subList(from, to)
		);
		assertThrows(
				IndexOutOfBoundsException.class,
				() -> testList.subList(from, to)
		);
	}
}