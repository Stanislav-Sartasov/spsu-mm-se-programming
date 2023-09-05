package BashProject.bash;

import BashProject.interpreter.Interpreter;
import BashProject.preprocessor.Preprocessor;
import BashProject.tokenizer.BashTokenizer;
import BashProject.tokenizer.Tokenizer;
import org.springframework.stereotype.Component;

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
