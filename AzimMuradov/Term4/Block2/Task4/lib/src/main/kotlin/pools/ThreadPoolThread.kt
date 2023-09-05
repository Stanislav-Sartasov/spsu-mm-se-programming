package pools


internal class ThreadPoolThread(
    name: String,
    private val isRunning: () -> Boolean,
    private val workQueue: BlockingQueue<Runnable>,
) : Thread(name) {

    override fun run() {
        while (isRunning() || workQueue.isNotEmpty()) {
            workQueue.poll()?.run()
        }
    }
}
