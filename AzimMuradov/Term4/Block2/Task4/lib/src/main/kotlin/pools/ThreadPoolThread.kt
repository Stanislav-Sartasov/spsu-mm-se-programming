package pools


internal class ThreadPoolThread(
    name: String,
    private val isRunning: () -> Boolean,
    private val workQueue: Queue<Runnable>,
) : Thread(name) {

    override fun run() {
        while (isRunning() || workQueue.isNotEmpty()) {
            while (true) workQueue.poll()?.run() ?: break
            sleep(1) // Helps to avoid hogging the CPU.
        }
    }
}
