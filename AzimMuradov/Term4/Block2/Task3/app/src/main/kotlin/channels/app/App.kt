package channels.app

import channels.Store
import channels.StoreConsumer
import channels.StoreProducer


fun main() {
    val store = Store { store ->
        repeat(times = 50) { i ->
            StoreProducer(store, sequence {
                repeat(times = 7) {
                    yield(value = "Producer #$i: message #$it")
                }
            })
        }
        repeat(times = 50) { i ->
            StoreConsumer<String>(store) { seq ->
                for (message in seq) {
                    println("Consumer #$i: $message")
                }
            }
        }
    }

    readln() // Wait for user interaction.

    store.stop()
}
