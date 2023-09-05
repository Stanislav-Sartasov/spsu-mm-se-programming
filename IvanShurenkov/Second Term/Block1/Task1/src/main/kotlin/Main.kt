import filters.GaussFilter
import java.io.File
import filters.GrayFilter
import filters.MedianFilter
import filters.SobelFilter

fun main(args: Array<String>) {
    println("This program apply filter on image")
    println("Filters:")
    println("gr 			:translate color to gray")
    println("m3 			:median filter 3x3")
    println("g3 			:Gauss filter 3x3")
    println("sx 			:Sobel filter to axis X")
    println("sy			    :Sobel filter to axis Y")

    if (args.size != 3) {
        println("Few or many arguments")
    }

    val filenameIn = args[0]
    val filenameOut = args[2]
    val file = File(filenameIn)
    if (!file.exists()) {
        println("$filenameIn file does not exist.")
        return
    }
    val bmpFile = BMPFile(file)
    when (args[1]) {
        "gr" -> GrayFilter().applyFilter(bmpFile)
        "g3" -> GaussFilter().applyFilter(bmpFile)
        "m3" -> MedianFilter().applyFilter(bmpFile)
        "sx" -> SobelFilter().applyFilter(bmpFile, "x")
        "sy" -> SobelFilter().applyFilter(bmpFile, "y")
        "sxy" -> SobelFilter().applyFilter(bmpFile, "xy")
    }

    val fileOut = File(filenameOut)
    if (!fileOut.exists())
        if (!fileOut.createNewFile()) {
            println("$filenameOut file can't be create or open")
            return
        }
    bmpFile.writeResult(fileOut)
    println("Image was saved")
    // println("Program arguments: ${args.joinToString()}")
}