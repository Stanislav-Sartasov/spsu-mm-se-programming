package bmp.lib

internal fun String.withSystemEndings(): String = replace(oldValue = "\n", newValue = System.lineSeparator())
