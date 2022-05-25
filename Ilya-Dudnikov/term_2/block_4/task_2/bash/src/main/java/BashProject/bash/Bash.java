package BashProject.bash;

import BashProject.interpreter.Interpreter;
import BashProject.shellcommand.commands.Cat;
import BashProject.shellcommand.commands.Pwd;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import BashProject.preprocessor.Preprocessor;
import BashProject.shellcommand.CommandList;
import BashProject.shellcommand.commands.Echo;
import BashProject.shellcommand.commands.Wc;
import BashProject.tokenizer.Lexeme.LexemeType;
import BashProject.tokenizer.Tokenizer;

import java.nio.ByteBuffer;
import java.util.ArrayList;
import java.util.NoSuchElementException;
import java.util.Scanner;

@Component
public class Bash {
	private Preprocessor preprocessor;
	private Tokenizer tokenizer;
	private Interpreter interpreter;

	public Bash(
			Preprocessor preprocessor,
			Tokenizer tokenizer,
			Interpreter interpreter
	) {
		this.preprocessor = preprocessor;
		this.tokenizer = tokenizer;
		this.interpreter = interpreter;
	}

	public void run() {
		System.out.println("Hello, this is Bourne again shell! Let's get to work :)");
		System.out.println("Type help to see the list of available commands");

		while (true) {
			System.out.print("> ");
			Scanner scanner = new Scanner(System.in);

			String input = "";
			try {
				input = scanner.nextLine();
			} catch (NoSuchElementException e) {
				break;
			}
			try {
				interpreter.interpret(tokenizer.tokenize(preprocessor.process(input)));
			} catch (IllegalArgumentException e) {
				System.out.println("Error: + " + e.getMessage());
			}
		}
	}
}
