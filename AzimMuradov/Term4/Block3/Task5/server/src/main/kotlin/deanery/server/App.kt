package deanery.server

import deanery.ConcurrentSetExamSystem
import deanery.ExamSystem
import deanery.set.NonBlockingSet
import deanery.set.LazySet
import io.ktor.server.application.*
import io.ktor.server.engine.*
import io.ktor.server.netty.*


fun main() {
    val env = System.getenv()["SET_TYPE"]

    val setType = when {
        env.isNullOrBlank() -> SetType.Lazy
        env == "lazy" -> SetType.Lazy
        env == "non-blocking" -> SetType.NonBlocking
        else -> throw IllegalArgumentException(
            "Wrong format, expected: `<SET_TYPE>`, where <SET_TYPE> is either \"lazy\" or \"non-blocking\""
        )
    }

    examSystem = ConcurrentSetExamSystem(
        set = when (setType) {
            SetType.Lazy -> LazySet()
            SetType.NonBlocking -> NonBlockingSet()
        }
    )

    embeddedServer(
        factory = Netty,
        port = 8080,
        host = "0.0.0.0",
        module = Application::module
    ).start(wait = true)
}

lateinit var examSystem: ExamSystem

fun Application.module() = configureRouting()
