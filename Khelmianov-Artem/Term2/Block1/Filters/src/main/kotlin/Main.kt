package bmp

import bmp.bmp.filters.*

fun main(args: Array<String>) {
    println(
        """Программа применяет фильтр
        |Использование: gradlew :run --args='-i input.bmp -o output.bmp -f gauss [radius=1]'
        |Доступные фильтры: gray, sobelx, sobley, gauss, median
        |Для gauss и median можно указать произвольный радиус ядра < 8""".trimMargin()
    )

    val arguments = args.fold(Pair(emptyMap<String, List<String>>(), "")) { (map, lastKey), elem ->
        if (elem.startsWith("-")) Pair(map + (elem to emptyList()), elem)
        else Pair(map + (lastKey to map.getOrDefault(lastKey, emptyList()) + elem), lastKey)
    }.first

    val input: String = arguments["-i"]?.getOrNull(0) ?: throw Error("Не указан входной файл")
    val output: String = arguments["-o"]?.getOrNull(0) ?: throw Error("Не указан выходной файл")
    val _filter: String = arguments["-f"]?.getOrNull(0) ?: throw Error("Не указан фильтр")
    val radius: Int = arguments["-f"]?.getOrNull(1)?.toIntOrNull() ?: 1

    val filters = mapOf(
        "median" to MedianFilter(radius),
        "gray" to GrayscaleFilter,
        "sobelx" to SobelXFilter,
        "sobely" to SobelYFilter,
        "gauss" to GaussianFilter(radius)
    )

    val filter = filters.getOrElse(_filter) { throw IllegalArgumentException("Неверное имя фильтра") }
    val bmp = BMPImage.open(input) ?: throw Error("Не удалось открыть изображение")
    bmp.applyFilter(filter)
    bmp.save(output)
}
