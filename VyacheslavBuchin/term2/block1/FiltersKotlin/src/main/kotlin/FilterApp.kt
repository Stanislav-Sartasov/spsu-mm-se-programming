
import filter.Filter
import io.BMPReader
import io.BMPWriter
import java.io.*

class FilterApp(
	private val inputStream: InputStream,
	private val outputStream: OutputStream,
	private val filter: Filter
) {

	fun run() {
		val reader = BMPReader(inputStream)
		val image = reader.readBMP()
		filter.applyTo(image)
		val writer = BMPWriter(outputStream)
		writer.writeBMP(image)
	}

}