package shellcommand.commands;

import BashProject.shellcommand.commands.Exit;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class ExitTest {
	private Exit exitCommand;

	@BeforeEach
	void setUp() {
		exitCommand = new Exit();
	}

	@Test
	void exitWithTooManyArguments() {
		assertEquals("Too many arguments" + System.lineSeparator(), new String(exitCommand.run("123", "123").array()));
	}

	@Test
	void exitWithNonIntegerArgument() {
		assertEquals("Argument must be an integer" + System.lineSeparator(), new String(exitCommand.run("hahaha").array()));
	}
}