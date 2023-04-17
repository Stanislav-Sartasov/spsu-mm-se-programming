import java.util.concurrent.Semaphore

abstract class Actor<T>(
    protected val queue: MutableList<T>,
    protected val mutex: Semaphore,
    protected val name: String = ""
) : Runnable {
    @Volatile
    var isRunning: Boolean = false
}