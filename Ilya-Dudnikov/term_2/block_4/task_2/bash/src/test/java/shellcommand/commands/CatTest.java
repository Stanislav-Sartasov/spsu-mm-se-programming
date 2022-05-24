package shellcommand.commands;

import BashProject.shellcommand.commands.Cat;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import javax.annotation.processing.SupportedSourceVersion;
import java.io.*;

import static org.junit.jupiter.api.Assertions.*;

class CatTest {
	private static final String RESOURCES_PATH = "src/test/resources/";
	private Cat cat;

	@BeforeEach
	void setUp() {
		cat = new Cat();
	}

	@Test
	void catStandardInput() {
		String input = "Hello, it's me! How are y'all?";
		ByteArrayInputStream inputStream = new ByteArrayInputStream(input.getBytes());
		System.setIn(inputStream);

		assertArrayEquals((input + System.lineSeparator()).getBytes(), cat.run().array());
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

		System.out.println(input);

		assertArrayEquals((input + System.lineSeparator()).getBytes(), cat.run().array());
	}

	@Test
	void fileCat() throws IOException {
		File file = new File(RESOURCES_PATH + "gradlew");
		FileInputStream fileInputStream = new FileInputStream(file);

		assertArrayEquals(
				(new String(fileInputStream.readAllBytes()) + System.lineSeparator()).getBytes(),
				cat.run(RESOURCES_PATH + "gradlew").array()
		);

		fileInputStream.close();
	}

	@Test
	void multipleFilesCat() throws IOException {
		File buildGradleFile = new File(RESOURCES_PATH + "build.gradle");
		File gradlewFile = new File(RESOURCES_PATH + "gradlew");

		FileInputStream buildGradleInputStream = new FileInputStream(buildGradleFile);
		FileInputStream gradlewInputStream = new FileInputStream(gradlewFile);

		assertArrayEquals(
				(
						new String(buildGradleInputStream.readAllBytes())
						+ System.lineSeparator() + new String(gradlewInputStream.readAllBytes())
						+ System.lineSeparator()
				).getBytes(),
				cat.run(RESOURCES_PATH + "build.gradle", RESOURCES_PATH + "gradlew").array()
		);

		buildGradleInputStream.close();
		gradlewInputStream.close();
	}

	@Test
	void fileNotFound() throws IOException {
		File gradlewFile = new File(RESOURCES_PATH + "gradlew");

		FileInputStream fileInputStream = new FileInputStream(gradlewFile);

		assertArrayEquals(
				(
						"cat: " + RESOURCES_PATH + "haha.txt: No such file or directory"
						+ System.lineSeparator() + new String(fileInputStream.readAllBytes())
						+ System.lineSeparator()
				).getBytes(),
				cat.run(RESOURCES_PATH + "haha.txt", RESOURCES_PATH + "gradlew").array()
		);
		fileInputStream.close();
	}

	@Test
	void givenPathIsDirectory() throws IOException {
		File gradlewFile = new File(RESOURCES_PATH + "gradlew");

		FileInputStream fileInputStream = new FileInputStream(gradlewFile);

		assertArrayEquals(
				(
						"cat: " + RESOURCES_PATH + "directory: Is a directory"
						+ System.lineSeparator() + new String(fileInputStream.readAllBytes())
						+ System.lineSeparator()
				).getBytes(),
				cat.run(RESOURCES_PATH + "directory", RESOURCES_PATH + "gradlew").array()
		);
		fileInputStream.close();
	}
}