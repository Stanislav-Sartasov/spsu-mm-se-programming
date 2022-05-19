package tokenizer;

import org.springframework.stereotype.Component;

import java.util.ArrayList;

@Component
public class Tokenizer {
	public ArrayList<String> tokenize(String input) {
		ArrayList<String> tokens = new ArrayList<>();

		StringBuilder currentToken = new StringBuilder();
		for (int i = 0; i < input.length(); i++) {
			char currentChar = input.charAt(i);

			if (currentChar == ' ' || currentChar == '|') {
				if (!currentToken.isEmpty())
					tokens.add(currentToken.toString());
				currentToken.setLength(0);

				if (currentChar == '|')
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
