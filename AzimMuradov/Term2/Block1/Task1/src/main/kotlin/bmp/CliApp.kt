package bmp

import bmp.lib.*
import bmp.lib.ValidatedBmp.Companion.validated
import bmp.lib.ValidatedBmp.ValidBmpBitsPerPixel
import bmp.lib.filters.*

object CliApp {

    fun run(args: Array<String>) {
        println(
            """
                |This application can filter bmp files with various algorithms.
                |
                |To run application, please, specify input file, filter, and output file.
                |Provided filters: ${FILTERS.keys.joinToString()}.
                |For example:
                |  On Windows: gradlew.bat :run --args="input.bmp gauss_5 output.bmp"
                |  On any other system: ./gradlew :run --args='input.bmp gauss_5 output.bmp'
                |
            """.trimMargin(marginPrefix = "|").withSystemEndings()
        )

        if (args.size != 3) {
            println("Error: expected 3 arguments, got ${args.size}.")
            println("Please, try again...")
            return
        }

        val (inputPath, filterName, outputPath) = args
        val filter = FILTERS[filterName]

        if (filter == null) {
            println("Error: expected one of the available filters (${FILTERS.keys.joinToString()}), got $filterName.")
            println("Please, try again...")
            return
        }

        try {
            val input = BmpIO.readBmp(path = inputPath)
            val output = input.bmp.toRawImg().filter(filter).toBmp(ValidBmpBitsPerPixel.BPP_24).validated()
            BmpIO.writeBmp(path = outputPath, validatedBmp = output)

            println("Filter $filterName was successfully applied to the $inputPath, you should find the result here - $outputPath.")
        } catch (e: Exception) {
            val msg = e.message
            if (msg != null) {
                println("Error: $msg.")
            } else {
                println("Unexpected error.")
            }
            println("Please, try again...")
        }
    }

    val FILTERS = mapOf(
        "gauss_3" to Gauss3Filter,
        "gauss_5" to Gauss5Filter,
        "gray_scale" to GrayScaleFilter,
        "median_3" to MedianFilter(size = 3),
        "sobel_x" to SobelXFilter,
        "sobel_y" to SobelYFilter,
    )
}
