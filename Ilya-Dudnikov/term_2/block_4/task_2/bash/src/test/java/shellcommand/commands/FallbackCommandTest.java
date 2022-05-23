package shellcommand.commands;

import BashProject.shellcommand.commands.FallbackCommand;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class FallbackCommandTest {
	@Test
	void unknownCommand() {
		FallbackCommand fallbackCommand = new FallbackCommand("hahaha.exe");
		assertEquals("hahaha.exe: command not found", new String(fallbackCommand.run().array()));
	}
}