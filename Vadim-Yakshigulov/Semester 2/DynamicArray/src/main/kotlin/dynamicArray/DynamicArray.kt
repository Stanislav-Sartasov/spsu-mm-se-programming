package dynamicArray

import java.lang.Integer.max
import java.lang.Integer.min

class DynamicArray<T> private constructor(initialSize: Int, vararg values: Any?) : IDynamicArray<T> {
    override var size = initialSize
        private set

    private var maxSize = max(initialSize * 2, 16)

    override fun add(value: T) {
        addAt(size, value)
    }

    @Suppress("UNCHECKED_CAST")
    override fun removeAt(index: Int): T {
        val item = array[index]
        shiftLeft(array, index)
        size--
        return item as T
    }

    override fun find(value: T): Int {
        for (i in 0 until size)
            if (this[i] == value)
                return i
        return -1
    }
    override fun removeLast(): T {
        return removeAt(size - 1)
    }

    override fun addAt(index: Int, value: T) {
        shiftRight(array, index)
        array[index] = value
        size++

        if (size == maxSize)
            resize(maxSize * 2)
    }

    @Suppress("UNCHECKED_CAST")
    override operator fun get(index: Int): T {
        if (index in 0 until size)
            return array[index] as T

        throw IndexOutOfBoundsException()
    }

    override operator fun set(index: Int, value: T) {
        if (index in 0 until size)
            array[index] = value
        else
            throw IndexOutOfBoundsException()
    }

    override fun iterator(): Iterator<T> {
        return object : Iterator<T> {
            var i = 0;
            override fun hasNext(): Boolean = i < size

            @Suppress("UNCHECKED_CAST")
            override fun next(): T = array[i++] as T
        }
    }

    override fun toString(): String {
        return array.slice(0 until size).toString()
    }


    constructor(vararg values: T) : this(values.size, *values)

    private var array: Array<in Any?> =
        if (values.isEmpty())
            arrayOfNulls(maxSize)
        else
            arrayCopy(values, arrayOfNulls(maxSize))

    private fun resize(newSize: Int) {
        maxSize = newSize
        array = arrayCopy(array, arrayOfNulls(maxSize))
    }

    private fun arrayCopy(from: Array<out Any?>, to: Array<Any?>): Array<Any?> {
        val len = min(from.size, to.size)
        for (i in 0 until len) {
            to[i] = from[i]
        }
        return to
    }

    private fun shiftRight(array: Array<Any?>, fromIndex: Int, toIndex: Int = array.size - 1) {
        for (i in toIndex downTo fromIndex + 1) {
            array[i] = array[i - 1]
        }
    }

    private fun shiftLeft(array: Array<Any?>, fromIndex: Int, toIndex: Int = array.size - 1) {
        for (i in fromIndex until toIndex) {
            array[i] = array[i + 1]
        }
    }

    override fun equals(other: Any?): Boolean {
        if (this === other) return true
        if (javaClass != other?.javaClass) return false

        other as DynamicArray<*>

        if (size != other.size) return false

        for (i in 0 until size) {
            if (other[i] != this[i])
                return false
        }

        return true
    }

    override fun hashCode(): Int {
        var result = size
        result = 31 * result + maxSize.hashCode()
        result = 31 * result + array.contentHashCode()
        return result
    }
}