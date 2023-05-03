package deanery.server

import deanery.ConcurrentSetExamSystem
import deanery.ExamSystem
import deanery.set.NonBlockingSet
import deanery.set.OptimisticSet
import io.ktor.server.application.*
import io.ktor.server.engine.*
import io.ktor.server.netty.*


fun main() {
    val env = System.getenv()["SET_TYPE"]

    val setType = when {
        env.isNullOrBlank() -> SetType.NonBlocking
        env == "optimistic" -> SetType.Optimistic
        env == "non-blocking" -> SetType.NonBlocking
        else -> throw IllegalArgumentException(
            "Wrong format, expected: `<SET_TYPE>`, where <SET_TYPE> is either \"optimistic\" or \"non-blocking\""
        )
    }

    examSystem = ConcurrentSetExamSystem(
        set = when (setType) {
            SetType.Optimistic -> OptimisticSet()
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
