package lib.weather.stream

import lib.weather.stream.Action.*

open class InputStream {
    open fun read(): Command {
        val command = Command()
        print("\rEnter u - to update; q - to exit; a <path to jar> <apikey> - to add service: ")
        val enter = (readLine()!!).split(' ')
        if (enter.isEmpty())
            return command
        if (enter[0] == "u")
            command.action = UPDATE
        else if (enter[0] == "q")
            command.action = EXIT
        else if (enter[0] == "a" && enter.size == 3) {
            command.action = ADD
            command.commandArgs = listOf(enter[1], enter[2])
        }
        return command
    }
}