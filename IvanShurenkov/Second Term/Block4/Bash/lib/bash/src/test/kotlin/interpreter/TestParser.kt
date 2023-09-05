package interpreter

import lib.interpreter.parser.Block
import lib.interpreter.parser.Parser
import lib.interpreter.parser.Type
import org.junit.jupiter.api.Test
import kotlin.test.assertEquals

class TestParser {
    @Test
    fun `Test parser parseInBlock`() {
        Parser.addSubString("a")
        Parser.parseInBlocks("")
        assert(Parser.blocks.isEmpty())

        val answers = listOf(
            listOf(Block("\$a"), Block("="), Block("'bdf dsf'")),
            listOf(Block("\$a")),
            listOf(Block("ls"), Block("|"), Block("grep"), Block("a"))
        )
        val questions = listOf(
            "\$a =   'bdf dsf'", "\$a", "ls|grep  a"
        )
        for (i in questions.indices) {
            val parsed = Parser.run(questions[i])
            assertEquals(answers[i].size, parsed.size)
            for (j in parsed.indices) {
                assertEquals(answers[i][j].type, parsed[j].type)
                assertEquals(answers[i][j].string, parsed[j].string)
            }
        }
    }

    @Test
    fun `Test block init`() {
        assertEquals(Block("\$a").type, Type.VARIABLE)
        assertEquals(Block("\'dfs\'").type, Type.SUBSTRING)
        assertEquals(Block("dfs").type, Type.SUBSTRING)
        assertEquals(Block("=").type, Type.EQUAL)
        assertEquals(Block("|").type, Type.SEQUENCE)

        assertEquals(Block("\'dfs\'").string, "dfs")
        assertEquals(Block("dfs").string, "dfs")
    }
}