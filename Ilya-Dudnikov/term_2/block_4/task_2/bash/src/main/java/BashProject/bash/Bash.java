package BashProject.bash;

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
import java.util.Scanner;

@Component
public class Bash {
	private Preprocessor preprocessor;
	private Tokenizer tokenizer;

	protected CommandList commandList;

	@Autowired
	public Bash(Preprocessor preprocessor, Tokenizer tokenizer, CommandList commandList) {
		this.preprocessor = preprocessor;
		this.tokenizer = tokenizer;
		this.commandList = commandList;
	}

	protected void setUp() {
		commandList.addCommand("echo", new Echo());
		commandList.addCommand("cat", new Cat());
		commandList.addCommand("wc", new Wc());
		commandList.addCommand("pwd", new Pwd());
	}

	public void run() {
		setUp();

		System.out.println("Hello, this is born-again shell! Let's get to work :)");
		System.out.println("Type help to see the list of available commands");

		while (true) {
			System.out.print("> ");
			Scanner scanner = new Scanner(System.in);

			String script = scanner.nextLine();
			var tokens = tokenizer.tokenize(preprocessor.process(script));

			ByteBuffer buffer;
			for (int i = 0; i < tokens.size(); i++) {
				var currentToken = tokens.get(i);

				switch (currentToken.getType()) {
					case COMMAND -> {
						ArrayList<String> args = new ArrayList<>();
						i++;
						while (tokens.get(i).getType() == LexemeType.ARGUMENT) {
							args.add(tokens.get(i++).getValue());
						}

						buffer = commandList.getCommand(currentToken.getValue()).run(args.toArray(new String[0]));
					}
					case VARIABLE_IDENTIFIER -> {
					}
					case ARGUMENT -> {
					}
					case OPERATOR -> {
					}
				}
			}
		}
	}
}
