package bash

/*
* sequence      :   pipe (';' pipe)* EOF
* pipe          :   simpleCommand ('|' simpleCommand)*
* simpleCommand :   assignment* ( command (redirections*)? )?
* assignment    :   AssignmentToken
* command       :   (WordToken)*
* redirections  :   (RedirectionToken WordToken)*
*/

interface Visitable {
    fun accept(visitor: Visitor): Any
}

open class AST : Visitable {
    override fun accept(visitor: Visitor): Any = visitor.visit(this)
}

data class Sequence(val pipes: List<AST>) : AST()
data class Pipe(val left: AST, val right: AST) : AST()
data class SimpleCommand(val assign: List<Assignment>?, val cmd: Command?, val redirections: List<Redirection>?) : AST()
data class Assignment(val key: Word, val value: Word) : AST()
data class Command(val command: Word, val args: List<Word>) : AST()
data class Redirection(val type: RedirectionType, val file: Word) : AST()
data class Word(val token: WordToken) : AST()


interface Visitor {
    fun visit(sequence: Sequence): Any
    fun visit(pipe: Pipe): Any
    fun visit(command: SimpleCommand): Any
    fun visit(assignment: Assignment): Any
    fun visit(command: Command): Any
    fun visit(redirection: Redirection): Any
    fun visit(word: Word): Any

    fun visit(ast: AST): Any = when (ast) {
        is Sequence -> this.visit(ast)
        is Pipe -> this.visit(ast)
        is SimpleCommand -> this.visit(ast)
        is Assignment -> this.visit(ast)
        is Command -> this.visit(ast)
        is Redirection -> this.visit(ast)
        is Word -> this.visit(ast)
        else -> throw NotImplementedError()
    }
}
