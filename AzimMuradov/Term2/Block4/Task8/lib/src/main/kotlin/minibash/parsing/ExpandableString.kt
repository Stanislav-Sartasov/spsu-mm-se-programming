package minibash.parsing

import minibash.utils.Either

sealed interface ExpandableString {

    data class Variable(val name: String) : ExpandableString

    data class Quoted(val content: List<Either<Variable, String>>) : ExpandableString

    data class Word(val value: String) : ExpandableString
}
