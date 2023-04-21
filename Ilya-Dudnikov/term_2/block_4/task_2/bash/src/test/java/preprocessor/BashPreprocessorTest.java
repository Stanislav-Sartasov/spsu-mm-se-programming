package preprocessor;

import BashProject.preprocessor.BashPreprocessor;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import BashProject.util.VariableStorage.SimpleVariableStorage;
import BashProject.util.VariableStorage.VariableStorage;

import static org.junit.jupiter.api.Assertions.*;

class BashPreprocessorTest {
	private BashPreprocessor bashPreprocessor;
	private VariableStorage variableStorage;
	@BeforeEach
	void setUp() {
		variableStorage = new SimpleVariableStorage();
		bashPreprocessor = new BashPreprocessor(variableStorage);
	}

	@Test
	void simpleProcessingTest() {
		variableStorage.set("a", "echo 123");
		variableStorage.set("b", "wc");
		String input = "$a | $b";

		assertEquals("echo 123 | wc", bashPreprocessor.process(input));
	}

	@Test
	void processingWithoutSpaces() {
		variableStorage.set("a", "echo 123");
		variableStorage.set("b", "wc");
		String input = "$a|$b";

		assertEquals("echo 123|wc", bashPreprocessor.process(input));
	}

	@Test
	void processWithSingleQuotes() {
		variableStorage.set("a", "echo 123");
		String input = "'$a' | wc";

		assertEquals("$a | wc", bashPreprocessor.process(input));
	}

	@Test
	void processWithDoubleQuotes() {
		variableStorage.set("a", "echo 123");
		String input = "\"$a\" | wc";

		assertEquals("echo\\ 123 | wc", bashPreprocessor.process(input));
	}

	@Test
	void quotesInsideQuotes() {
		String input = "echo \"'\"";

		assertEquals("echo '", bashPreprocessor.process(input));
	}

	@Test
	void specialCharactersInSingleQuotes() {
		String input = "cat 'asd\\\\ \\'";

		assertEquals("cat asd\\\\\\\\\\ \\\\", bashPreprocessor.process(input));
	}

	@Test
	void specialCharactersInDoubleQuotes() {
		String input = "wc \"fa\\ \\\"asdf\\\" \"";

		assertEquals("wc fa\\ \\\"asdf\\\"\\ ", bashPreprocessor.process(input));
	}

	@Test
	void unmatchedSingleQuotes() {
		String input = "wc ' asdfasdf";

		assertThrows(IllegalArgumentException.class, () -> bashPreprocessor.process(input));
	}

	@Test
	void unmatchedDoubleQuotes() {
		String input = "wc \" asdf";

		assertThrows(IllegalArgumentException.class, () -> bashPreprocessor.process(input));
	}
}