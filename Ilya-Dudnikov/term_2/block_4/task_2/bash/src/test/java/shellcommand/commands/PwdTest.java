package shellcommand.commands;

import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class PwdTest {

	@Test
	void run() {
		Pwd command = new Pwd();
		assertEquals(System.getenv("user.dir") + System.lineSeparator(), new String(command.run().array()));
	}
}