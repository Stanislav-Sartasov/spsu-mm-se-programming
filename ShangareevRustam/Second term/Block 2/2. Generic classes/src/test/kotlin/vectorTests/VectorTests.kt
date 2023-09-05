package vectorTests

import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.api.Test
import vector.*
import kotlin.random.Random
import kotlin.random.nextULong

class VectorTests {

	private fun getRandomEntitiesWithRandomType(requiredSize: Int): MutableList<Any> {
		val list = listOf("Int", "ByteArray", "Double", "ULong", "String", "MutableList<Float>")
		val randomType = list[Random.nextInt(6)]
		val result = mutableListOf<Any>()

		when (randomType) {
			"Int" -> {
				repeat(requiredSize) {
					result.add(Random.nextInt(1000))
				}
			}
			"ByteArray" -> {
				repeat(requiredSize) {
					result.add(Random.nextBytes(Random.nextInt(50)))
				}
			}
			"Double" -> {
				repeat(requiredSize) {
					result.add(Random.nextDouble(-1000.0, 1000.0))
				}
			}
			"ULong" -> {
				repeat(requiredSize) {
					result.add(Random.nextULong(10000u))
				}
			}
			"String" -> {
				repeat(requiredSize) {
					val size = Random.nextInt(10)
					var str = ""
					repeat(size) {
						str += Random.nextInt(256).toChar()
					}
					result.add(str)
				}
			}
			"MutableList<Float>" -> {
				repeat(requiredSize) {
					val size = Random.nextInt(10)
					var curList = mutableListOf<Float>()
					repeat(size) {
						curList.add(Random.nextFloat())
					}
					result.add(curList)
				}
			}
		}

		return result
	}

	private fun getListAndVec(listSize: Int): Pair<MutableList<Any>, Vector<Any>> {
		val list = getRandomEntitiesWithRandomType(listSize)
		val vec = vectorOf(*list.toTypedArray())

		return Pair(list, vec)
	}

	@Test
	fun constructorTest() {
		val elem = getRandomEntitiesWithRandomType(1)[0]
		val size = Random.nextInt(100)
		val vec = Vector(size, elem)

		for (i in 0 until size) {
			if (vec[i] != elem) assert(false)
		}
		assert(true)
	}

	@Test
	fun getTest() {
		val (list, vec) = getListAndVec(Random.nextInt(50))

		assert((0 until vec.size).all { i -> vec[i] == list[i] })
	}

	@Test
	fun toStringTest() {
		val (list, vec) = getListAndVec(Random.nextInt(50))

		assertEquals(list.toString(), vec.toString())
	}

	@Test
	fun equalsTest() {
		val (list, vec) = getListAndVec(Random.nextInt(50))

		assertEquals(vec, vectorOf(*list.toTypedArray()))
	}

	@Test
	fun notEqualTest() {
		val expectedVec = getListAndVec(Random.nextInt(10, 50)).second
		val actualVec = getListAndVec(Random.nextInt(10, 50)).second

		assert(expectedVec != actualVec)
	}

	@Test
	fun nullEqualsTest() {
		assert(
			getListAndVec(Random.nextInt(10, 50)).second != null
		)
	}

	@Test
	fun notVectorEqualsTest() {
		val actualEntity = getListAndVec(Random.nextInt(100)).first

		assert(
			getListAndVec(Random.nextInt(10, 50)).second != actualEntity
		)
	}

	@Test
	fun toListTest() {
		val (list, vec) = getListAndVec(Random.nextInt(50))

		assertEquals(list, vec.toList())
	}

	@Test
	fun iteratorTest() {
		val (expectedList, vec) = getListAndVec(Random.nextInt(50))

		val actualList = mutableListOf<Any>()
		val it = vec.begin()
		while (it.hasNext()) {
			actualList.add(it.next()!!)
		}

		assertEquals(expectedList, actualList)
	}

	@Test
	fun emptyTest() {
		val (list, vec) = getListAndVec(0)

		assert(vec.empty())
	}

