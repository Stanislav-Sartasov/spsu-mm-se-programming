package channels.app

import channels.Store
import channels.StoreConsumer
import channels.StoreProducer


fun main() {
    val store = Store<String> { store ->
        repeat(times = 50) { i ->
            StoreProducer(
                store,
                producer = (0 until 7).asSequence().map { "Producer #$i: message #$it" }
            )
        }
        repeat(times = 50) { i ->
            StoreConsumer(store) { products ->
                products.forEach { println("Consumer #$i: $it") }
            }
        }
    }

    readln() // Wait for user interaction.

    store.stop()
}
