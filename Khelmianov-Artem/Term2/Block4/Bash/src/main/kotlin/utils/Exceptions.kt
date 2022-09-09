package utils

abstract class BashException : Exception()

class BashExitException(val exitCode: Int) : BashException()

class BashLexerException : BashException()

class BashParserException : BashException()

class BashInterpreterException : BashException()
