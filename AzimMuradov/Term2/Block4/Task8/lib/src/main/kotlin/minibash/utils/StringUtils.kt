package minibash.utils

object StringUtils {

    fun String.ln() = "$this${System.lineSeparator()}"

    fun createErrorMessage(cmd: String, e: Throwable) =
        "$cmd: ${e::class.simpleName}${if (e.message != null) " \"${e.message}\"" else ""}".ln()

    fun createTooManyArgumentsErrorMessage(cmd: String) = "$cmd: too many arguments".ln()

    fun createNoArgumentsErrorMessage(cmd: String) = "$cmd: no arguments".ln()
}
