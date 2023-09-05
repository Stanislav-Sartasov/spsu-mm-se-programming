
fun main(args: Array<String>) {
    var start: Long = 0
    val now = System::currentTimeMillis
    val time = { now() - start }

    start = now()
    ThreadPool(12u).use { tp ->
        repeat(42) {
            tp.execute {
                Thread.sleep(500);
                println("[${time()}] $it done")
            }
        }
    }
}
