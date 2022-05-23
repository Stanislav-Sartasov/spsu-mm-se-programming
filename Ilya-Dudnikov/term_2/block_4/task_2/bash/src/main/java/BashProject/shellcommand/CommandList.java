package BashProject.shellcommand;

import BashProject.shellcommand.commands.*;
import org.springframework.stereotype.Component;

import java.util.HashMap;
import java.util.Map;

@Component
public class CommandList {
	private Map<String, Command> commandList;

	public CommandList() {
		commandList = new HashMap<>();
	}

	public void addInitialCommands() {
		addCommand("echo", new Echo());
		addCommand("cat", new Cat());
		addCommand("wc", new Wc());
		addCommand("pwd", new Pwd());
		addCommand("exit", new Exit());
	}

	public void addCommand(String name, Command command) {
		commandList.put(name, command);
	}

	public void removeCommand(String name) {
		commandList.remove(name);
	}

	public Command getCommand(String name) {
		var command = commandList.get(name);
		if (command == null)
			return new FallbackCommand(name);
		return command;
	}
}
