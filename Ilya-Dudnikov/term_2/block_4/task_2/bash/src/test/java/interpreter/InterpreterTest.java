package interpreter;

import BashProject.interpreter.Interpreter;
import BashProject.preprocessor.Preprocessor;
import BashProject.shellcommand.CommandList;
import BashProject.tokenizer.Tokenizer;
import BashProject.util.VariableStorage.SimpleVariableStorage;
import BashProject.util.VariableStorage.VariableStorage;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.io.*;
import java.nio.ByteBuffer;

import static org.junit.jupiter.api.Assertions.assertEquals;
import static org.junit.jupiter.api.Assertions.assertTrue;

class InterpreterTest {
	private static final String RESOURCES_PATH = "src/test/resources/";
	private Interpreter interpreter;
	private Tokenizer tokenizer;
	private Preprocessor preprocessor;
	private VariableStorage variableStorage;

	private ByteArrayOutputStream byteArrayOutputStream;

	@BeforeEach
	void setUp() {
		var commandList = new CommandList();
		commandList.addInitialCommands();

		this.variableStorage = new SimpleVariableStorage();
		this.preprocessor = new Preprocessor(variableStorage);
		this.tokenizer = new Tokenizer();
		this.interpreter = new Interpreter(commandList, variableStorage);
		this.byteArrayOutputStream = new ByteArrayOutputStream();

		System.setOut(new PrintStream(byteArrayOutputStream));
	}

	@Test
	void simpleEcho() {
		String input = "echo 123";

		interpreter.interpret(tokenizer.tokenize(input));

		assertEquals("123" + System.lineSeparator(), byteArrayOutputStream.toString());
	}

	@Test
	void simplePwd() {
		String input = "pwd";

		interpreter.interpret(tokenizer.tokenize(input));

		assertEquals(System.getProperty("user.dir") + System.lineSeparator(), byteArrayOutputStream.toString());
	}

	@Test
	void simpleWcFile() {
		String input = "wc src/test/resources/gradlew";

		interpreter.interpret(tokenizer.tokenize(input));

		assertEquals("234 1159 8070 src/test/resources/gradlew" + System.lineSeparator(), byteArrayOutputStream.toString());
	}

	@Test
	void simpleWcMultipleFiles() {
		String input = "wc src/test/resources/gradlew src/test/resources/build.gradle";

		interpreter.interpret(tokenizer.tokenize(input));

		assertEquals(
				"234 1159 8070 " + RESOURCES_PATH + "gradlew" + System.lineSeparator() +
						"35 67 1088 " + RESOURCES_PATH + "build.gradle" + System.lineSeparator() +
						"269 1226 9158 total" + System.lineSeparator(),
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

		interpreter.interpret(tokenizer.tokenize(input));
		assertEquals("6 69 460" + System.lineSeparator(), byteArrayOutputStream.toString());
	}

	@Test
	void simpleCatFile() throws IOException {
		String input = "cat " + RESOURCES_PATH + "gradlew";

		File file = new File(RESOURCES_PATH + "gradlew");
		FileInputStream fileInputStream = new FileInputStream(file);

		interpreter.interpret(tokenizer.tokenize(input));

		assertEquals(new String(fileInputStream.readAllBytes()) + System.lineSeparator(), byteArrayOutputStream.toString());

		fileInputStream.close();
	}

	@Test
	void simpleCatMultipleFiles() throws IOException {
		String input = "cat " + RESOURCES_PATH + "build.gradle " + RESOURCES_PATH + "gradlew";

		File buildGradleFile = new File(RESOURCES_PATH + "build.gradle");
		File gradlewFile = new File(RESOURCES_PATH + "gradlew");

		FileInputStream buildGradleInputStream = new FileInputStream(buildGradleFile);
		FileInputStream gradlewInputStream = new FileInputStream(gradlewFile);

		byte[] buildGradleBytes = buildGradleInputStream.readAllBytes();
		byte[] newLine = System.lineSeparator().getBytes();
		byte[] gradlewBytes = gradlewInputStream.readAllBytes();

		byte[] concatenatedBytes = new byte[buildGradleBytes.length + newLine.length + gradlewBytes.length];
		System.arraycopy(buildGradleBytes, 0, concatenatedBytes, 0, buildGradleBytes.length);
		System.arraycopy(newLine, 0, concatenatedBytes, buildGradleBytes.length, newLine.length);
		System.arraycopy(gradlewBytes, 0, concatenatedBytes, buildGradleBytes.length + newLine.length, gradlewBytes.length);

		interpreter.interpret(tokenizer.tokenize(input));
		assertEquals(new String(concatenatedBytes) + System.lineSeparator(), byteArrayOutputStream.toString());

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

		interpreter.interpret(tokenizer.tokenize(input));
		assertEquals(new String(buffer.array()) + System.lineSeparator(), byteArrayOutputStream.toString());
	}

	@Test
	void simpleVariableAssignment() {
		String input = "a=3";
		interpreter.interpret(tokenizer.tokenize(input));

		assertEquals(variableStorage.get("a"), "3");
	}

	@Test
	void simpleVariableUsage() {
		String assignment = "a=3";
		String retrieval = "echo $a";

		interpreter.interpret(tokenizer.tokenize(preprocessor.process(assignment)));
		interpreter.interpret(tokenizer.tokenize(preprocessor.process(retrieval)));

		assertEquals("3" + System.lineSeparator(), byteArrayOutputStream.toString());
	}

	@Test
	void fallbackNonExistentCommand() {
		String input = "No such command 123";

		interpreter.interpret(tokenizer.tokenize(input));
		assertEquals("No: command not found" + System.lineSeparator(), byteArrayOutputStream.toString());
	}

	@Test
	void fallbackExistentCommand(){
		String input = "whoami";

		interpreter.interpret(tokenizer.tokenize(input));
		assertTrue(byteArrayOutputStream.toString().contains(System.getProperty("user.name")));
	}

	@Test
	void commandsWithPipe() {
		String input = "echo 123 | wc";

		interpreter.interpret(tokenizer.tokenize(input));
		assertEquals("1 1 5", byteArrayOutputStream.toString());
	}
}