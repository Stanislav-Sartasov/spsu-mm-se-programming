import java.util.concurrent.locks.ReentrantLock

abstract class Actor<T>(
    protected val queue: MutableList<T>,
    protected val mutex: ReentrantLock,
    protected val name: String = ""
) : Runnable {
    @Volatile
    var isRunning: Boolean = false
}