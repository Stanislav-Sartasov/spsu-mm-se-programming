package preprocessor

interface Preprocessor {
	fun applySubstitutions(str: String): String
}