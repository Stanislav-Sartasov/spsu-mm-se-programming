package tokenizer

interface Tokenizer {
	fun tokenize(str: String): List<String>
}