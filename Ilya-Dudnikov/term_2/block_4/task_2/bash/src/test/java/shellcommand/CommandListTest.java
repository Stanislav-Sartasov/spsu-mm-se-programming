package shellcommand;

import BashProject.shellcommand.CommandList;
import BashProject.shellcommand.commands.*;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class CommandListTest {
	private CommandList commandList;

	@BeforeEach
	void setUp() {
		commandList = new CommandList();
		commandList.addInitialCommands();
	}

	@Test
	void getExistentCommands() {
		assertInstanceOf(Echo.class, commandList.getCommand("echo"));
		assertInstanceOf(Wc.class, commandList.getCommand("wc"));
		assertInstanceOf(Pwd.class, commandList.getCommand("pwd"));
		assertInstanceOf(Exit.class, commandList.getCommand("exit"));
		assertInstanceOf(Cat.class, commandList.getCommand("cat"));
	}

	@Test
	void getNonBuiltinCommandExpectFallbackCommand() {
		assertInstanceOf(FallbackCommand.class, commandList.getCommand("Interesting command"));
	}
}