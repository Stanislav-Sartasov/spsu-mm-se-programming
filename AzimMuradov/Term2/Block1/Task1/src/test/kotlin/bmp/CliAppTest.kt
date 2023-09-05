package bmp

import bmp.CliApp.FILTERS
import bmp.TestUtils.TEST_RES_PATH
import bmp.lib.BmpIO
import com.github.stefanbirkner.systemlambda.SystemLambda
import org.junit.jupiter.api.*
import java.io.File
import kotlin.io.path.createTempDirectory
import kotlin.test.assertEquals
import kotlin.test.assertTrue

internal class CliAppTest {

    private lateinit var tempFolder: File

    private val tempFilePath get() = tempFolder.resolve(OUTPUT_PATH).absolutePath

    @BeforeEach
    fun setUp() {
        tempFolder = File(createTempDirectory(TEMP_TEST_DIR_PREFIX).toUri())
    }

    @AfterEach
    fun tearDown() {
        tempFolder.deleteRecursively()
    }


    @Test
    fun `successful run`() {
        val consoleOutput = SystemLambda.tapSystemOut {
            CliApp.run(arrayOf(INPUT_PATH, FILTER_NAME, tempFilePath))
        }
        assertEquals(
            expected = BmpIO.readBmp(EXPECTED_PATH),
            actual = BmpIO.readBmp(tempFilePath)
        )
        assertEquals(
            expected = """
                |$INTRO_MESSAGE
                |Filter $FILTER_NAME was successfully applied to the $INPUT_PATH, you should find the result here - $tempFilePath.
                |
            """.trimMargin(marginPrefix = "|").withSystemEndings(),
            actual = consoleOutput
        )
    }

    @Test
    fun `fail to run with 2 arguments`() {
        val consoleOutput = SystemLambda.tapSystemOut {
            CliApp.run(arrayOf(INPUT_PATH, FILTER_NAME))
        }
        assertTrue { !File(tempFilePath).exists() }
        assertEquals(
            expected = """
                |$INTRO_MESSAGE
                |Error: expected 3 arguments, got 2.
                |$ERROR_ENDING_MESSAGE
                |
            """.trimMargin(marginPrefix = "|").withSystemEndings(),
            actual = consoleOutput
        )
    }

    @Test
    fun `fail to run with 4 arguments`() {
        val consoleOutput = SystemLambda.tapSystemOut {
            CliApp.run(arrayOf(INPUT_PATH, FILTER_NAME, tempFilePath, tempFilePath))
        }
        assertTrue { !File(tempFilePath).exists() }
        assertEquals(
            expected = """
                |$INTRO_MESSAGE
                |Error: expected 3 arguments, got 4.
                |$ERROR_ENDING_MESSAGE
                |
            """.trimMargin(marginPrefix = "|").withSystemEndings(),
            actual = consoleOutput
        )
    }

    @Test
    fun `fail to run with wrong filter`() {
        val consoleOutput = SystemLambda.tapSystemOut {
            CliApp.run(arrayOf(INPUT_PATH, NOT_A_FILTER_NAME, tempFilePath))
        }
        assertTrue { !File(tempFilePath).exists() }
        assertEquals(
            expected = """
                |$INTRO_MESSAGE
                |Error: expected one of the available filters (${FILTERS.keys.joinToString()}), got $NOT_A_FILTER_NAME.
                |$ERROR_ENDING_MESSAGE
                |
            """.trimMargin(marginPrefix = "|").withSystemEndings(),
            actual = consoleOutput
        )
    }

    @Test
    fun `fail to run with wrong input pathname`() {
        CliApp.run(arrayOf(NOT_AN_INPUT_PATH, FILTER_NAME, tempFilePath))
        assertTrue { !File(tempFilePath).exists() }
    }


    private companion object {

        private const val FILE_PREFIX = "win_tulips"

        private const val FILE_POSTFIX = ".bmp"


        private const val FILTER_NAME = "sobel_x"

        private const val NOT_A_FILTER_NAME = "not_a_filter"

        private val INPUT_PATH = "$TEST_RES_PATH$FILE_PREFIX$FILE_POSTFIX"

        private const val NOT_AN_INPUT_PATH = "not_an_input"

        private const val TEMP_TEST_DIR_PREFIX = "TEST"

        private const val OUTPUT_PATH = "${FILE_PREFIX}_out$FILE_POSTFIX"

        private val EXPECTED_PATH = "$TEST_RES_PATH${FILE_PREFIX}_$FILTER_NAME.bmp"


        private val INTRO_MESSAGE = """
                |This application can filter bmp files with various algorithms.
                |
                |To run application, please, specify input file, filter, and output file.
                |Provided filters: ${FILTERS.keys.joinToString()}.
                |For example:
                |  On Windows: gradlew.bat :run --args="input.bmp gauss_5 output.bmp"
                |  On any other system: ./gradlew :run --args='input.bmp gauss_5 output.bmp'
                |
            """.trimMargin(marginPrefix = "|").withSystemEndings()

        private const val ERROR_ENDING_MESSAGE = "Please, try again..."

        private fun String.withSystemEndings(): String = replace(oldValue = "\n", newValue = System.lineSeparator())
    }
}
