package examsystem

import com.sun.net.httpserver.HttpExchange
import com.sun.net.httpserver.HttpServer.create
import java.io.OutputStream
import java.net.InetSocketAddress

fun main() {
    val examSystem = OptimisticExamSystem()

    val server = create(InetSocketAddress(8080), 0)
    server.createContext("/add") { handler(examSystem::Add, it) }
    server.createContext("/remove") { handler(examSystem::Remove, it) }
    server.createContext("/contains") { handler(examSystem::Contains, it) }
    server.createContext("/count") { exchange ->
        val response = examSystem.Count.toString()
        exchange.sendResponseHeaders(200, response.length.toLong())
        val os: OutputStream = exchange.responseBody
        os.write(response.toByteArray())
        os.close()
    }

    server.start()
    println("Server started")
}

private fun handler(operation: (Long, Long) -> Any, exchange: HttpExchange) {
    val queryParams = exchange.requestURI.query.split("&")
    val params = queryParams.associate { param ->
        val (key, value) = param.split("=")
        key to value.toLong()
    }

    val response = operation(params["studentId"]!!, params["courseId"]!!).toString()
    exchange.sendResponseHeaders(200, response.length.toLong())
    val os: OutputStream = exchange.responseBody
    os.write(response.toByteArray())
    os.close()
}
