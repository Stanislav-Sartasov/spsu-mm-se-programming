package BashProject.tokenizer;

import BashProject.tokenizer.Lexeme.Lexeme;
import BashProject.tokenizer.Lexeme.LexemeType;
import org.springframework.stereotype.Component;

import java.util.ArrayList;

@Component
public class BashTokenizer implements Tokenizer {
	private int normalizeIndex(int index, int arrayLength) {
		if (index == -1)
			return arrayLength;
		return index;
	}

	private ArrayList<Lexeme> simpleTokenize(String input) {
		ArrayList<Lexeme> tokens = new ArrayList<>();

		StringBuilder currentToken = new StringBuilder();
		for (int i = 0; i < input.length(); i++) {
			var currentChar = input.charAt(i);

			if (currentChar == '\\') {
				currentToken.append(input.charAt(i + 1));
				i += 1;
				continue;
			}

			if (currentChar == ' ') {
				tokens.add(new Lexeme(LexemeType.ARGUMENT, currentToken.toString()));
				currentToken.setLength(0);

				continue;
			}

			currentToken.append(currentChar);
		}

		tokens.add(new Lexeme(LexemeType.ARGUMENT, currentToken.toString()));
		tokens.removeIf(lexeme -> lexeme.getValue().equals(""));
		return tokens;
	}

	public ArrayList<Lexeme> tokenize(String input) {
		ArrayList<Lexeme> tokens = new ArrayList<>();

		StringBuilder currentToken = new StringBuilder();
		for (int i = 0; i < input.length(); i++) {
			var currentChar = input.charAt(i);
			if (!" |=\\".contains(String.valueOf(currentChar))) {
				currentToken.append(currentChar);
				continue;
			}

			if (currentChar == '\\') {
				currentToken.append(input.charAt(i + 1));
				i++;
				continue;
			}

			if (currentChar == ' ') {
				if (!tokens.isEmpty() && tokens.get(tokens.size() - 1).getValue().equals("=")) {
					tokens.add(new Lexeme(LexemeType.ARGUMENT, currentToken.toString()));
					currentToken.setLength(0);
					continue;
				}

				if (currentToken.isEmpty())
					continue;

				tokens.add(new Lexeme(LexemeType.COMMAND, currentToken.toString()));
				int nextIndex = normalizeIndex(input.indexOf('|', i + 1), input.length());
				tokens.addAll(simpleTokenize(input.substring(i + 1, nextIndex)));
				i = nextIndex - 1;
				currentToken.setLength(0);
				continue;
			}

			if (currentChar == '|') {
				tokens.add(new Lexeme(LexemeType.OPERATOR, String.valueOf(currentChar)));
			}

			if (currentChar == '=') {
				tokens.add(new Lexeme(LexemeType.VARIABLE_IDENTIFIER, currentToken.toString()));
				tokens.add(new Lexeme(LexemeType.OPERATOR, String.valueOf(currentChar)));
			}

			currentToken.setLength(0);
		}

		if (!tokens.isEmpty() && tokens.get(tokens.size() - 1).getValue().equals("=")) {
			tokens.add(new Lexeme(LexemeType.ARGUMENT, currentToken.toString()));
		} else {
			tokens.add(new Lexeme(LexemeType.COMMAND, currentToken.toString()));
		}
		tokens.removeIf(lexeme -> lexeme.getValue().equals(""));
		return tokens;
	}
}
