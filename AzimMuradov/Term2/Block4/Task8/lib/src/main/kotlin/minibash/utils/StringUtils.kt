package minibash.utils

object StringUtils {

    fun String.ln() = "$this${System.lineSeparator()}"

    fun createErrorMessage(name: String, e: Throwable) =
        "$name: ${e::class.simpleName}${e.message?.let { " \"$it\"" } ?: ""}".ln()

    fun createTooManyArgumentsErrorMessage(name: String) = "$name: too many arguments".ln()

    fun createNoArgumentsErrorMessage(name: String) = "$name: no arguments".ln()
}
