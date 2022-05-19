package tokenizer;

import java.util.ArrayList;

public class Tokenizer {
	public ArrayList<String> tokenize(String input) {
		ArrayList<String> tokens = new ArrayList<>();

		StringBuilder currentToken = new StringBuilder();
		for (int i = 0; i < input.length(); i++) {
			char currentChar = input.charAt(i);

			if (currentChar == ' ') {
				if (!currentToken.isEmpty())
					tokens.add(currentToken.toString());
				currentToken.setLength(0);

				continue;
			}

			if (currentChar == '|' || currentChar == '$' || currentChar == '=') {
				if (!currentToken.isEmpty())
					tokens.add(currentToken.toString());
				currentToken.setLength(0);
				tokens.add(String.valueOf(currentChar));

				continue;
			}

			if (currentChar == '\\') {
				currentToken.append(input.charAt(i + 1));
				i++;
			} else {
				currentToken.append(currentChar);
			}
		}

		if (!currentToken.isEmpty())
			tokens.add(currentToken.toString());
		return tokens;
	}
}
