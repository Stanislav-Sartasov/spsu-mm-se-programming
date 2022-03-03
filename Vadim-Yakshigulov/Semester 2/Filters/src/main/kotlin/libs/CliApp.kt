package libs

import libs.filters.*

object CliApp {
    enum class States {
        SUCCESS, INVALID_ARGS_COUNT_ERROR, INVALID_FILTER_NAME_ERROR, INVALID_BMP_IMAGE_ERROR, ANOTHER_ERROR
    }

    private val FILTERS = mapOf(
        "gaussian_3x3" to GaussianFilter3x3,
        "gaussian_5x5" to GaussianFilter5x5,
        "greyscale" to GreyscaleFilter,
        "median" to MedianFilter,
        "sobelX" to SobelXFilter,
        "sobelY" to SobelYFilter,
    )

    fun run(args: Array<String>): States {
        println(
            """
                |This application can filter bmp files with various algorithms.
                |
                |To run application, please, specify input file, filter, and output file.
                |Provided filters: ${FILTERS.keys.joinToString()}.
                |For example:
                |  On Windows: gradlew.bat :run --args="input.bmp median output.bmp"
                |  On any other system: ./gradlew :run --args='input.bmp median output.bmp'
                |
            """.trimMargin(marginPrefix = "|")
        )
        if (args.size != 3) {
            println("Error: expected 3 arguments, got ${args.size}.")
            println("Please, try again...")
            return States.INVALID_ARGS_COUNT_ERROR
        }

        val (inputPath, filterName, outputPath) = args
        val filter = FILTERS[filterName]

        if (filter == null) {
            println("Error: expected one of the available filters (${FILTERS.keys.joinToString()}), got $filterName.")
            println("Please, try again...")
            return States.INVALID_FILTER_NAME_ERROR
        }
        try {
            println("Opening $inputPath file...")
            val img = BmpImage.open(inputPath)

            if (img == null) {
                println("Error: expected 24/32 bits per pixel bmp file")
                println("Please, double check the input file and try again...")
                return States.INVALID_BMP_IMAGE_ERROR
            }

            println("Applying $filterName filter...")
            img.applyFilter(filter)
            println("$filterName filter applied successfully!")
            println("Saving as $outputPath file...")
            img.saveAs(outputPath)
            println("Saved successfully!")

        } catch (e: Exception) {
            val msg = e.message
            if (msg != null) {
                println("Error: $msg.")
            } else {
                println("Unexpected error.")
            }
            println("Please, try again...")
            return States.ANOTHER_ERROR
        }
        return States.SUCCESS

    }
}