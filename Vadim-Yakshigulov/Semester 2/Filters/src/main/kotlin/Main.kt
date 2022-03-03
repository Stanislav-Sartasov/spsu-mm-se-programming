import libs.BmpImage
import libs.applyFilter
import libs.deepCopy
import libs.filters.*
import java.io.File
import java.util.concurrent.TimeUnit

fun main() {
    ProcessBuilder(
        "src/test/resources/CFilters",
        "src/test/resources/img/kitten.bmp",
        "gaussian_5x5",
        "src/test/resources/img/kittenCG5.bmp"
    ).start().waitFor()
}