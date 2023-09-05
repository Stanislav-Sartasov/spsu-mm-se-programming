import co.paralleluniverse.fibers.FiberExecutorScheduler
import co.paralleluniverse.fibers.Suspendable
import co.paralleluniverse.kotlin.fiber
import fibers.MyProcess
import fibers.ProcessManager
import java.util.concurrent.Executors


val time = System::currentTimeMillis
val start = time()
fun fromStart() = time() - start

fun main(args: Array<String>) {
    val executor = Executors.newSingleThreadExecutor()
    val scheduler = FiberExecutorScheduler("Fibers", executor)

    fiber(start = false, scheduler = scheduler) @Suspendable {
        val pm = ProcessManager(scheduler, ProcessManager::withPriority)
        repeat(3) {
            pm.submit(MyProcess(it).also { p ->
                println("Process $it: priority ${p.priority}, intervals ${p.intervals}")
            })
        }
        pm.run()
    }.start().join()

    scheduler.shutdown()
    executor.shutdown()
}
