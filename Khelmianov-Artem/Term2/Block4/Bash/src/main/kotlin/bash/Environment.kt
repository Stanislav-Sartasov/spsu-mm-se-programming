package bash

interface IEnvironment {
    var exiting: Boolean
    var exitCode: Int
    fun set(key: String, value: String)
    fun get(key: String): String
    fun getAll(): Map<String, String>
}

class Environment : IEnvironment {
    override var exiting = false
    override var exitCode = 0
    private val variables = System.getenv().toMutableMap()

    override fun set(key: String, value: String) = variables.set(key, value)
    override fun get(key: String): String = variables.getOrDefault(key, "")
    override fun getAll(): Map<String, String> = variables
}