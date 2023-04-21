package lib.interpreter

import lib.interpreter.parser.Block
import lib.interpreter.parser.Type
import lib.pipe.Output
import lib.pipe.commands.ExternalCommand
import lib.pipe.commands.ICommand
import org.kodein.di.DI
import org.kodein.di.instance

class Instruction(val args: List<Block>, val commands: DI) {
    fun run(input: String?, variables: Map<String, String>): InstructionOut {
        if (args.isEmpty())
            return InstructionOut()
        if (args[0].type == Type.VARIABLE) {
            if (args.size == 3 && args[1].type == Type.EQUAL && args[2].type == Type.SUBSTRING) {
                return InstructionOut(Output(), (args[0].string to args[2].string))
            } else if (variables[args[0].string] == null) {
                return InstructionOut()
            }
            args[0].string = variables[args[0].string].toString()
        }

        val name = args[0].string
        var newArgs = emptyArray<String>()
        for (i in 1 until args.size) {
            newArgs += if (args[i].type == Type.VARIABLE) {
                if (variables[args[i].string] != null)
                    variables[args[i].string].toString()
                else
                    args[i].string
            } else {
                args[i].string
            }
        }

        try {
            val command: ICommand by commands.instance(name)
            return InstructionOut(command.run(newArgs, input))
        } catch (e: Exception) {
            val command = ExternalCommand(name)
            return InstructionOut(command.run(newArgs, input))
        }
    }
}
