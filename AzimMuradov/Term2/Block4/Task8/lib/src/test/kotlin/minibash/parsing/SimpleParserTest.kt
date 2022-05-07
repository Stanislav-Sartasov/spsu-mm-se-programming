package minibash.parsing

import minibash.TestUtils.asArguments
import minibash.parsing.SimpleParser.INCORRECT_COMMAND_OR_PIPE_SYNTAX_ERROR_MESSAGE
import minibash.parsing.SimpleParser.INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE
import minibash.parsing.SimpleParser.TOO_MANY_LINES_SYNTAX_ERROR_MESSAGE
import minibash.utils.left
import minibash.utils.right
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.MethodSource
import kotlin.test.assertEquals
import minibash.parsing.ExpandableString.Quoted as quoted
import minibash.parsing.ExpandableString.Variable as variable
import minibash.parsing.ExpandableString.Word as word
import minibash.parsing.Instruction.None as none
import minibash.parsing.Instruction.Pipe as pipe
import minibash.parsing.Instruction.SyntaxError as err
import minibash.parsing.Instruction.VariableAssignment as va

internal class SimpleParserTest {

    @ParameterizedTest
    @MethodSource("linesWithInstructions")
    fun `parse line`(line: String, instruction: Instruction) {
        assertEquals(expected = instruction, actual = SimpleParser.parse(line))
    }


    private companion object {

        @JvmStatic
        fun linesWithInstructions() = listOf(

            // Variable assignment instruction

            "\$var=value" to va(name = "var", value = word("value")),
            " \$var=value" to va(name = "var", value = word("value")),
            " \t \$var=value" to va(name = "var", value = word("value")),
            "\$var=;value" to va(name = "var", value = word(";value")),
            "\$var=value " to va(name = "var", value = word("value")),
            "\$var=value \t " to va(name = "var", value = word("value")),
            "\$_var=value" to va(name = "_var", value = word("value")),
            "\$_Va17r34=value" to va(name = "_Va17r34", value = word("value")),
            "\$Var=value" to va(name = "Var", value = word("value")),
            "\$va_r_=value" to va(name = "va_r_", value = word("value")),
            "\$var=\"\"" to va(name = "var", value = quoted(emptyList())),
            "\$var=\" \"" to va(name = "var", value = quoted(listOf(" ".right()))),
            "\$var=\"  \t  \"" to va(name = "var", value = quoted(listOf("  \t  ".right()))),
            "\$var=\"value\"" to va(name = "var", value = quoted(listOf("value".right()))),
            "\$var=\"v alue\"" to va(name = "var", value = quoted(listOf("v alue".right()))),
            "\$var=\" v a lue \"" to va(name = "var", value = quoted(listOf(" v a lue ".right()))),
            "\$var=\" value \"" to va(name = "var", value = quoted(listOf(" value ".right()))),
            "\$var=\" value\"" to va(name = "var", value = quoted(listOf(" value".right()))),
            "\$var=\"value \"" to va(name = "var", value = quoted(listOf("value ".right()))),
            "\$var=\$abc" to va(name = "var", value = variable("abc")),
            "\$var=\$_abc" to va(name = "var", value = variable("_abc")),
            "\$var=\$_Ab18c33" to va(name = "var", value = variable("_Ab18c33")),
            "\$var=\$Abc" to va(name = "var", value = variable("Abc")),
            "\$var=\$ab_c_" to va(name = "var", value = variable("ab_c_")),
            "\$var=\$abc  \t " to va(name = "var", value = variable("abc")),
            "\$var=\"\$abc \"" to va(name = "var", value = quoted(
                listOf(
                    variable("abc").left(),
                    " ".right()
                )
            )),
            "\$var=\" \$abc\"" to va(name = "var", value = quoted(
                listOf(
                    " ".right(),
                    variable("abc").left(),
                )
            )),
            "\$var=\" \$abc\t \"" to va(name = "var", value = quoted(
                listOf(
                    " ".right(),
                    variable("abc").left(),
                    "\t ".right()
                )
            )),
            "\$var=\"txt\$abc text\"" to va(name = "var", value = quoted(
                listOf(
                    "txt".right(),
                    variable("abc").left(),
                    " text".right(),
                )
            )),
            "\$var=\"txt \$abc \$xyz\"" to va(name = "var", value = quoted(
                listOf(
                    "txt ".right(),
                    variable("abc").left(),
                    " ".right(),
                    variable("xyz").left(),
                )
            )),


            // Variable assignment instruction failed

            "\$var" to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$var=" to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$var= " to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$var=  \t  " to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$=value" to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$var= value" to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$1var=value" to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$-var=value" to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$va-r=value" to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$va;r=value" to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$var =value" to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$ var=sd" to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$ var =sd" to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$var=txt\"text\"" to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$var=\"text\"txt" to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$var=txt\"text\"txt" to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$v ar=value" to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$\$var=value" to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$var=\$abc\$xyz" to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$var=text\$abc" to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$var=$" to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$var=$$" to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$var=$23$-" to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$var=\$abc$\$xyz" to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),
            "\$var=\$abc\$xyz$" to err(message = INCORRECT_VARIABLE_SYNTAX_ERROR_MESSAGE),


            // Pipe instruction

            "cmd" to pipe(commandsWithArguments = listOf("cmd" to emptyList())),
            "cmd " to pipe(commandsWithArguments = listOf("cmd" to emptyList())),
            " cmd " to pipe(commandsWithArguments = listOf("cmd" to emptyList())),
            "\t cmd " to pipe(commandsWithArguments = listOf("cmd" to emptyList())),
            "cmd arg" to pipe(commandsWithArguments = listOf("cmd" to listOf(word("arg")))),
            "cmd \t  arg  " to pipe(commandsWithArguments = listOf("cmd" to listOf(word("arg")))),
            "cmd \t  arg1  arg2" to pipe(commandsWithArguments = listOf("cmd" to listOf(word("arg1"), word("arg2")))),
            "cmd1 | cmd2 | cmd3" to pipe(
                commandsWithArguments = listOf(
                    "cmd1" to emptyList(),
                    "cmd2" to emptyList(),
                    "cmd3" to emptyList(),
                )
            ),
            "cmd1 arg1 arg2 | cmd2 | cmd3 arg1" to pipe(
                commandsWithArguments = listOf(
                    "cmd1" to listOf(word("arg1"), word("arg2")),
                    "cmd2" to emptyList(),
                    "cmd3" to listOf(word("arg1")),
                )
            ),
            "cmd1 \"abc | xyz\"" to pipe(
                commandsWithArguments = listOf(
                    "cmd1" to listOf(quoted(listOf("abc | xyz".right()))),
                )
            ),


            // Pipe instruction failed

            "cmd1 | | cmd2" to err(message = INCORRECT_COMMAND_OR_PIPE_SYNTAX_ERROR_MESSAGE),
            "cmd1 |" to err(message = INCORRECT_COMMAND_OR_PIPE_SYNTAX_ERROR_MESSAGE),


            // No instruction

            "" to none,
            "  " to none,
            " \t \t " to none,


            // Failed with too many lines

            "cmd1\ncmd2" to err(message = TOO_MANY_LINES_SYNTAX_ERROR_MESSAGE),
        ).asArguments()
    }
}
