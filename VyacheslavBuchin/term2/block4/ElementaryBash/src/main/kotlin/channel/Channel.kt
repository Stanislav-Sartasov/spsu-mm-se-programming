package channel

interface Channel<T> {
	fun write(value: T)
	fun read(): T
}