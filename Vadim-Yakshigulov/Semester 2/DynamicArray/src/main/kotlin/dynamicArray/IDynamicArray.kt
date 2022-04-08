package dynamicArray

interface IDynamicArray<T> : Iterable<T> {
    val size: Int
    fun add(value: T)
    fun addAt(index: Int, value: T)
    fun removeAt(index: Int): T
    fun removeLast(): T
    fun find(value: T): Int
    operator fun get(index: Int): T
    operator fun set(index: Int, value: T)
}