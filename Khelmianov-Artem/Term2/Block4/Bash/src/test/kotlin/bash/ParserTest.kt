package bash

import TestLogger
import org.junit.jupiter.api.Test
import org.junit.jupiter.api.assertThrows
import utils.ASTPrettyPrinter
import utils.BashParserException
import kotlin.test.assertEquals

internal class ParserTest {

    @Test
    fun parse() {
        val expected = """
Sequence:
	Simple command: 
		Assignment:
			a
			abc
	Simple command: 
		Command:
			one
			a
			a-b-c
	Pipe:
		Simple command: 
			Command:
				pipe
		Pipe:
			Simple command: 
				Command:
					pipe
					with
					args
			Simple command: 
				Command:
					pipe
	Pipe:
		Simple command: 
			Command:
				cat
			Redirection: In
				file
		Simple command: 
			Command:
				cat
			Redirection: Out
				another/file
        """.trimIndent()
        val actual = Parser(
            Lexer(
                """
            a=abc
            one ${'$'}a "${'$'}a-b-c"
            pipe | pipe with "args" | pipe
            cat < file | cat > another/file
        """.trimIndent(), TestLogger()
            ).lex(), TestLogger()
        ).parse()
        assertEquals(expected, ASTPrettyPrinter().run(actual))
    }

    @Test
    fun pipeToNothing() {
        assertThrows<BashParserException> {
            Parser(Lexer("echo echo | ", TestLogger()).lex(), TestLogger()).parse()
        }
    }

    @Test
    fun pipeFromNothing() {
        assertThrows<BashParserException> {
            Parser(Lexer(" | cat ", TestLogger()).lex(), TestLogger()).parse()
        }
    }
}