package plugins

import EXAM_SYSTEM
import examsystem.Credit
import io.ktor.http.*
import io.ktor.server.application.*
import io.ktor.server.request.*
import io.ktor.server.response.*
import io.ktor.server.routing.*
import io.ktor.util.pipeline.*

fun Application.configureRouting() {
    routing {
        route("/") {
            get {
                val (studentId, courseId) = call.request.getCredit()
                    ?: return@get badRequest(badRequestMessage)
                call.respond(EXAM_SYSTEM.contains(studentId, courseId).toString())
            }
            post {
                val (studentId, courseId) = call.request.getCredit()
                    ?: return@post badRequest(badRequestMessage)
                EXAM_SYSTEM.add(studentId, courseId)
                call.respond(HttpStatusCode.OK)
            }
            delete {
                val (studentId, courseId) = call.request.getCredit()
                    ?: return@delete badRequest(badRequestMessage)
                EXAM_SYSTEM.remove(studentId, courseId)
                call.respond(HttpStatusCode.OK)
            }
            get("count") {
                call.respondText(EXAM_SYSTEM.count.toString())
            }
        }
    }
}

internal const val badRequestMessage = "Expected query parameters 'studentId' and 'courseId' of type Long"

internal suspend fun PipelineContext<*, ApplicationCall>.badRequest(msg: String) = call.respond(
    status = HttpStatusCode.BadRequest, message = msg
)

internal fun ApplicationRequest.getCredit(): Credit? {
    return this.queryParameters["studentId"]?.toLongOrNull()?.let { student ->
        this.queryParameters["courseId"]?.toLongOrNull()?.let { course ->
            Credit(student, course)
        }
    }
}
