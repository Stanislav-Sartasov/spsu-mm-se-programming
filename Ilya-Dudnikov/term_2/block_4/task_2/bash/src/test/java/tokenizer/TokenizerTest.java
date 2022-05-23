package tokenizer;

import BashProject.tokenizer.Tokenizer;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;
import BashProject.shellcommand.CommandList;
import BashProject.shellcommand.commands.Cat;
import BashProject.shellcommand.commands.Echo;
import BashProject.shellcommand.commands.Pwd;
import BashProject.shellcommand.commands.Wc;
import BashProject.tokenizer.Lexeme.Lexeme;
import BashProject.tokenizer.Lexeme.LexemeType;

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
	void simplestTest() {
		String input = "echo 123";
		assertEquals(
				Arrays.asList(
						new Lexeme(LexemeType.COMMAND, "echo"),
						new Lexeme(LexemeType.ARGUMENT, "123")
				),
				tokenizer.tokenize(input)
		);
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