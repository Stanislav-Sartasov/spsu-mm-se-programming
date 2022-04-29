import io.BMPReader
import org.junit.jupiter.api.BeforeEach

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test
import java.io.*

internal class FilterCLITest {

	private lateinit var console: OutputStream
	private val cli = FilterCLI()
	private val resourcesPath = "src/test/resources"
	private val existingFilePathIn = "$resourcesPath/test.bmp"
	private val existingFilePathOut = "$resourcesPath/test_out.bmp"
	private val notExistingFilePath = "$resourcesPath/test1.bmp"
	private val notBMPFileName = "$resourcesPath/notBMP.txt"
	private val allowedFilterName = "average"
	private val notAllowedFilterName = "keklol"

	@BeforeEach
	fun setUp() {
		console = ByteArrayOutputStream()
		System.setOut(PrintStream(console))
	}

	@Test
	fun `help message should be printed if the only argument is --help`() {
		main(arrayOf("--help"))
		assertEquals(cli.helpMessage, console.toString().trim())
	}

	@Test
	fun `error message should be printed if arguments are incorrect`() {
		main(arrayOf("42", "228"))
		assertEquals(cli.errorMessage, console.toString().trim())
	}

	@Test
	fun `no such filter message should be printed if given filter is not allowed`() {
		main(arrayOf(this.existingFilePathIn, this.existingFilePathOut, notAllowedFilterName))
		assertEquals("${cli.noSuchFilterMessage} $notAllowedFilterName", console.toString().trim())
	}

	@Test
	fun `file not found message should be printed if given file does not exist`() {
		main(arrayOf(notExistingFilePath, this.existingFilePathOut, allowedFilterName))
		assertTrue(console.toString().trim().startsWith(cli.fileNotFoundMessage))
	}

	@Test
	fun `unsupported file format message should be printed if given file is not 32-bit or 24-bit BMP file`() {
		main(arrayOf(notBMPFileName, this.existingFilePathOut, allowedFilterName))
		assertEquals(cli.unsupportedFileFormatMessage, console.toString().trim())
	}

	@Test
	fun `executing program should create filtered image`() {
		main(arrayOf(existingFilePathIn, this.existingFilePathOut, allowedFilterName))
		FileInputStream(existingFilePathOut).use {
			val result = BMPReader(it.buffered()).readBMP()
			FileInputStream("$resourcesPath/$allowedFilterName.bmp").use {
				val reference = BMPReader(it.buffered()).readBMP()
				assertEquals(reference, result)
			}
		}
	}
}