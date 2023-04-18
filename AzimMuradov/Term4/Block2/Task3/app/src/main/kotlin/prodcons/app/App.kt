package prodcons.app

import prodcons.Consumer
import prodcons.Store
import prodcons.Producer
import kotlin.random.Random
import kotlin.random.nextLong


fun main(args: Array<String>) {
    require(args.size == 2) { "Wrong number of argument, expected 2" }

    val parsedArgs = args.mapNotNull { it.toUByteOrNull() }

    require(parsedArgs.size == 2) {
        "Wrong format, expected: `<NUM_OF_PRODUCERS> <NUM_OF_CONSUMERS>`, where each of them is in 0..255"
    }

    val (pCount, cCount) = parsedArgs


    val store = Store {
        repeat(times = pCount.toInt()) { i ->
            var cnt = 0
            +Producer {
                "Producer #$i: message #${cnt++}".also {
                    Thread.sleep(Random.nextLong(200L..500L))
                }
            }
        }
        repeat(times = cCount.toInt()) { i ->
            +Consumer { message: String ->
                println("Consumer #$i: $message")
                Thread.sleep(Random.nextLong(200L..500L))
            }
        }
    }

    readln() // Wait for user interaction.

    store.stop()
}
