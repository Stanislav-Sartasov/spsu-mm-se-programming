package bmp

import bmp.TestUtils.TEST_RES_PATH
import bmp.lib.BmpIO
import com.github.stefanbirkner.systemlambda.SystemLambda
import org.junit.jupiter.api.AfterEach
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test
import java.io.File
import kotlin.io.path.createTempDirectory
import kotlin.test.assertEquals
import kotlin.test.assertTrue

internal class CliAppTest {
    private lateinit var tempFolder: File

    private val tempFilePath get() = tempFolder.resolve("win_tulips_out.bmp").absolutePath

    @BeforeEach
    fun setUp() {
        tempFolder = File(createTempDirectory("TEST").toUri())
    }

    @AfterEach
    fun tearDown() {
        tempFolder.deleteRecursively()
    }


    @Test
    fun `successful run`() {
        val consoleOutput = SystemLambda.tapSystemOut {
            CliApp.run(arrayOf(INPUT_PATH, FILTER, tempFilePath))
        }
        assertEquals(
            expected = BmpIO.readBmp(EXPECTED_PATH),
            actual = BmpIO.readBmp(tempFilePath)
        )
        assertEquals(
            expected = joinLines(
                INTRO_MESSAGE,
                "Filter $FILTER was successfully applied to the $INPUT_PATH, you should find the result here - $tempFilePath."
            ),
            actual = consoleOutput
        )
    }

    @Test
    fun `fail to run with 2 arguments`() {
        val consoleOutput = SystemLambda.tapSystemOut {
            CliApp.run(arrayOf(INPUT_PATH, FILTER))
        }
        assertTrue { !File(tempFilePath).exists() }
        assertEquals(
            expected = joinLines(
                INTRO_MESSAGE,
                "Error: expected 3 arguments, got 2.",
                "Please, try again..."
            ),
            actual = consoleOutput
        )
    }

    @Test
    fun `fail to run with 4 arguments`() {
        val consoleOutput = SystemLambda.tapSystemOut {
            CliApp.run(arrayOf(INPUT_PATH, FILTER, tempFilePath, tempFilePath))
        }
        assertTrue { !File(tempFilePath).exists() }
        assertEquals(
            expected = joinLines(
                INTRO_MESSAGE,
                "Error: expected 3 arguments, got 4.",
                "Please, try again..."
            ),
            actual = consoleOutput
        )
    }

    @Test
    fun `fail to run with wrong filter`() {
        val consoleOutput = SystemLambda.tapSystemOut {
            CliApp.run(arrayOf(INPUT_PATH, "not_a_filter", tempFilePath))
        }
        assertTrue { !File(tempFilePath).exists() }
        assertEquals(
            expected = joinLines(
                INTRO_MESSAGE,
                "Error: expected one of the available filters (gauss_3, gauss_5, gray_scale, median_3, sobel_x, sobel_y), got not_a_filter.",
                "Please, try again..."
            ),
            actual = consoleOutput
        )
    }

    @Test
    fun `fail to run with wrong input pathname`() {
        CliApp.run(arrayOf("not_an_input", FILTER, tempFilePath))
        assertTrue { !File(tempFilePath).exists() }
    }


    companion object {
        private const val FILTER = "sobel_x"
        private val INPUT_PATH = "${TEST_RES_PATH}win_tulips.bmp"
        private val EXPECTED_PATH = "${TEST_RES_PATH}win_tulips_$FILTER.bmp"
        private val INTRO_MESSAGE = joinLines(
            "This application can filter bmp files with various algorithms.",
            "",
            "To run application, please, specify input file, filter, and output file.",
            "Provided filters: gauss_3, gauss_5, gray_scale, median_3, sobel_x, sobel_y.",
            "For example:",
            "    On Windows: gradlew.bat :run --args=\"input.bmp gauss_5 output.bmp\"",
            "    On any other system: ./gradlew :run --args='input.bmp gauss_5 output.bmp'"
        )

        private fun joinLines(vararg lines: String) = lines.joinToString(separator = "") {
            "$it${System.lineSeparator()}"
        }
    }
}
