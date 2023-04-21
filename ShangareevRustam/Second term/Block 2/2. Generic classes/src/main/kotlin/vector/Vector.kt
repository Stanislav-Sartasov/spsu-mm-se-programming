package vector

import java.util.*

class Vector<E>(reqSize: Int = 0, elem: E? = null) {
	var size: Int = reqSize
		private set
	var capacity: Int = reqSize + 1
		private set
	private var elements: Array<E?> = Array<Any?>(capacity) { elem } as Array<E?>


	operator fun get(index: Int): E? = elements[index]

	override fun toString(): String {
		return elements.slice(0 until size).map { it -> it!! }.toString()
	}

	override fun equals(other: Any?): Boolean {
		if (other == null || other !is Vector<*> || this.toList() != other.toList()) return false
		return true
	}

	override fun hashCode(): Int {
		return Objects.hash(elements.slice(0 until size).map { it!! })
	}

	fun toList(): List<E> = elements.slice(0 until size).map { it!! }

	fun begin(): Iterator<E?> = elements.slice(0 until size).iterator()

	fun empty(): Boolean = (size == 0)

	fun at(index: Int): E {
		if (index !in 0 until size) throw IndexOutOfBoundsException("Index out of vector range")
		return elements[index]!!
	}

	fun front(): E {
		if (size == 0) throw Exception("Vector is empty")
		return elements[0]!!
	}

	fun back(): E {
		if (size == 0) throw Exception("Vector is empty")
		return elements[size - 1]!!
	}

	fun reserve(newCapacity: Int) {
		if (newCapacity <= capacity) return
		elements += arrayOfNulls<Any?>(newCapacity - capacity) as Array<E?>
		capacity = newCapacity
	}

	fun resize(newSize: Int, elem: E) {
		if (newSize > capacity) reserve(newSize)
		for (i in size until newSize) elements[i] = elem
		size = newSize
	}

	fun pushBack(elem: E) {
		if (capacity == size) reserve(2 * capacity)

		elements[size] = elem
		++size
	}

	fun popBack() {
		if (size == 0) throw Exception("Vector is empty")
		--size
	}

	fun find(elem: E): Int {
		for (i in 0 until size) {
			if (elements[i] == elem) return i
		}
		return -1
	}

	fun clear() {
		size = 0
		capacity = 1
		elements = arrayOf<Any?>(null) as Array<E?>
	}
}

fun <E> vectorOf(vararg args: E): Vector<E> {
	val res = Vector<E>()
	for (elem in args) res.pushBack(elem)
	return res
}