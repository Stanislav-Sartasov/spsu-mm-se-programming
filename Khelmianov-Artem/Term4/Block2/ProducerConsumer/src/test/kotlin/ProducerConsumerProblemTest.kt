import org.junit.jupiter.api.Test
import java.util.concurrent.locks.ReentrantLock

class ProducerConsumerProblemTest {
    private fun <T, C : Actor<T>> createAndRun(actor: Class<C>, list: MutableList<T>) {
        val a = actor.getConstructor(MutableList::class.java, ReentrantLock::class.java, String::class.java)
            .newInstance(list, ReentrantLock(), "test")
        val t = Thread(a).apply { start() }
        Thread.sleep(100)
        a.isRunning = false
        t.join()
    }

    @Test
    fun producer() {
        val list = mutableListOf<Data>()
        createAndRun(Producer::class.java, list)
        assert(list.isNotEmpty())
    }

    @Test
    fun consumer() {
        val list = mutableListOf(Data("test"))
        createAndRun(Consumer::class.java, list)
        assert(list.isEmpty())
    }

    @Test
    fun test() {
        val pc = ProducerConsumerProblem(3, 3)
        pc.start()
        Thread.sleep(100)
        assert(pc.actors.all { it.isRunning })
        pc.stop()
        assert(pc.actors.all { !it.isRunning })
    }
}