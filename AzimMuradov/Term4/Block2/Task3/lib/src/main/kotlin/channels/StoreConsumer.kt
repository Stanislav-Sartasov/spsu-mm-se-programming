package channels

import kotlin.random.Random
import kotlin.random.nextLong


class StoreConsumer<T>(
    private val store: Store<T>,
    private val consumer: (Sequence<T>) -> Unit,
) : Consumer {

    override fun consume() = consumer(
        generateSequence {
            if (store.isRunning) {
                var product: T?
                do product = store.poll()
                while (store.isRunning && product == null)
                product.also {
                    Thread.sleep(Random.nextLong(50L..500L))
                }
            } else {
                null
            }
        }
    )
}
