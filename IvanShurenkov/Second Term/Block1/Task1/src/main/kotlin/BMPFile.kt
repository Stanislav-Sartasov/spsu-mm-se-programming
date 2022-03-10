import java.io.File

class BMPFile(_file: File) {
    val width: Int
    val height: Int
    val bitCnt: Int
    val row: Int
    val image: Array<Array<Short>>
    private val header: ByteArray = ByteArray(54)
    private val file: File

    private fun from4BytesToInt(buffer: ByteArray, offset: Int): Int {
        var i: Int = offset
        return (buffer[i++].toInt() and 0xff) or
                (buffer[i++].toInt() and 0xff shl 8) or
                (buffer[i++].toInt() and 0xff shl 16) or
                (buffer[i].toInt() and 0xff shl 24)
    }

    init {
        file = _file

        var bytes: ByteArray = file.readBytes()
        width = from4BytesToInt(bytes, 18)
        height = from4BytesToInt(bytes, 22)
        bitCnt = from4BytesToInt(bytes, 26) shr 16
        row = ((bitCnt * width + 31) / 32) * 4
        image = Array(height) { Array(row) { 0 } }

        for (i in 0..53) {
            header[i] = bytes[i]
        }
        for (i in 0 until row * height) {
            image[i / row][i % row] = (bytes[i + 54].toInt() and 0xff).toShort()
        }
    }

    fun writeResult(fileOut: File) {
        val size: Int = from4BytesToInt(header, 2)
        var result: ByteArray = ByteArray(size)
        for (i in 0..53) {
            result[i] = header[i];
        }
        for (i in 54 until row * height + 54) {
            result[i] = image[(i - 54) / row][(i - 54) % row].toByte()
        }
        fileOut.writeBytes(result)
    }
}
