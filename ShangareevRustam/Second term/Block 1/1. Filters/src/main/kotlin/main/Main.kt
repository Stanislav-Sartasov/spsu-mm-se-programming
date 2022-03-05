package main

import bmp.*
import kotlin.system.exitProcess

fun inputPrompt() {
	println("To start, use the template: <file name> <input bmp file> <filter name> <output bmp file>")
	println("Available filters: gray, sobelX, sobelY, median, gauss")
	println()
}

private fun input(args: Array<String>): List<String> {
	if (args.size != 3) {
		println("Input does not match input template. Try again")
		inputPrompt()
		exitProcess(-1)
	}
	return listOf(args[0], args[1], args[2])
}

fun main(args: Array<String>) {
	inputPrompt()
	val (inputFilePath, filterName, outputFilePath) = input(args)
	val myPicture: Bmp = readBmp(inputFilePath)
	myPicture.applyFilter(filterName)
	myPicture.write(outputFilePath)
	println("Successfully! Filter ${args[1]} applied to bmp image ${args[0]}, result saved in ${args[2]}")
}