	@Test
	fun notEmptyTest() {
		val (list, vec) = getListAndVec(Random.nextInt(1, 50))

		assert(!vec.empty())
	}

	@Test
	fun atTest() {
		val (list, vec) = getListAndVec(Random.nextInt(1, 50))
		val index = Random.nextInt(0, list.size)

		assertEquals(vec[index], vec.at(index))
	}

	@Test
	fun atOutOfBoundsTest() {
		val (list, vec) = getListAndVec(Random.nextInt(100))
		val index = Random.nextInt(list.size, list.size + 1000)

		var flag = false
		try {
			vec.at(index)
		} catch (e: IndexOutOfBoundsException) {
			flag = true
		}

		assert(flag)
	}

	@Test
	fun frontTest() {
		val (list, vec) = getListAndVec(Random.nextInt(1, 100))

		assert(vec[0] == list[0] && list[0] == vec.front())
	}

	@Test
	fun frontEmptyTest() {
		val (list, vec) = getListAndVec(0)

		var flag = false
		try {
			vec.front()
		} catch (e: Exception) {
			flag = true
		}

		assert(flag)
	}

	@Test
	fun backTest() {
		val (list, vec) = getListAndVec(Random.nextInt(1, 100))

		assert(vec[vec.size - 1] == list[list.size - 1] && list[list.size - 1] == vec.back())
	}

	@Test
	fun backEmptyTest() {
		val (list, vec) = getListAndVec(0)

		var flag = false
		try {
			vec.back()
		} catch (e: Exception) {
			flag = true
		}

		assert(flag)
	}

	@Test
	fun pushBackTest() {
		val list = getRandomEntitiesWithRandomType(Random.nextInt(10, 100))
		val last = list.removeLast()

		val vec = vectorOf(*list.toTypedArray())
		vec.pushBack(last)

		assertEquals(list + last, vec.toList())
	}

	@Test
	fun reserveTest() {
		val (list, vec) = getListAndVec(Random.nextInt(10, 100))
		val saveCapacity = vec.capacity

		vec.reserve(Random.nextInt(vec.capacity + 1, vec.capacity + 1000))

		var flag = (vec.capacity != saveCapacity)
		try {
			for (i in vec.size until vec.capacity) {
				vec[i]
			}
		} catch (e: Exception) {
			flag = false
		}

		assert(flag)
	}

	@Test
	fun resizeTest() {
		val list = getRandomEntitiesWithRandomType(Random.nextInt(10, 100))
		val elem = list.last()
		val vec = vectorOf(*list.toTypedArray())

		val newSize = Random.nextInt(vec.size - 10, vec.size + 100)
		for (i in 1..newSize - list.size) {
			list.add(elem)
		}

		vec.resize(newSize, elem)

		assertEquals(list.slice(0 until newSize), vec.toList())
	}

	@Test
	fun findTest() {
		val (list, vec) = getListAndVec(Random.nextInt(10, 100))

		val elem = list[Random.nextInt(list.size)]
		var index = 0
		while (list[index] != elem) ++index
		assert(index == list.indexOf(elem) && index == vec.find(elem))
	}

	@Test
	fun noElementFindTest() {
		val (list, vec) = getListAndVec(Random.nextInt(10, 100))

		val elem = getRandomEntitiesWithRandomType(1)[0]
		assert(-1 == list.indexOf(elem) && -1 == vec.find(elem))
	}

	@Test
	fun clearTest() {
		val (list, vec) = getListAndVec(Random.nextInt(10, 100))

		vec.clear()

		assertEquals(listOf<Any>(), vec.toList())
	}

	@Test
	fun popBackTest() {
		val (list, vec) = getListAndVec(Random.nextInt(1, 100))

		vec.popBack()
		list.removeLast()

		assertEquals(list, vec.toList())
	}

	@Test
	fun popBackEmptyTest() {
		val (list, vec) = getListAndVec(0)

		var flag = false
		try {
			vec.popBack()
		} catch (e: Exception) {
			flag = true
		}

		assert(flag)
	}
}