package bash

import utils.getClassName

abstract class Token(
    val line: Int = 0,
    val pos: Int = 0
) {
    fun position(): String = "@[$line:$pos]"
    override fun toString(): String = "${position()} ${getClassName(this)}"
}

enum class RedirectionType {
    In,
    Out
}

class SemicolonToken(
    line: Int,
    pos: Int
) : Token(line, pos)

class PipeToken(
    line: Int,
    pos: Int
) : Token(line, pos)

class EOFToken(
    line: Int,
    pos: Int
) : Token(line, pos)

class RedirectionToken(
    val direction: RedirectionType,
    line: Int,
    pos: Int
) : Token(line, pos)

open class WordToken(
    var word: String,
    line: Int = 0,
    pos: Int = 0
) : Token(line, pos) {
    constructor(line: Int, pos: Int) : this("", line, pos)
    override fun toString(): String = "${position()} ${getClassName(this)} = $word"
}

class CompoundWordToken(
    line: Int = 0,
    pos: Int = 0
) : WordToken(line, pos) {
    val wordlist = arrayListOf<WordToken>()

//    fun addToken(compWord: CompoundWordToken) = compWord.wordlist.map { addToken(it) }
    fun addToken(token: Token) = wordlist.add(token as WordToken)
    override fun toString(): String = "${position()} ${getClassName(this)} = $wordlist"
}

class AssignmentToken(
    val key: WordToken,
    val value: WordToken,
    line: Int = 0,
    pos: Int = 0,
) : WordToken(line, pos) {
    override fun toString(): String = "${position()} ${getClassName(this)}: $key = $value"
}

class VarSubstitutionToken(
    name: String,
    line: Int = 0,
    pos: Int = 0
) : WordToken(name, line, pos) {
    override fun toString(): String = "${position()} ${getClassName(this)} key = $word"
}

class CommandSubstitutionToken(
    cmd: String,
    line: Int = 0,
    pos: Int = 0,
) : WordToken(cmd, line, pos)