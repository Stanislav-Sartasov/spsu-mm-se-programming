package channels

import kotlin.random.Random
import kotlin.random.nextLong


class StoreConsumer<T>(
    private val store: Store<T>,
    private val consumer: (Sequence<T>) -> Unit,
) : Consumer {

    init {
        store += this
    }


    override fun consume() = consumer(
        generateSequence {
            if (store.isRunning) {
                var product: T?
                do {
                    product = store.poll()
                    Thread.sleep(1) // Helps to avoid hogging the CPU.
                } while (store.isRunning && product == null)
                product.also {
                    Thread.sleep(Random.nextLong(10L..1000L))
                }
            } else {
                null
            }
        }
    )
}
