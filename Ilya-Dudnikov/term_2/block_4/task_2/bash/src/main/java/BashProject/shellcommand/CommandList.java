package BashProject.shellcommand;

import org.springframework.stereotype.Component;

import java.util.HashMap;
import java.util.Map;

@Component
public class CommandList {
	private Map<String, Command> commandList;

	public CommandList() {
		commandList = new HashMap<>();
	}

	public void addCommand(String name, Command command) {
		commandList.put(name, command);
	}

	public void removeCommand(String name) {
		commandList.remove(name);
	}

	public Command getCommand(String name) {
		return commandList.get(name);
	}
}
