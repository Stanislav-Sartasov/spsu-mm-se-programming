package com.example

import EXAM_SYSTEM
import collections.StripedCuckooHashSet
import examsystem.ExamSystemImpl
import io.ktor.client.request.*
import io.ktor.client.statement.*
import io.ktor.http.*
import io.ktor.server.testing.*
import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test
import plugins.configureRouting


class ApplicationTest {
    @BeforeEach
    fun setUp() {
        EXAM_SYSTEM = ExamSystemImpl(StripedCuckooHashSet())
    }

    @Test
    fun count() = testExamSystem {
        repeat(5) { n ->
            client.get("/count").let { response ->
                assertEquals(HttpStatusCode.OK, response.status)
                assertEquals(n.toString(), response.bodyAsText())
            }
            EXAM_SYSTEM.add(n.toLong(), n.toLong())
        }
    }

    @Test
    fun get() = testExamSystem {
        repeat(5) { EXAM_SYSTEM.add(it.toLong(), it.toLong()) }

        getCredit(1, 1).let { response ->
            assertEquals(HttpStatusCode.OK, response.status)
            assertEquals("true", response.bodyAsText())
        }
        getCredit(1, 2).let { response ->
            assertEquals(HttpStatusCode.OK, response.status)
            assertEquals("false", response.bodyAsText())
        }

        assertEquals(HttpStatusCode.BadRequest, client.get("/").status)
    }

    @Test
    fun post() = testExamSystem {
        assertEquals(HttpStatusCode.OK, postCredit(42, 42).status)
        assert(EXAM_SYSTEM.contains(42, 42))
        assertEquals(HttpStatusCode.BadRequest, client.post("/").status)
    }

    @Test
    fun delete() = testExamSystem {
        assertEquals(HttpStatusCode.OK, deleteCredit(1, 1).status)
        assert(!EXAM_SYSTEM.contains(1, 1))
        assertEquals(HttpStatusCode.BadRequest, client.delete("/").status)
    }

    companion object {
        private fun testExamSystem(block: suspend ApplicationTestBuilder.() -> Unit): Unit =
            testApplication {
                application { configureRouting() }
                block()
            }

        private suspend fun ApplicationTestBuilder.getCredit(sid: Int, cid: Int) = client.get("/") {
            url {
                parameters.append("studentId", "$sid")
                parameters.append("courseId", "$cid")
            }
        }

        private suspend fun ApplicationTestBuilder.postCredit(sid: Int, cid: Int) = client.post("/") {
            url {
                parameters.append("studentId", "$sid")
                parameters.append("courseId", "$cid")
            }
        }

        private suspend fun ApplicationTestBuilder.deleteCredit(sid: Int, cid: Int) = client.delete("/") {
            url {
                parameters.append("studentId", "$sid")
                parameters.append("courseId", "$cid")
            }
        }
    }
}
