package tokenizer;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import java.util.ArrayList;
import java.util.Arrays;

import static org.junit.jupiter.api.Assertions.*;

class TokenizerTest {
	private Tokenizer tokenizer;
	@BeforeEach
	void setUp() {
		tokenizer = new Tokenizer();
	}

	@Test
	void simpleLexemesTest() {
		String input = "echo 123 12345 | pwd | exit";
		assertEquals(Arrays.asList(input.split(" ")), tokenizer.tokenize(String.join(" ", input)));
	}

	@Test
	void simpleLexemesWithVariables() {
		String input = "variable=124 | echo 123 | wc";
		assertEquals(
				Arrays.asList("variable", "=", "124", "|", "echo", "123", "|", "wc"),
				tokenizer.tokenize(input)
		);
	}

	@Test
	void lexemesWithoutSpaces() {
		String input = "$a|wc";
		assertEquals(
				Arrays.asList("$", "a", "|", "wc"),
				tokenizer.tokenize(input)
		);
	}
}