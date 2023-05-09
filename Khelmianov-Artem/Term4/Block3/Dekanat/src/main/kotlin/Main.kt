import collections.LockFreeListSet
import collections.StripedCuckooHashSet
import examsystem.Credit
import examsystem.ExamSystem
import examsystem.ExamSystemImpl
import io.ktor.server.application.*
import io.ktor.server.engine.*
import io.ktor.server.netty.*
import plugins.configureRouting
import java.io.File

lateinit var EXAM_SYSTEM: ExamSystem

fun main() {
    val sets = mapOf(
        "cuckoo" to StripedCuckooHashSet<Credit>(),
        "lock-free" to LockFreeListSet<Credit>()
    )

    EXAM_SYSTEM = ExamSystemImpl(
        System.getenv()["SET"]
            ?.let { sets[it] }
            ?: sets.values.first()
    )

    embeddedServer(
        Netty,
        port = 8080,
        host = "0.0.0.0",
        module = Application::module,
    ).start(wait = true)
}

fun Application.module() {
    configureRouting()
}
