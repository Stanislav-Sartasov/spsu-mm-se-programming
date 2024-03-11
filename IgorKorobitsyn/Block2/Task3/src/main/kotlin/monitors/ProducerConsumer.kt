package monitors

class Producer<T>(private val list: SynchronizedList<T>) {
    fun produce(item: T) {
        list.write(item)
        println("Produced: $item")
    }
}

class Consumer<T>(private val list: SynchronizedList<T>) {
    fun consume(): T {
        val item = list.read()
        println("Produced: $item")
        return item
    }
}