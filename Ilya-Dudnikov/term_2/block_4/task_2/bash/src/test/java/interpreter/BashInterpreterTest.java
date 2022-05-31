package interpreter;

import BashProject.interpreter.BashInterpreter;
import BashProject.preprocessor.BashPreprocessor;
import BashProject.shellcommand.CommandList;
import BashProject.tokenizer.BashTokenizer;
import BashProject.util.VariableStorage.SimpleVariableStorage;
import BashProject.util.VariableStorage.VariableStorage;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.io.*;
import java.nio.ByteBuffer;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertTrue;

class BashInterpreterTest {
	private static final String RESOURCES_PATH = "src/test/resources/";
	private BashInterpreter interpreter;
	private BashTokenizer bashTokenizer;
	private BashPreprocessor bashPreprocessor;
	private VariableStorage variableStorage;

	private ByteArrayOutputStream byteArrayOutputStream;

	@BeforeEach
	void setUp() {
		var commandList = new CommandList();
		commandList.addInitialCommands();

		this.variableStorage = new SimpleVariableStorage();
		this.bashPreprocessor = new BashPreprocessor(variableStorage);
		this.bashTokenizer = new BashTokenizer();
		this.interpreter = new BashInterpreter(commandList, variableStorage);
		this.byteArrayOutputStream = new ByteArrayOutputStream();

		System.setOut(new PrintStream(byteArrayOutputStream));
	}

	@Test
	void simpleEcho() {
		String input = "echo 123";

		interpreter.interpret(bashTokenizer.tokenize(input));

		assertEquals("123" + System.lineSeparator(), byteArrayOutputStream.toString());
	}

	@Test
	void simplePwd() {
		String input = "pwd";

		interpreter.interpret(bashTokenizer.tokenize(input));

		assertEquals(System.getProperty("user.dir") + System.lineSeparator(), byteArrayOutputStream.toString());
	}

	@Test
	void simpleWcFile() {
		String input = "wc src/test/resources/gradlew";

		interpreter.interpret(bashTokenizer.tokenize(input));

		assertEquals("234 1159 8304 src/test/resources/gradlew" + System.lineSeparator(), byteArrayOutputStream.toString());
	}

	@Test
	void simpleWcMultipleFiles() {
		String input = "wc src/test/resources/gradlew src/test/resources/build.gradle";

		interpreter.interpret(bashTokenizer.tokenize(input));

		assertEquals(
				"234 1159 8304 " + RESOURCES_PATH + "gradlew" + System.lineSeparator() +
						"35 67 1088 " + RESOURCES_PATH + "build.gradle" + System.lineSeparator() +
						"269 1226 9392 total" + System.lineSeparator(),
				byteArrayOutputStream.toString()
		);
	}

	@Test
	void simpleWcStdin() {
		ByteBuffer buffer = ByteBuffer.wrap(
				("Lorem ipsum dolor sit amet, consectetur adipiscing elit, " + System.lineSeparator() +
						"sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " + System.lineSeparator() +
						"Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris " + System.lineSeparator() +
						" nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in " + System.lineSeparator() +
						"reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. " + System.lineSeparator() +
						" Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia " + System.lineSeparator() +
						" deserunt mollit anim id est laborum.").getBytes()
		);
		System.setIn(new ByteArrayInputStream(buffer.array()));

		String input = "wc";

		interpreter.interpret(bashTokenizer.tokenize(input));
		assertEquals("6 69 460" + System.lineSeparator(), byteArrayOutputStream.toString());
	}

	@Test
	void simpleCatFile() throws IOException {
		String input = "cat " + RESOURCES_PATH + "gradlew";

		File file = new File(RESOURCES_PATH + "gradlew");
		FileInputStream fileInputStream = new FileInputStream(file);

		interpreter.interpret(bashTokenizer.tokenize(input));

		assertEquals(new String(fileInputStream.readAllBytes()), byteArrayOutputStream.toString());

		fileInputStream.close();
	}

	@Test
	void simpleCatMultipleFiles() throws IOException {
		String input = "cat " + RESOURCES_PATH + "build.gradle " + RESOURCES_PATH + "gradlew";

		File buildGradleFile = new File(RESOURCES_PATH + "build.gradle");
		File gradlewFile = new File(RESOURCES_PATH + "gradlew");

		FileInputStream buildGradleInputStream = new FileInputStream(buildGradleFile);
		FileInputStream gradlewInputStream = new FileInputStream(gradlewFile);

		interpreter.interpret(bashTokenizer.tokenize(input));
		assertEquals(
				new String(buildGradleInputStream.readAllBytes())
				+ System.lineSeparator()
				+ new String(gradlewInputStream.readAllBytes()),
				byteArrayOutputStream.toString()
		);

		buildGradleInputStream.close();
		gradlewInputStream.close();
	}

	@Test
	void simpleCatStdin() {
		ByteBuffer buffer = ByteBuffer.wrap(
				("Lorem ipsum dolor sit amet, consectetur adipiscing elit, " + System.lineSeparator() +
						"sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. " + System.lineSeparator() +
						"Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris " + System.lineSeparator() +
						" nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in " + System.lineSeparator() +
						"reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. " + System.lineSeparator() +
						" Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia " + System.lineSeparator() +
						" deserunt mollit anim id est laborum.").getBytes()
		);
		System.setIn(new ByteArrayInputStream(buffer.array()));

		String input = "cat";

		interpreter.interpret(bashTokenizer.tokenize(input));
		assertEquals(new String(buffer.array()), byteArrayOutputStream.toString());
	}

	@Test
	void simpleVariableAssignment() {
		String input = "a=3";
		interpreter.interpret(bashTokenizer.tokenize(input));

		assertEquals(variableStorage.get("a"), "3");
	}

	@Test
	void simpleVariableUsage() {
		String assignment = "a=3";
		String retrieval = "echo $a";

		interpreter.interpret(bashTokenizer.tokenize(bashPreprocessor.process(assignment)));
		interpreter.interpret(bashTokenizer.tokenize(bashPreprocessor.process(retrieval)));

		assertEquals("3" + System.lineSeparator(), byteArrayOutputStream.toString());
	}

	@Test
	void fallbackNonExistentCommand() {
		String input = "No such command 123";

		interpreter.interpret(bashTokenizer.tokenize(input));
		assertEquals("No: command not found" + System.lineSeparator(), byteArrayOutputStream.toString());
	}

	@Test
	void fallbackExistentCommand(){
		String input = "whoami";

		interpreter.interpret(bashTokenizer.tokenize(input));
		assertTrue(byteArrayOutputStream.toString().contains(System.getProperty("user.name")));
	}

	@Test
	void commandsWithPipe() {
		String input = "echo 123 | wc";

		interpreter.interpret(bashTokenizer.tokenize(input));
		assertEquals("1 1 5" + System.lineSeparator(), byteArrayOutputStream.toString());
	}
}