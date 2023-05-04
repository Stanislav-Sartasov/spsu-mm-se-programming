package deanery.server

import deanery.CourseId
import deanery.StudentId
import deanery.server.ValidationResult.Invalid
import deanery.server.ValidationResult.Valid
import io.ktor.http.*
import io.ktor.server.application.*
import io.ktor.server.request.*
import io.ktor.server.response.*
import io.ktor.server.routing.*
import io.ktor.util.pipeline.*


fun Application.configureRouting() {
    routing {
        route("/exam") {
            get {
                val (studentId, courseId) = when (val result = call.request.params) {
                    is Valid -> result
                    is Invalid -> return@get respondBadRequest(result)
                }
                call.respondText(text = examSystem.contains(studentId, courseId).toString())
            }

            post {
                val (studentId, courseId) = when (val result = call.request.params) {
                    is Valid -> result
                    is Invalid -> return@post respondBadRequest(result)
                }
                examSystem.add(studentId, courseId)
                call.respond(HttpStatusCode.OK)
            }

            delete {
                val (studentId, courseId) = when (val result = call.request.params) {
                    is Valid -> result
                    is Invalid -> return@delete respondBadRequest(result)
                }
                examSystem.remove(studentId, courseId)
                call.respond(HttpStatusCode.OK)
            }
        }

        route("/exams/count") {
            get {
                call.respondText(text = examSystem.count.toString())
            }
        }
    }
}


private suspend fun PipelineContext<*, ApplicationCall>.respondBadRequest(invalidResult: Invalid) =
    call.respondText(invalidResult.message, status = HttpStatusCode.BadRequest)

private sealed interface ValidationResult {
    data class Valid(val studentId: StudentId, val courseId: CourseId) : ValidationResult
    data class Invalid(val message: String) : ValidationResult
}

private val ApplicationRequest.params: ValidationResult
    get(): ValidationResult {
        val studentId = (queryParameters["student_id"] ?: return Invalid("Missing `student_id`"))
            .toLongOrNull() ?: return Invalid("Wrong format of `student_id`")
        val courseId = (queryParameters["course_id"] ?: return Invalid("Missing `course_id`"))
            .toLongOrNull() ?: return Invalid("Wrong format of `course_id`")

        return Valid(studentId, courseId)
    }
