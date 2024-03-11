package threadpool

fun main(args: Array<String>) {

    require(args.size == 1) { "Wrong number of arguments, expected 1" }

    val parsedArgs = args.map { it.toInt() }
    val numberOfThreads = parsedArgs[0]

    val threadPool = ThreadPool(numberOfThreads)

    for (i in 1..20) {
        val task = Runnable { println("Task $i executed by ${Thread.currentThread().name}") }
        threadPool.execute(task)
    }

    Thread.sleep(1000) // Let the tasks execute
    threadPool.close()
}