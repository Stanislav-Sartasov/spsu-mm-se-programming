package channels.app

import channels.MonitoredStore
import channels.StoreConsumer
import channels.StoreProducer


fun main() {
    val store = MonitoredStore<String> { store ->
        repeat(times = 50) { i ->
            store += StoreProducer(
                store,
                producer = (0 until 7).asSequence().map { "Producer #$i: message #$it" }
            )
        }
        repeat(times = 50) { i ->
            store += StoreConsumer(store) { products ->
                products.forEach { println("Consumer #$i: $it") }
            }
        }
    }

    readln() // Wait for user interaction.

    store.stop()
}
