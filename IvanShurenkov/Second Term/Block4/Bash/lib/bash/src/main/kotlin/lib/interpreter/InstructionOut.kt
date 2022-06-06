package lib.interpreter

import lib.pipe.Output

class InstructionOut(val output: Output = Output(), val variable: Pair<String, String>? = null)