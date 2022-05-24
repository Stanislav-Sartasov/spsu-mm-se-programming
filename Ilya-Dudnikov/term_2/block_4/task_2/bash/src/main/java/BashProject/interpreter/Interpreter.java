package BashProject.interpreter;

import BashProject.shellcommand.CommandList;
import BashProject.tokenizer.Lexeme.Lexeme;
import BashProject.tokenizer.Lexeme.LexemeType;
import BashProject.util.VariableStorage.VariableStorage;
import org.springframework.stereotype.Component;

import java.io.ByteArrayInputStream;
import java.io.InputStream;
import java.nio.ByteBuffer;
import java.util.ArrayList;
import java.util.List;

@Component
public class Interpreter {
	private CommandList commandList;
	private VariableStorage variableStorage;

	public Interpreter(CommandList commandList, VariableStorage variableStorage) {
		this.commandList = commandList;
		this.variableStorage = variableStorage;

		commandList.addInitialCommands();
	}

	private ByteBuffer executeCommand(String name, String ... args) {
		return commandList.getCommand(name).run(args);
	}

	private void setVariable(String name, String value) {
		variableStorage.set(name, value);
	}

	public void interpret(List<Lexeme> tokens) {
		tokens.add(new Lexeme(LexemeType.OPERATOR, "|"));
		InputStream stdin = System.in;

		ByteBuffer buffer = ByteBuffer.wrap(new byte[0]);

		boolean firstCommand = true;
		for (int i = 0; i < tokens.size();) {
			var currentToken = tokens.get(i);

			if (currentToken.getType() == LexemeType.COMMAND) {
				ArrayList<String> args = new ArrayList<>();

				i++;
				while (i < tokens.size() && tokens.get(i).getType() == LexemeType.ARGUMENT) {
					args.add(tokens.get(i).getValue());
					i++;
				}


				if (!firstCommand) {
					InputStream inputStream = new ByteArrayInputStream(buffer.array());
					System.setIn(inputStream);
				}

				firstCommand = false;
				buffer = executeCommand(currentToken.getValue(), args.toArray(new String[0]));
				continue;
			}

			if (currentToken.getType() == LexemeType.VARIABLE_IDENTIFIER) {
				i += 2;

				if (i < tokens.size() && tokens.get(i).getType() == LexemeType.ARGUMENT) {
					setVariable(currentToken.getValue(), tokens.get(i).getValue());
					i++;
				}
				continue;
			}
			i++;
		}

		System.setIn(stdin);
		System.out.print(new String(buffer.array()));
	}
}
