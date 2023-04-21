package psrs

import java.io.File


/**
 * Collection of useful input-output methods for integer arrays.
 */
object IntArrayIO {

    /**
     * Read an integer array from a file with the given [name].
     */
    fun readFile(name: String): IntArray = File(name)
        .readText()
        .splitToSequence(" ")
        .map(String::toInt)
        .toList()
        .toIntArray()

    /**
     * Write the [integer array][array] to a file with the given [name].
     */
    fun writeFile(name: String, array: IntArray) =
        File(name).writeText(array.joinToString(separator = " "))
}
