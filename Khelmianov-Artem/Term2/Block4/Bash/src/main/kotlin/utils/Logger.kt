package utils

interface ILogger {
    fun error(from: String, message: String)
}

class Logger : ILogger {
    override fun error(from: String, message: String) = System.err.println("[$from] Error: $message")
}
