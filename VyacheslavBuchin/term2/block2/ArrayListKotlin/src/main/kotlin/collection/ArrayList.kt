package collection

class ArrayList<T> : AbstractList<T>() {


	private val initCapacity = 10
	private val loadFactor = 2
	private var data: Array<Any?> = Array(initCapacity) {}
	private var capacity = initCapacity
	private var usedCapacity = 0

	override val size: Int
		get() = usedCapacity

	private fun data(index: Int): T {
		return data[index] as T
	}

	override fun isEmpty(): Boolean {
		return size == 0
	}

	override operator fun contains(element: T): Boolean {
		return indexOf(element) != -1
	}

	fun add(element: T) {
		add(size, element)
	}

	fun add(index: Int, element: T) {
		checkIndexInBoundsWithSize(index)
		shiftRight(index, 1)
		data[index] = element
	}

	override fun get(index: Int): T {
		checkIndexInBounds(index)
		return data(index)
	}

	fun remove(index: Int): T {
		checkIndexInBoundsWithSize(index)
		val value = data(index)
		shiftRight(index + 1, -1)
		return value
	}

	fun remove(o: Any?): Boolean {
		val index = indexOf(o)
		if (index != -1) remove(index)
		return index != -1
	}

	operator fun set(index: Int, element: T): T {
		checkIndexInBounds(index)
		val oldValue = get(index)
		data[index] = element
		return oldValue
	}



	private fun shiftRight(index: Int, shiftSize: Int) {
		val copyRange = data.copyOfRange(index, size)
		updateSize(size + shiftSize)
		for (i in copyRange.indices) {
			data[index + shiftSize + i] = copyRange[i]
		}
	}

	private fun updateSize(newSize: Int) {
		while (newSize > capacity) grow()
		usedCapacity = newSize
	}

	private fun grow() {
		capacity *= loadFactor
		data = data.copyOf(capacity)
	}

	private fun checkIndexInBoundsWithSize(index: Int) {
		if (index < 0 || index > size) throw IndexOutOfBoundsException(outOfBoundsMessage(index))
	}

	private fun checkIndexInBounds(index: Int) {
		if (index < 0 || index >= size) throw IndexOutOfBoundsException(outOfBoundsMessage(index))
	}

	private fun outOfBoundsMessage(index: Int): String {
		return "Index: $index, Size: $size"
	}
}