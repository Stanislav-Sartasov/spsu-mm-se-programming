import utils.ILogger

class TestLogger : ILogger {
    override fun error(from: String, message: String) {}
}
