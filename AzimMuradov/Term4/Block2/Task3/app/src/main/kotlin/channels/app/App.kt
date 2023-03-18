package channels.app

import channels.ConsumerImpl
import channels.ProducerImpl
import channels.Store


fun main() {
    val store = Store { store ->
        repeat(times = 50) {
            ProducerImpl(name = "Producer #$it", store = store)
        }
        repeat(times = 50) {
            ConsumerImpl(name = "Consumer #$it", store = store)
        }
    }

    readln()

    store.stop()
}
