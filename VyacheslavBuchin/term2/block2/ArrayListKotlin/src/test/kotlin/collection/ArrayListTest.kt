package collection

import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.api.Assertions.assertThrows
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.Arguments
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertTrue

internal class ArrayListTest {

	private val testList = ArrayList<Int>()
	private val referenceList = kotlin.collections.ArrayList<Int>()

	companion object {
		private val values = intArrayOf(0, -1, 1, Int.MAX_VALUE, Int.MIN_VALUE)
		private val inBoundsIndexes = intArrayOf(0, 50, 99)
		private val outOfBoundsIndexes = intArrayOf(-1, 101, 1000)

		@JvmStatic
		private fun values() = values.map { Arguments.of(it) }

		@JvmStatic
		private fun inBoundsIndexes() = inBoundsIndexes.map { Arguments.of(it) }

		@JvmStatic
		private fun outOfBoundsIndexes() = outOfBoundsIndexes.map { Arguments.of(it) }

		@JvmStatic
		private fun inBoundsIndexedValues(): List<Arguments> {
			val argList = mutableListOf<Arguments>()
			inBoundsIndexes.forEach { index ->
				values.forEach { value ->
					argList.add(Arguments.of(index, value))
				}
			}
			return argList
		}

		@JvmStatic
		private fun outOfBoundsIndexedValues(): List<Arguments> {
			val argList = mutableListOf<Arguments>()
			outOfBoundsIndexes.forEach { index ->
				values.forEach { value ->
					argList.add(Arguments.of(index, value))
				}
			}
			return argList
		}
	}
	@BeforeEach
	fun setUp() {
		for (i in 0..99) {
			testList.add(i)
			referenceList.add(i)
		}
	}

	@Test
	fun `size test`() {
		assertEquals(referenceList.size, testList.size)
	}

	@Test
	fun `isEmpty test`() {
		assertEquals(referenceList.isEmpty(), testList.isEmpty())
	}

	@ParameterizedTest
	@MethodSource("values")
	fun `contains test`(value: Int) {
		assertEquals(referenceList.contains(value), testList.contains(value))
	}

	@ParameterizedTest
	@MethodSource("inBoundsIndexedValues")
	fun `add should insert value in given index if index is in bounds`(index: Int, value: Int) {
		referenceList.add(index, value)
		testList.add(index, value)
		assertTrue(referenceList.isEqualTo(testList))
	}

	@ParameterizedTest
	@MethodSource("outOfBoundsIndexedValues")
	fun `add should throw exception if index is not in bounds`(index: Int, value: Int) {
		assertThrows(IndexOutOfBoundsException::class.java) { referenceList.add(index, value) }
		assertThrows(IndexOutOfBoundsException::class.java) { testList.add(index, value) }
	}

	@ParameterizedTest
	@MethodSource("inBoundsIndexes")
	fun `remove should remove value from given index if index is in bounds`(index: Int) {
		referenceList.removeAt(index)
		testList.remove(index)
		assertTrue(referenceList.isEqualTo(testList))
	}

	@ParameterizedTest
	@MethodSource("outOfBoundsIndexes")
	fun `remove should throw exception if index is not in bounds`(index: Int) {
		assertThrows(IndexOutOfBoundsException::class.java) { referenceList.removeAt(index) }
		assertThrows(IndexOutOfBoundsException::class.java) { testList.remove(index) }
	}

	@ParameterizedTest
	@MethodSource("values")
	fun `remove should remove given object if it is in list`(value: Int) {
		assertEquals(referenceList.remove(value as Any), testList.remove(value as Any))
		assertTrue(referenceList.isEqualTo(testList))
	}

	@ParameterizedTest
	@MethodSource("inBoundsIndexes")
	fun `get should return value stored in given index`(index: Int) {
		assertEquals(referenceList[index], testList[index])
	}

	@ParameterizedTest
	@MethodSource("outOfBoundsIndexes")
	fun `get should throw exception if given index is not in bounds`(index: Int) {
		assertThrows(IndexOutOfBoundsException::class.java) { referenceList[index] }
		assertThrows(IndexOutOfBoundsException::class.java) { testList[index] }
	}

	@ParameterizedTest
	@MethodSource("inBoundsIndexedValues")
	fun `set should change value by the given index`(index: Int, value: Int) {
		referenceList[index] = value
		testList[index] = value
		assertTrue(referenceList.isEqualTo(testList))
	}

	@ParameterizedTest
	@MethodSource("outOfBoundsIndexedValues")
	fun `set should throw exception if given index is not in bounds`(index: Int, value: Int) {
		assertThrows(IndexOutOfBoundsException::class.java) { referenceList[index] = value }
		assertThrows(IndexOutOfBoundsException::class.java) { testList[index] = value }
	}


}

fun kotlin.collections.ArrayList<Int>.isEqualTo(o: ArrayList<Int>): Boolean {
	this.forEachIndexed { index, element ->
		if (element != o[index])
			return false
	}
	return true
}