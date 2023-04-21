package bash

import TestLogger
import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.api.Test
import org.junit.jupiter.api.assertThrows
import utils.BashLexerException
import utils.LexerPrettyPrinter


internal class LexerTest {

    @Test
    fun scanTokens() {
        val tokens = Lexer(
            """
            hello=world
            escape\=eqsign
            multiple=equal=signs
            42notavalid=name
            a="\" quote"
            b='\' also quote'
            zero            # this is a comment
            one two "' three '" '" four "';
            ${'$'}hello
            "${'$'}hello "
            \${'$'}hello
            "\${'$'}hello "
            '${'$'}hello '
            "sub""sequent"${'$'}hello' world'
            pipe | pipe with "args" | pipe
            echo > file
            cat < file
            
        """.trimIndent(), TestLogger()
        ).lex()
        val actual = LexerPrettyPrinter(tokens)
        val expected = """
@[1:1] AssignmentToken: @[1:1] WordToken = hello = @[1:7] WordToken = world @[1:12] SemicolonToken 
@[2:1] WordToken = escape=eqsign @[2:15] SemicolonToken 
@[3:1] AssignmentToken: @[3:1] WordToken = multiple = @[3:10] WordToken = equal=signs @[3:21] SemicolonToken 
@[4:1] WordToken = 42notavalid=name @[4:17] SemicolonToken 
@[5:1] AssignmentToken: @[5:1] WordToken = a = @[5:4] WordToken = \" quote @[5:13] SemicolonToken 
@[6:1] AssignmentToken: @[6:1] WordToken = b = @[6:3] WordToken = \' also quote @[6:18] SemicolonToken 
@[7:1] WordToken = zero @[7:36] SemicolonToken 
@[8:1] WordToken = one @[8:5] WordToken = two @[8:10] WordToken = ' three ' @[8:21] WordToken = " four " @[8:31] SemicolonToken 
@[9:1] VarSubstitutionToken key = hello @[9:7] SemicolonToken 
@[10:1] CompoundWordToken = [@[10:2] VarSubstitutionToken key = hello, @[10:8] WordToken =  ] @[10:10] SemicolonToken 
@[11:1] WordToken = ${'$'}hello @[11:8] SemicolonToken 
@[12:2] WordToken = \${'$'}hello  @[12:11] SemicolonToken 
@[13:1] WordToken = ${'$'}hello  @[13:10] SemicolonToken 
@[14:1] CompoundWordToken = [@[14:2] WordToken = sub, @[14:7] WordToken = sequent, @[14:15] VarSubstitutionToken key = hello, @[14:21] WordToken =  world] @[14:29] SemicolonToken 
@[15:1] WordToken = pipe @[15:6] PipeToken @[15:8] WordToken = pipe @[15:13] WordToken = with @[15:19] WordToken = args @[15:25] PipeToken @[15:27] WordToken = pipe @[15:31] SemicolonToken 
@[16:1] WordToken = echo @[16:7] RedirectionToken @[16:8] WordToken = file @[16:12] SemicolonToken 
@[17:1] WordToken = cat @[17:6] RedirectionToken @[17:7] WordToken = file @[17:11] SemicolonToken 
@[18:1] EOFToken 

        """.trimIndent()

        assertEquals(expected, actual)
    }

    @Test
    fun quotePairs() {
        assertThrows<BashLexerException> { Lexer("'quote".trimIndent(), TestLogger()).lex() }
    }

    @Test
    fun doubleQuotePairs() {
        assertThrows<BashLexerException> { Lexer("\"quote".trimIndent(), TestLogger()).lex() }
    }
}