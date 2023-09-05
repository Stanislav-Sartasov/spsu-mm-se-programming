import filter.*
import java.io.*

class FilterCLI {

	val errorMessage =
		"Incorrect arguments passed" + System.lineSeparator() +
				"Use --help to see details"

	val helpMessage =
		"This program applies following filters to BMP file" + System.lineSeparator() +
				"<filter> \t\t| <filter name>" + System.lineSeparator() +
				"Grayscale \t\t| grayscale" + System.lineSeparator() +
				"Average \t\t| average" + System.lineSeparator() +
				"Gaussian \t\t| gaussian" + System.lineSeparator() +
				"Sobel X-axis \t\t| sobelX" + System.lineSeparator() +
				"Sobel Y-axis \t\t| sobelY" + System.lineSeparator() +
				"Minimum \t\t| min" + System.lineSeparator() +
				"Maximum \t\t| max" + System.lineSeparator() +
				System.lineSeparator() +
				"Args: <source path> <destination path> <filter1>..."

	val noSuchFilterMessage = "No such filter:"
	val accessDeniedMessage = "Access denied to file:"
	val fileNotFoundMessage = "Cannot find file:"
	val unsupportedFileFormatMessage = "Unsupported file format: only 32-bit and 24-bit BMP files allowed"

	private lateinit var sourceFilename: String
	private lateinit var destinationFilename: String
	private lateinit var filterName: String

	val filters = mapOf(
		"grayscale" to GrayscaleFilter(),
		"average" to KernelFilter(
			arrayOf(
				arrayOf(1.0 / 9, 1.0 / 9, 1.0 / 9),
				arrayOf(1.0 / 9, 1.0 / 9, 1.0 / 9),
				arrayOf(1.0 / 9, 1.0 / 9, 1.0 / 9)
			)
		),
		"gaussian" to KernelFilter(
			arrayOf(
				arrayOf(1.0 / 16, 1.0 / 8, 1.0 / 16),
				arrayOf(1.0 / 8, 1.0 / 4, 1.0 / 8),
				arrayOf(1.0 / 16, 1.0 / 8, 1.0 / 16)
			)
		),
		"sobelX" to KernelFilter(
			arrayOf(
				arrayOf(-1.0, -2.0, -1.0),
				arrayOf(.0, .0, .0),
				arrayOf(1.0, 2.0, 1.0)
			)
		),
		"sobelY" to KernelFilter(
			arrayOf(
				arrayOf(-1.0, .0, 1.0),
				arrayOf(-2.0, .0, 2.0),
				arrayOf(-1.0, .0, 1.0)
			)
		)
	)

	var run: () -> Unit = { println(errorMessage) }

	fun parseArgs(args: Array<String>) {
		if (args.size == 1 && args[0] == "--help")
			run = { println(helpMessage) }
		if (args.size == 3) {
			run = ::run
			sourceFilename = args[0]
			destinationFilename = args[1]
			filterName = args[2]
		}
	}

	private fun run() {
		if (!filters.containsKey(filterName)) {
			println("$noSuchFilterMessage $filterName")
			return
		}
		val filter = filters[filterName]
		try {
			BufferedInputStream(FileInputStream(sourceFilename)).use { inputStream ->
				BufferedOutputStream(FileOutputStream(destinationFilename)).use { outputStream ->
					FilterApp(inputStream, outputStream, filter!!).run()
				}
			}
		} catch (accessDenied: AccessDeniedException) {
			println("$accessDeniedMessage ${accessDenied.file.path}")
		} catch (fileNotFound: FileNotFoundException) {
			println("$fileNotFoundMessage $sourceFilename")
		} catch (illegalArgument: IllegalArgumentException) {
			println(unsupportedFileFormatMessage)
		} catch (exception: Exception) {
			println(exception.message)
		}

	}
}
