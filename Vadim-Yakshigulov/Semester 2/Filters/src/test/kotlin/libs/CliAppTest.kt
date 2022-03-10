package libs

import org.junit.jupiter.api.Test

import org.junit.jupiter.api.Assertions.*
import java.io.File
import kotlin.io.path.createTempDirectory

internal class CliAppTest {

    @Test
    fun `fail to run with 2 arguments`() {
        val state = CliApp.run(arrayOf("1.bmp", "2.bmp"))
        assertEquals(state, CliApp.States.INVALID_ARGS_COUNT_ERROR)
    }

    @Test
    fun `fail to run with 4 arguments`() {
        val state = CliApp.run(arrayOf("1.bmp", "2.bmp", "filter", "3.bmp"))
        assertEquals(state, CliApp.States.INVALID_ARGS_COUNT_ERROR)
    }

    @Test
    fun `fail to run with invalid filter name`() {
        val state = CliApp.run(arrayOf("1.bmp", "filter", "2.bmp"))
        assertEquals(state, CliApp.States.INVALID_FILTER_NAME_ERROR)
    }

    @Test
    fun `fail to run if invalid image`() {
        val state = CliApp.run(arrayOf("src/test/resources/img/brokenKitten.bmp", "median", "2.bmp"))
        assertEquals(state, CliApp.States.INVALID_BMP_IMAGE_ERROR)
    }

    @Test
    fun `fail to run if output path is not valid`() {
        val state =
            CliApp.run(arrayOf("src/test/resources/img/kitten.bmp", "median", "?.<lk!/"))
        assertEquals(state, CliApp.States.ANOTHER_ERROR)
    }

    @Test
    fun `call with valid arguments works fine`() {
        val tempFolder = File(createTempDirectory("tempFiltersTestFolder").toUri())

        val state = CliApp.run(
            arrayOf(
                "src/test/resources/img/kitten.bmp",
                "median",
                tempFolder.resolve("medianKitten").absolutePath
            )
        )
        assertEquals(state, CliApp.States.SUCCESS)

        tempFolder.deleteRecursively()
    }
}