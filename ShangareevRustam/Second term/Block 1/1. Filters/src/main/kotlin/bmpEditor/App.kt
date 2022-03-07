package bmpEditor

import bmpEditor.bmp.*
import kotlin.system.exitProcess

fun inputPrompt() {
	println("To start, use the template: gradlew :run --args = \"<input bmp path> <filter name> <output bmp path>\"")
	println("Available filters: gray, sobelX, sobelY, median, gauss")
	println()
}

private fun input(args: Array<String>): List<String> {
	if (args.size != 3) {
		println("Input does not match input template. Try again")
		inputPrompt()
		exitProcess(0)
	}
	return listOf(args[0], args[1], args[2])
}

fun runApp(args: Array<String>) {
	inputPrompt()
	val (inputFilePath, filterName, outputFilePath) = input(args)
	val myPicture: Bmp = readBmp(inputFilePath)
	myPicture.applyFilter(filterName)
	myPicture.write(outputFilePath)
	println("Successfully! Filter ${args[1]} applied to bmp image ${args[0]}, result saved in ${args[2]}")
}