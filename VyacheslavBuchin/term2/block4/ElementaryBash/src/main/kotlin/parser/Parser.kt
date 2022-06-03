package parser

interface Parser {
	fun parse(tokens: List<String>): List<List<String>>
}