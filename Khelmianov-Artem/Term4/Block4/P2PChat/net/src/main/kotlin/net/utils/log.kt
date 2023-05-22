package net.utils

import java.util.logging.*

class MessageOnly : Formatter() {
    override fun format(record: LogRecord): String {
        return "${record.level}: [${record.sourceMethodName}] ${record.message}\n"
    }
}

val logger: Logger = Logger.getAnonymousLogger().apply {
    val lvl = Level.WARNING
    level = lvl
    useParentHandlers = false
    addHandler(ConsoleHandler().apply {
        level = lvl
        formatter = MessageOnly()
    })
}
