package pools

import kotlinx.atomicfu.AtomicBoolean


internal class ThreadPoolThread(
    name: String,
    private val execute: AtomicBoolean,
    private val runnables: Queue<Runnable>,
) : Thread(name) {
    override fun run() {
        try {
            // Continue to execute when the execute flag is true, or when there are runnables in the queue
            while (execute.value || runnables.isNotEmpty()) {
                // Poll a runnable from the queue and execute it
                while (true) {
                    runnables.poll()?.run() ?: break
                }
                // Sleep in case there wasn't any runnable in the queue. This helps to avoid hogging the CPU.
                sleep(1)
            }
        } catch (e: RuntimeException) {
            throw ThreadPoolException(e)
        } catch (e: InterruptedException) {
            throw ThreadPoolException(e)
        }
    }
}
