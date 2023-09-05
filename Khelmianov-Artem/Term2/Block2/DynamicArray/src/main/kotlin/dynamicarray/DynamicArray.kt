package dynamicarray

class DynamicArray<T> {
    var size = 0
        get() = field
        private set
    private var capacity = 0
    private var data = emptyArray<Any?>()  //arrayOfNulls<Any?>(capacity)

    private fun resize() {
        capacity = if (capacity != 0) capacity * 2 else 4
        data = data.copyInto(arrayOfNulls<Any?>(capacity))
    }

    private fun checkIndex(index: Int) {
        require(index in 0 until size) { "Index out of range" }
    }

    @Suppress("UNCHECKED_CAST")
    operator fun get(index: Int): T {
        checkIndex(index)
        return data[index] as T
    }

    operator fun set(index: Int, item: T) {
        checkIndex(index)
        data[index] = item
        data.lastIndex
    }

    fun add(item: T, index: Int = size) {
        require(index in 0..size) { "Index out of range" }
        if (size == capacity) {
            resize()
        }
        data.copyInto(data, index + 1, index, size)
        data[index] = item
        size++
    }

    fun remove(index: Int) {
        checkIndex(index)
        data.copyInto(data, index, index + 1, size)
        size--
    }

    fun indexOf(item: T): Int = data.indexOf(item)

    fun isEmpty(): Boolean = size == 0

    fun isNotEmpty(): Boolean = !isEmpty()

    fun contains(item: T): Boolean = data.contains(item)
}












