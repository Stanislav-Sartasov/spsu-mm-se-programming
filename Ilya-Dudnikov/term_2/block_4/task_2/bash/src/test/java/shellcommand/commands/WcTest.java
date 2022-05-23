package shellcommand.commands;

import org.javatuples.Triplet;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.io.ByteArrayInputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;

import static org.junit.jupiter.api.Assertions.*;

class WcTest {
	private static final String RESOURCES_PATH = "src/test/resources/";
	private Wc wc;

	@BeforeEach
	void setUp() {
		wc = new Wc();
	}

	@Test
	void wordCountStandardInput() {
		String input = "Hello, it's me! How are y'all?";
		ByteArrayInputStream inputStream = new ByteArrayInputStream(input.getBytes());
		System.setIn(inputStream);

		assertEquals("0 6 30", new String(wc.run().array()));
	}

	@Test
	void multilineStandardInput() {
		String input = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, " + System.lineSeparator() +
				"sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " + System.lineSeparator() +
				"Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris " + System.lineSeparator() +
				" nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in " + System.lineSeparator() +
				"reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. " + System.lineSeparator() +
				" Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia " + System.lineSeparator() +
				" deserunt mollit anim id est laborum.";

		ByteArrayInputStream inputStream = new ByteArrayInputStream(input.getBytes());
		System.setIn(inputStream);

		assertEquals("6 69 460", new String(wc.run().array()));
	}

	@Test
	void fileWc() {
		assertEquals("234 1159 8070 src/test/resources/gradlew" + System.lineSeparator(), new String(wc.run(RESOURCES_PATH + "gradlew").array()));
	}

	@Test
	void multipleFilesWc() {
		assertEquals(
				"35 67 1088 " + RESOURCES_PATH + "build.gradle" + System.lineSeparator() +
						"234 1159 8070 " + RESOURCES_PATH + "gradlew" + System.lineSeparator() +
						"269 1226 9158 total" + System.lineSeparator(),
				new String(wc.run(RESOURCES_PATH + "build.gradle", RESOURCES_PATH + "gradlew").array())
		);
	}

	@Test
	void fileNotFound() {
		String fileName = "NoSuchFile.txt";
		assertEquals(
				"wc: " + fileName + ": No such file or directory" + System.lineSeparator(),
				new String(wc.run(fileName).array())
		);
	}

	@Test
	void givenPathIsDirectory() {
		String path = RESOURCES_PATH + "directory";
		assertEquals(
				"wc: " + path + ": Is a directory" + System.lineSeparator() +
				"0 0 0 " + path + System.lineSeparator(),
				new String(wc.run(path).array())
		);
	}
}