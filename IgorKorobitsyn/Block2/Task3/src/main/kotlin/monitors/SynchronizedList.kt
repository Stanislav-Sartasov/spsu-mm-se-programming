package monitors

import java.util.Objects

class SynchronizedList<T>{
    private val list: MutableList<T> = mutableListOf()

    // Kotlin's `Any` doesn't have `wait()` and `notify()` methods
    @Suppress("PLATFORM_CLASS_MAPPED_TO_KOTLIN")
    private val monitor: Object = Object()

    fun write(value: T) {
        synchronized(monitor) {
            list.add(value)
            monitor.notify()
        }
    }

    fun read(): T {
        synchronized(monitor) {
            while (list.isEmpty()) {
                monitor.wait()
            }
            val value = list.removeAt(0)
            return value
        }
    }
}