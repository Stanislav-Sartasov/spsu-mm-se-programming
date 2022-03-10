package libs

import java.io.RandomAccessFile
import java.nio.ByteOrder
import java.nio.channels.FileChannel


class BmpImage private constructor(private val header: BmpHeader, val pixelData: Array<Array<RGBColor>>) {
    companion object {
        fun open(filename: String): BmpImage? {
            val inputFile = RandomAccessFile(filename, "r")
            try {
                val header = readBmpHeaderFromFile(inputFile)

                if (!header.isValid)
                    return null

                val pixelData = readPixelDataFromFile(inputFile, header)

                inputFile.close()
                return BmpImage(header, pixelData)

            } catch (e: Exception) {
                return null
            }
        }

        private fun readBmpHeaderFromFile(inputFile: RandomAccessFile): BmpHeader {
            val byteReader = inputFile.channel.map(FileChannel.MapMode.READ_ONLY, 0, inputFile.length())
            byteReader.order(ByteOrder.LITTLE_ENDIAN)
            return BmpHeader(
                bSignature = byteReader.get().toInt().toChar(),
                mSignature = byteReader.get().toInt().toChar(),
                sizeOfBmpFile = byteReader.int,
                reserved = byteReader.int,
                offset = byteReader.int,
                sizeOfBmpInfoHeader = byteReader.int,
                width = byteReader.int,
                height = byteReader.int,
                numberOfPlanes = byteReader.short,
                numberOfBitsPerPixel = byteReader.short,
                compressionType = byteReader.int,
                sizeOfImageData = byteReader.int,
                horizontalResolution = byteReader.int,
                verticalResolution = byteReader.int,
                numberOfColors = byteReader.int,
                numberOfImportantColors = byteReader.int
            )
        }

        private fun readPixelDataFromFile(
            inputFile: RandomAccessFile,
            header: BmpHeader
        ): Array<Array<RGBColor>> {
            inputFile.seek(header.offset.toLong())
            val arrayOfPixels = Array(header.height) { Array(header.width) { RGBColor(0, 0, 0) } }

            for (i in header.height - 1 downTo 0) {
                for (j in 0 until header.width) {
                    arrayOfPixels[i][j] = RGBColor(
                        blue = inputFile.readUnsignedByte(),
                        green = inputFile.readUnsignedByte(),
                        red = inputFile.readUnsignedByte()
                    )
                    if (header.numberOfBitsPerPixel.toInt() == 32) inputFile.skipBytes(1) // alpha byte skip
                }
                if (header.numberOfBitsPerPixel.toInt() == 24) inputFile.skipBytes((4 - (header.width * 3) % 4) % 4) // align bytes skip
            }
            return arrayOfPixels
        }
    }

    fun saveAs(filename: String) {
        val outputFile = RandomAccessFile(filename, "rw")
        writeHeaderIntoFile(outputFile)
        writePixelDataIntoFile(outputFile)
        outputFile.close()
    }

    private fun writeHeaderIntoFile(outputFile: RandomAccessFile) {
        val byteReader = outputFile.channel.map(FileChannel.MapMode.READ_WRITE, 0, 54)
        byteReader.order(ByteOrder.LITTLE_ENDIAN)
        byteReader.put(header.bSignature.code.toByte())
        byteReader.put(header.mSignature.code.toByte())
        byteReader.putInt(header.sizeOfBmpFile)
        byteReader.putInt(header.reserved)
        byteReader.putInt(header.offset)
        byteReader.putInt(header.sizeOfBmpInfoHeader)
        byteReader.putInt(header.width)
        byteReader.putInt(header.height)
        byteReader.putShort(header.numberOfPlanes)
        byteReader.putShort(header.numberOfBitsPerPixel)
        byteReader.putInt(header.compressionType)
        byteReader.putInt(header.sizeOfImageData)
        byteReader.putInt(header.horizontalResolution)
        byteReader.putInt(header.verticalResolution)
        byteReader.putInt(header.numberOfColors)
        byteReader.putInt(header.numberOfImportantColors)
    }

    private fun writePixelDataIntoFile(outputFile: RandomAccessFile) {
        outputFile.seek(header.offset.toLong())

        for (i in header.height - 1 downTo 0) {
            for (j in 0 until header.width) {
                outputFile.writeByte(pixelData[i][j].blue)
                outputFile.writeByte(pixelData[i][j].green)
                outputFile.writeByte(pixelData[i][j].red)
                if (header.numberOfBitsPerPixel.toInt() == 32) outputFile.writeByte(0) // alpha byte add
            }
            if (header.numberOfBitsPerPixel.toInt() == 24) outputFile.write(ByteArray((4 - (header.width * 3) % 4) % 4) { 0 }) // align bytes add
        }
    }

    private class BmpHeader(
        val bSignature: Char,
        val mSignature: Char,
        val sizeOfBmpFile: Int,
        val reserved: Int,
        val offset: Int,
        val sizeOfBmpInfoHeader: Int,
        val width: Int,
        val height: Int,
        val numberOfPlanes: Short,
        val numberOfBitsPerPixel: Short,
        val compressionType: Int,
        val sizeOfImageData: Int,
        val horizontalResolution: Int,
        val verticalResolution: Int,
        val numberOfColors: Int,
        val numberOfImportantColors: Int,
    ) {
        val isValid = bSignature == 'B' && mSignature == 'M' && reserved == 0 && numberOfPlanes.toInt() == 1
                && compressionType == 0 && (numberOfBitsPerPixel.toInt() in arrayOf(24, 32))

    }

    val width
        get() = header.width

    val height
        get() = header.height
}

