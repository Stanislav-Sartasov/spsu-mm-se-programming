package lib.weather.stream

open class ErrorStream {
    open fun print(input: String) {
        println("Error: $input")
    }
}