package lib.weather.stream

import lib.weather.date.Weather

open class OutputStream {
    open fun print(input: String) {
        println(input)
    }

    open fun print(input: Weather) {
        println(input)
    }
}