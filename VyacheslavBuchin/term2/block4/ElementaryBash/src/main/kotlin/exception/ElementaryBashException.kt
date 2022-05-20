package exception

open class ElementaryBashException(
	open val name: String,
	override val message: String
) : Exception(message) {
	companion object {
		const val SYNTAX_ERROR = "syntax error"
		const val INVALID_ARGUMENTS = "invalid arguments"
		const val UNKNOWN_ERROR = "unknown error"
		const val INVALID_SUBSTITUTION = "bad substitution"
		const val UNKNOWN_COMMAND = "unknown command"
	}
}