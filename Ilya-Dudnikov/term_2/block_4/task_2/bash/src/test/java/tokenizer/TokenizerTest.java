package tokenizer;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import shellcommand.CommandList;
import shellcommand.commands.Cat;
import shellcommand.commands.Echo;
import shellcommand.commands.Pwd;
import shellcommand.commands.Wc;
import tokenizer.Lexeme.Lexeme;
import tokenizer.Lexeme.LexemeType;

import java.util.Arrays;

import static org.junit.jupiter.api.Assertions.*;

class TokenizerTest {
	private Tokenizer tokenizer;
	@BeforeEach
	void setUp() {
		CommandList commandList = new CommandList();
		commandList.addCommand("echo", new Echo());
		commandList.addCommand("cat", new Cat());
		commandList.addCommand("wc", new Wc());
		commandList.addCommand("pwd", new Pwd());
		tokenizer = new Tokenizer(commandList);
	}

	@Test
	void simpleLexemesTest() {
		String input = "echo 123 12345 | pwd | exit";
		assertEquals(
				Arrays.asList(
						new Lexeme(LexemeType.COMMAND, "echo"),
						new Lexeme(LexemeType.ARGUMENT, "123"),
						new Lexeme(LexemeType.ARGUMENT, "12345"),
						new Lexeme(LexemeType.OPERATOR, "|"),
						new Lexeme(LexemeType.COMMAND, "pwd"),
						new Lexeme(LexemeType.OPERATOR, "|"),
						new Lexeme(LexemeType.COMMAND, "exit")
				),
				tokenizer.tokenize(input)
		);
	}

	@Test
	void simpleLexemesWithVariables() {
		String input = "variable=124 | echo 123 | wc";
		assertEquals(
				Arrays.asList(
						new Lexeme(LexemeType.VARIABLE_IDENTIFIER, "variable"),
						new Lexeme(LexemeType.OPERATOR, "="),
						new Lexeme(LexemeType.ARGUMENT, "124"),
						new Lexeme(LexemeType.OPERATOR, "|"),
						new Lexeme(LexemeType.COMMAND, "echo"),
						new Lexeme(LexemeType.ARGUMENT, "123"),
						new Lexeme(LexemeType.OPERATOR, "|"),
						new Lexeme(LexemeType.COMMAND, "wc")
				),
				tokenizer.tokenize(input)
		);
	}

	@Test
	void lexemesWithoutSpaces() {
		String input = "echo 123|wc";
		assertEquals(
				Arrays.asList(
						new Lexeme(LexemeType.COMMAND, "echo"),
						new Lexeme(LexemeType.ARGUMENT, "123"),
						new Lexeme(LexemeType.OPERATOR, "|"),
						new Lexeme(LexemeType.COMMAND, "wc")
				),
				tokenizer.tokenize(input)
		);
	}

	@Test
	void escapeSymbol() {
		String input = "cat file\\ name |wc";
		assertEquals(
				Arrays.asList(
						new Lexeme(LexemeType.COMMAND, "cat"),
						new Lexeme(LexemeType.ARGUMENT, "file name"),
						new Lexeme(LexemeType.OPERATOR, "|"),
						new Lexeme(LexemeType.COMMAND, "wc")
				),
				tokenizer.tokenize(input)
		);
	}
}