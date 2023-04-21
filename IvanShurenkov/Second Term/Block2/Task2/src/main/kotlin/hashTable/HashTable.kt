package hashTable

class HashTable<Type>(_size: Int) {
    var size: Int
        private set
    private var cntLists: Int
    private var listOfElement: Array<List<Node<Type>>>

    init {
        size = 0
        cntLists = _size
        listOfElement = Array(cntLists) { mutableListOf<Node<Type>>() }
    }

    private fun hash(key: Int): Int {
        return key % this.cntLists
    }

    private fun isBalanced(): Boolean {
        for (i in listOfElement)
            if (2 * i.size > cntLists)
                return false
        return true
    }

    private fun rebalance() {
        if (isBalanced())
            return
        cntLists *= cntLists + 1
        val newListOfElement: Array<List<Node<Type>>> = Array(cntLists) { mutableListOf<Node<Type>>() }
        for (elements in listOfElement) {
            for (i in elements) {
                newListOfElement[hash(i.key)] = newListOfElement[hash(i.key)] + i
            }
        }
        listOfElement = newListOfElement
    }

    fun add(key: Int, value: Type) {
        val index = hash(key)
        listOfElement[index].forEach {
            if (it.key == key) {
                it.value = value
                return
            }
        }
        size++
        listOfElement[index] = listOfElement[index] + Node<Type>(key, value)
        rebalance()
    }

    fun remove(key: Int) {
        val index = hash(key)
        val lenOldList = listOfElement[index].size
        listOfElement[index] = listOfElement[index].filter {
            it.key != key
        }
        if (lenOldList > listOfElement[index].size)
            size--
    }

    fun get(key: Int): Type? {
        val index = hash(key)
        val temp = listOfElement[index].find { it.key == key } ?: return null
        return temp.value
    }
}
