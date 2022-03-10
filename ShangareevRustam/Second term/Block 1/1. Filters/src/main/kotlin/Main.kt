package bmpEditor

import bmpEditor.bmp.Bmp
import kotlin.system.exitProcess

fun inputPrompt() {
	println("To start, use the template: gradlew :run --args=\"<input bmp path> <filter name> <output bmp path>\"")
	println("Available filters: gray, sobelX, sobelY, median, gauss")
	println()
}

fun input(args: Array<String>): List<String> {
	if (args.size != 3) {
		println("Input does not match input template. Try again")
		inputPrompt()
		exitProcess(0)
	}
	return listOf(args[0], args[1], args[2])
}

fun bmpCorruptedError() {
	println(
		"The bmp file is corrupted: the file size is less than 54 bytes allocated for headers," +
				" or the bmp header format is incorrect," +
				" or the number of bits per pixel is not 24 or 32. Try again"
	)
	inputPrompt()
	exitProcess(0)
}

fun invalidFilterNameError() {
	println("Invalid filter name! Try again")
	inputPrompt()
	exitProcess(0)
}

fun invalidBmpFileError() {
	println(
		"The bmp file you tried to write to a file is too large" +
				" or has the wrong format: the number of bits per pixel is not 24 or 32"
	)
	inputPrompt()
	exitProcess(0)
}

fun main(args: Array<String>) {
	inputPrompt()
	val (inputFilePath, filterName, outputFilePath) = input(args)
	val myPicture: Bmp? = Bmp.readBmp(inputFilePath)

	if (myPicture == null) bmpCorruptedError()
	if (!myPicture!!.applyFilter(filterName)) invalidFilterNameError()
	if (!myPicture.write(outputFilePath)) invalidBmpFileError()

	println("Successfully! Filter ${args[1]} applied to bmp image ${args[0]}, result saved in ${args[2]}")
}