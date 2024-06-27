package monitors

import java.util.*
import kotlin.concurrent.thread


fun main(args: Array<String>) {
    require(args.size == 2) { "Wrong number of arguments, expected 2" }

    val parsedArgs = args.map { it.toInt() }
    val (producerCount, consumerCount) = parsedArgs

    val synchronizedList = SynchronizedList<Int>()

    val producers = List(producerCount) { Producer(synchronizedList) }
    val consumers = List(consumerCount) { Consumer(synchronizedList) }

    val threads = mutableListOf<Thread>()

    threads += producers.map { producer ->
        thread {
            try{
                val random = Random()
                while (true) {
                    val item = random.nextInt(100) // Adjust the item creation as needed
                    producer.produce(item)
                    Thread.sleep(random.nextInt(1000).toLong()) // Introduce a random pause
                }
            } catch (e: InterruptedException){
                println("Producer stopped")
            }

        }
    }

    threads += consumers.map { consumer ->
        thread {
            try {
                val random = Random()
                while (true) {
                    consumer.consume()
                    Thread.sleep(random.nextInt(1000).toLong()) // Introduce a random pause
                }
            } catch (e: InterruptedException) {
                println("Consumer stopped")
            }
        }
    }

    println("Press Enter to exit...")
    readLine()

    // Interrupt producer and consumer threads gracefully unlike `Thread.stop()`
    // (methods like `sleep()`, `wait()`, `notify()` will throw `InterruptedException`)
    threads.forEach { it.interrupt() }

    threads.forEach { it.join() }
}
