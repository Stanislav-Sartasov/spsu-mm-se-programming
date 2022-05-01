package minibash.parsing

sealed interface StringValue {

    data class Plain(val value: String) : StringValue

    data class Variable(val name: String) : StringValue
}
