package shellcommand.commands;

import BashProject.shellcommand.commands.Echo;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class EchoTest {

	@Test
	void run() {
		String msg = "Hello, it's Putin! Goodbye, my friend!" + System.lineSeparator();
		Echo command = new Echo();

		assertEquals(msg, new String(command.run("Hello, it's Putin!", "Goodbye, my friend!").array()));
	}
}