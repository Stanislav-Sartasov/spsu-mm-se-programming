package shellcommand.commands;

import BashProject.shellcommand.commands.Ls;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.io.File;
import java.nio.file.Path;
import java.util.Arrays;
import java.util.stream.Collectors;

import static org.junit.jupiter.api.Assertions.*;

class LsTest {
	private static final String RESOURCES_PATH = "src/test/resources/";

	private Ls ls;

	@BeforeEach
	void setUp() {
		ls = new Ls();
	}

	@Test
	void simpleLs() {
		File directory = new File(System.getProperty("user.dir"));

		assertEquals(
				Arrays.stream(directory.listFiles()).map(File::getName).collect(Collectors.joining(" ")) + System.lineSeparator(),
				new String(ls.run().array())
		);
	}

	@Test
	void lsWithSingleArgumentRelativePath() {
		File directory = new File(RESOURCES_PATH + "directory");

		assertEquals(
				Arrays.stream(directory.listFiles()).map(File::getName).collect(Collectors.joining(" ")) + System.lineSeparator(),
				new String(ls.run(RESOURCES_PATH + "directory").array())
		);
	}

	@Test
	void multipleArgumentsRelativePath() {
		File directory = new File(RESOURCES_PATH + "directory");
		File anotherDirectory = new File(RESOURCES_PATH + "anotherDirectory");

		assertEquals(
				RESOURCES_PATH + "directory:" + System.lineSeparator()
				+ Arrays.stream(directory.listFiles()).map(File::getName).collect(Collectors.joining(" ")) + System.lineSeparator()
				+ RESOURCES_PATH + "anotherDirectory:" + System.lineSeparator()
				+ Arrays.stream(anotherDirectory.listFiles()).map(File::getName).collect(Collectors.joining(" ")) + System.lineSeparator(),
				new String(ls.run(RESOURCES_PATH + "directory", RESOURCES_PATH + "anotherDirectory").array())
		);
	}
}