package bmp

import bmp.lib.BmpIO
import com.github.stefanbirkner.systemlambda.SystemLambda
import org.junit.jupiter.api.AfterEach
import org.junit.jupiter.api.Test
import java.io.File
import kotlin.test.assertEquals
import kotlin.test.assertTrue

internal class CliAppTest {
    @AfterEach
    fun cleanUp() {
        File(OUTPUT_PATH).delete()
    }


    @Test
    fun `successful run`() {
        val consoleOutput = SystemLambda.tapSystemOut {
            CliApp.run(arrayOf(INPUT_PATH, FILTER, OUTPUT_PATH))
        }
        assertEquals(
            expected = BmpIO.readBmp(EXPECTED_PATH),
            actual = BmpIO.readBmp(OUTPUT_PATH)
        )
        assertEquals(
            expected = joinLines(
                INTRO_MESSAGE,
                "Filter $FILTER was successfully applied to the $INPUT_PATH, you should find the result here - $OUTPUT_PATH."
            ),
            actual = consoleOutput
        )
    }

    @Test
    fun `fail to run with 2 arguments`() {
        val consoleOutput = SystemLambda.tapSystemOut {
            CliApp.run(arrayOf(INPUT_PATH, FILTER))
        }
        assertTrue { !File(OUTPUT_PATH).exists() }
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
            CliApp.run(arrayOf(INPUT_PATH, FILTER, OUTPUT_PATH, OUTPUT_PATH))
        }
        assertTrue { !File(OUTPUT_PATH).exists() }
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
            CliApp.run(arrayOf(INPUT_PATH, "not_a_filter", OUTPUT_PATH))
        }
        assertTrue { !File(OUTPUT_PATH).exists() }
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
        val consoleOutput = SystemLambda.tapSystemOut {
            CliApp.run(arrayOf("not_an_input", FILTER, OUTPUT_PATH))
        }
        assertTrue { !File(OUTPUT_PATH).exists() }
        assertEquals(
            expected = joinLines(
                INTRO_MESSAGE,
                "Error: not_an_input (No such file or directory).",
                "Please, try again..."
            ),
            actual = consoleOutput
        )
    }


    companion object {
        private const val FILTER = "sobel_x"
        private const val INPUT_PATH = "src/test/resources/win_tulips.bmp"
        private const val OUTPUT_PATH = "src/test/resources/win_tulips_TEST.bmp"
        private const val EXPECTED_PATH = "src/test/resources/win_tulips_$FILTER.bmp"
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
