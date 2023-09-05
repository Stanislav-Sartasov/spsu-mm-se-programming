package service.command

import command.Command

interface CommandManager {
	operator fun get(name: String): Command
	operator fun set(name: String, factory: () -> Command)
}