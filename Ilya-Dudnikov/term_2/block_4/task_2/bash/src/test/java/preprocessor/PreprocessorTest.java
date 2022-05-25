package preprocessor;

import BashProject.preprocessor.Preprocessor;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import BashProject.util.VariableStorage.SimpleVariableStorage;
import BashProject.util.VariableStorage.VariableStorage;

import static org.junit.jupiter.api.Assertions.*;

class PreprocessorTest {
	private Preprocessor preprocessor;
	private VariableStorage variableStorage;
	@BeforeEach
	void setUp() {
		variableStorage = new SimpleVariableStorage();
		preprocessor = new Preprocessor(variableStorage);
	}

	@Test
	void simpleProcessingTest() {
		variableStorage.set("a", "echo 123");
		variableStorage.set("b", "wc");
		String input = "$a | $b";

		assertEquals("echo 123 | wc", preprocessor.process(input));
	}

	@Test
	void processingWithoutSpaces() {
		variableStorage.set("a", "echo 123");
		variableStorage.set("b", "wc");
		String input = "$a|$b";

		assertEquals("echo 123|wc", preprocessor.process(input));
	}

	@Test
	void processWithSingleQuotes() {
		variableStorage.set("a", "echo 123");
		String input = "'$a' | wc";

		assertEquals("$a | wc", preprocessor.process(input));
	}

	@Test
	void processWithDoubleQuotes() {
		variableStorage.set("a", "echo 123");
		String input = "\"$a\" | wc";

		assertEquals("echo\\ 123 | wc", preprocessor.process(input));
	}

	@Test
	void quotesInsideQuotes() {
		String input = "echo \"'\"";

		assertEquals("echo '", preprocessor.process(input));
	}

	@Test
	void specialCharactersInSingleQuotes() {
		String input = "cat 'asd\\\\ \\'";

		assertEquals("cat asd\\\\\\\\\\ \\\\", preprocessor.process(input));
	}

	@Test
	void specialCharactersInDoubleQuotes() {
		String input = "wc \"fa\\ \\\"asdf\\\" \"";

		assertEquals("wc fa\\ \\\"asdf\\\"\\ ", preprocessor.process(input));
	}

	@Test
	void unmatchedSingleQuotes() {
		String input = "wc ' asdfasdf";

		assertThrows(IllegalArgumentException.class, () -> preprocessor.process(input));
	}

	@Test
	void unmatchedDoubleQuotes() {
		String input = "wc \" asdf";

		assertThrows(IllegalArgumentException.class, () -> preprocessor.process(input));
	}
}