package channels.app

import channels.MonitoredStore
import channels.StoreConsumer
import channels.StoreProducer
import kotlin.random.Random
import kotlin.random.nextLong


fun main(args: Array<String>) {
    require(args.size == 2) { "Wrong number of argument, expected 2" }

    val parsedArgs = args.mapNotNull { it.toUByteOrNull() }

    require(parsedArgs.size == 2) {
        "Wrong format, expected: `<NUM_OF_PRODUCERS> <NUM_OF_CONSUMERS>`, where each of them is in 0..255"
    }

    val (pCount, cCount) = parsedArgs


    val store = MonitoredStore { store ->
        repeat(times = pCount.toInt()) { i ->
            store += StoreProducer(store, (0 until 10).asSequence().map {
                "Producer #$i: message #$it".also {
                    Thread.sleep(Random.nextLong(200L..500L))
                }
            })
        }
        repeat(times = cCount.toInt()) { i ->
            store += StoreConsumer(store) { messages ->
                messages.forEach {
                    println("Consumer #$i: $it")
                    Thread.sleep(Random.nextLong(200L..500L))
                }
            }
        }
    }

    readln() // Wait for user interaction.

    store.stop()
}
