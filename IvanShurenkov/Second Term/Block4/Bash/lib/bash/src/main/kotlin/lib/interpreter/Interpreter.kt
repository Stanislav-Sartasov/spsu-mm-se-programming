package lib.interpreter

import lib.interpreter.parser.Block
import lib.interpreter.parser.Parser
import lib.interpreter.parser.Type
import lib.pipe.Output
import org.kodein.di.DI

class Interpreter {
    var variables: Map<String, String> = emptyMap()

    fun run(line: String, commands: DI): Output {
        val parsed = Parser.run(line)
        var leftSeq = -1
        var rightSeq = -1

        var output = Output()
        while (rightSeq < parsed.size) {
            leftSeq = rightSeq
            var subLine = emptyList<Block>()
            rightSeq++
            while (rightSeq < parsed.size && parsed[rightSeq].type != Type.SEQUENCE) {
                subLine += parsed[rightSeq]
                rightSeq++
            }
            var newOut = Instruction(subLine, commands).run(output.output, variables)
            output = newOut.output
            if (output.error != null && output.error!!.isNotEmpty())
                return output
            if (newOut.variable != null) {
                variables += (newOut.variable) as Pair<String, String>
            }
        }
        return output
    }
}
//$a = wc
//$a gradlew