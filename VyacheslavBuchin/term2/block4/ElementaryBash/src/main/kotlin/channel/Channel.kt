package channel

import java.io.InputStream
import java.io.OutputStream

interface Channel<T> {
	val inputStream: InputStream
	val outputStream: OutputStream

	fun write(value: T)
	fun read(): T
}