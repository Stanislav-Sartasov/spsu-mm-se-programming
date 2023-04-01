package channels

import kotlin.random.Random
import kotlin.random.nextLong


class StoreProducer<T>(
    private val store: Store<T>,
    private val producer: Sequence<T>,
) : Producer {

    init {
        store += this
    }


    override fun produce() {
        if (!store.isRunning) return
        producer.forEach {
            store.offer(element = it)
            Thread.sleep(Random.nextLong(10L..1000L))
            if (!store.isRunning) return
        }
    }
}
