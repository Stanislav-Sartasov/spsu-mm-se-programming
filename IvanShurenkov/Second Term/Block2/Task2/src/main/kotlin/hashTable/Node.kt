package hashTable

class Node<Type>(_key: Int, _value: Type) {
    val key: Int
    var value: Type

    init {
        key = _key
        value = _value
    }
}