package minibash.parsing

sealed interface ExpandableString {

    data class Variable(val name: String) : ExpandableString

    data class Quoted(val content: List<Either<Variable, String>>) : ExpandableString

    data class Word(val value: String) : ExpandableString
}
