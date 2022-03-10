import filters.GaussFilter
import filters.GrayFilter
import filters.MedianFilter
import filters.SobelFilter
import org.junit.jupiter.api.Test
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.ValueSource
import java.io.File
import kotlin.test.assertEquals

class HelloJunit5Test {
    private val filePathIn = "src/test/resources/Image/original"
    private val filePathRightGray = "src/test/resources/Image/gray"
    private val filePathRightGauss = "src/test/resources/Image/gauss"
    private val filePathRightMedian = "src/test/resources/Image/median"
    private val filePathRightSobelX = "src/test/resources/Image/sobelx"
    private val filePathRightSobelY = "src/test/resources/Image/sobely"
    private val filePathRightSobelXY = "src/test/resources/Image/sobelxy"

    private val filePathOut = "src/test/resources/Image/out"

    private fun compareByteArray(first: ByteArray, second: ByteArray): Boolean {
        if (first.size != second.size)
            return false
        for (i in first.indices)
            if (first[i] != second[i])
                return false
        return true
    }

    @Test
    fun `Try open file witch don't exist`() {
        main(arrayOf("dont_exist.bmp", "gr", "${filePathOut}${1}.bmp"))
    }

    @Test
    fun `Open BMP image and read head (24bit)`() {
        val bmpFile: BMPFile = BMPFile(File("${filePathIn}1.bmp"))
        assertEquals(474, bmpFile.width)
        assertEquals(652, bmpFile.height)
        assertEquals(24, bmpFile.bitCnt)
        assertEquals(1424, bmpFile.row)
    }

    @ParameterizedTest
    @ValueSource(strings = ["gr", "g3", "m3", "sx", "sy", "sxy"])
    fun `Test main fun`(filter: String) {
        var rightFile: String = ""
        when (filter) {
            "gr" -> rightFile = filePathRightGray + "2.bmp"
            "g3" -> rightFile = filePathRightGauss + "2.bmp"
            "m3" -> rightFile = filePathRightMedian + "2.bmp"
            "sx" -> rightFile = filePathRightSobelX + "2.bmp"
            "sy" -> rightFile = filePathRightSobelY + "2.bmp"
            "sxy" -> rightFile = filePathRightSobelXY + "2.bmp"
        }
        main(arrayOf("${filePathIn}2.bmp", filter, "${filePathOut}2.bmp"))
        assert(
            compareByteArray(
                File("${filePathOut}2.bmp").readBytes(),
                File(rightFile).readBytes()
            )
        )
    }

    @ParameterizedTest
    @ValueSource(ints = [1, 2])
    fun `Test gray filter (24bit)`(picId: Int) {
        val bmpFile: BMPFile = BMPFile(File("${filePathIn}${picId}.bmp"))
        GrayFilter().applyFilter(bmpFile)
        bmpFile.writeResult(File("${filePathOut}${picId}.bmp"))

        assert(
            compareByteArray(
                File("${filePathOut}${picId}.bmp").readBytes(),
                File("${filePathRightGray}${picId}.bmp").readBytes()
            )
        )
    }

    @ParameterizedTest
    @ValueSource(ints = [1, 2])
    fun `Test gauss filter (24bit)`(picId: Int) {
        val bmpFile: BMPFile = BMPFile(File("${filePathIn}${picId}.bmp"))
        GaussFilter().applyFilter(bmpFile)
        bmpFile.writeResult(File("${filePathOut}${picId}.bmp"))

        assert(
            compareByteArray(
                File("${filePathOut}${picId}.bmp").readBytes(),
                File("${filePathRightGauss}${picId}.bmp").readBytes()
            )
        )
    }

    @ParameterizedTest
    @ValueSource(ints = [1, 2])
    fun `Test median filter (24bit)`(picId: Int) {
        val bmpFile: BMPFile = BMPFile(File("${filePathIn}${picId}.bmp"))
        MedianFilter().applyFilter(bmpFile)
        bmpFile.writeResult(File("${filePathOut}${picId}.bmp"))

        assert(
            compareByteArray(
                File("${filePathOut}${picId}.bmp").readBytes(),
                File("${filePathRightMedian}${picId}.bmp").readBytes()
            )
        )
    }

    @ParameterizedTest
    @ValueSource(ints = [1, 2])
    fun `Test sobel filter to axis x (24bit)`(picId: Int) {
        val bmpFile: BMPFile = BMPFile(File("${filePathIn}${picId}.bmp"))
        SobelFilter().applyFilter(bmpFile, "x")
        bmpFile.writeResult(File("${filePathOut}${picId}.bmp"))

        assert(
            compareByteArray(
                File("${filePathOut}${picId}.bmp").readBytes(),
                File("${filePathRightSobelX}${picId}.bmp").readBytes()
            )
        )
    }

    @ParameterizedTest
    @ValueSource(ints = [1, 2])
    fun `Test sobel filter to axis y (24bit)`(picId: Int) {
        val bmpFile: BMPFile = BMPFile(File("${filePathIn}${picId}.bmp"))
        SobelFilter().applyFilter(bmpFile, "y")
        bmpFile.writeResult(File("${filePathOut}${picId}.bmp"))

        assert(
            compareByteArray(
                File("${filePathOut}${picId}.bmp").readBytes(),
                File("${filePathRightSobelY}${picId}.bmp").readBytes()
            )
        )
    }

    @ParameterizedTest
    @ValueSource(ints = [1, 2])
    fun `Test sobel filter to axis x and y (24bit)`(picId: Int) {
        val bmpFile: BMPFile = BMPFile(File("${filePathIn}${picId}.bmp"))
        SobelFilter().applyFilter(bmpFile, "xy")
        bmpFile.writeResult(File("${filePathOut}${picId}.bmp"))

        assert(
            compareByteArray(
                File("${filePathOut}${picId}.bmp").readBytes(),
                File("${filePathRightSobelXY}${picId}.bmp").readBytes()
            )
        )
    }
}