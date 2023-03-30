import java.io.File

fun readArray(path: String): IntArray =
    File(path)
        .readText()
        .trim()
        .split(' ')
        .map(String::toInt)
        .toIntArray()


fun writeArray(path: String, array: IntArray) {
    File(path).writeText(array.joinToString(separator = " "))
}
