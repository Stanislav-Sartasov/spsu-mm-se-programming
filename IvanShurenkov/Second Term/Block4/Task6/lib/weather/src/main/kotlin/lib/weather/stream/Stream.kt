package lib.weather.stream

import lib.weather.date.Weather

open class Stream {
    open fun print(input: String) {
        println(input)
    }

    open fun print(input: Weather) {
        println(input)
    }

    open fun printErr(input: String) {
        println("Error: $input")
    }

    open fun read(): Command {
        /*val command = Command()
        kotlin.io.print("\rEnter u - to update; q - to exit; a <path to jar> <apikey> - to add service: ")
        val enter = (readLine()!!).split(' ')
        if (enter.isEmpty())
            return command
        if (enter[0] == "u")
            command.action = Action.UPDATE
        else if (enter[0] == "q")
            command.action = Action.EXIT
        else if (enter[0] == "a" && enter.size == 3) {
            command.action = Action.ADD
            command.commandArgs = listOf(enter[1], enter[2])
        }*/
        return Command()
    }
}