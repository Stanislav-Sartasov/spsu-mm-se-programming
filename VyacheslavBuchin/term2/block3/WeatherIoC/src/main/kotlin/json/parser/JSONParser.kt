package json.parser

interface JSONParser<T> {
	fun parse(rawJson: String): T
}