package BashProject.preprocessor;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import BashProject.util.VariableStorage.VariableStorage;

import java.util.ArrayList;

@Component
public class BashPreprocessor implements Preprocessor {
	private VariableStorage variableStorage;
	private static final String SPECIAL_CHARACTERS = "~`$\"' \\#=[]!>|;{}()*?&";

	@Autowired
	public BashPreprocessor(VariableStorage variableStorage) {
		this.variableStorage = variableStorage;
	}

	private String stringOrEmpty(String str) {
		if (str == null)
			return "";
		return str;
	}

	private ArrayList<Boolean> markVariablesToReplace(String input) {
		boolean readingDoubleQuotes = false;
		boolean readingSingleQuotes = false;

		ArrayList<Boolean> shouldBeReplaced = new ArrayList<>();

		for (int i = 0; i < input.length(); i++) {
			char currentChar = input.charAt(i);
			if (!"'\"$".contains(String.valueOf(currentChar))) {
				continue;
			}

			if (currentChar == '$') {
				if (readingDoubleQuotes || !readingSingleQuotes) {
					shouldBeReplaced.add(true);
				} else {
					shouldBeReplaced.add(false);
				}

				continue;
			}

			if (currentChar == '\'') {
				if (readingDoubleQuotes) {
					continue;
				}

				readingSingleQuotes = !readingSingleQuotes;
				continue;
			}

			if (readingSingleQuotes) {
				continue;
			}

			readingDoubleQuotes = !readingDoubleQuotes;
		}

		return shouldBeReplaced;
	}

	private String removeQuotes(String input) {
		boolean readingDoubleQuotes = false;
		boolean readingSingleQuotes = false;

		StringBuilder currentString = new StringBuilder();
		for (int i = 0; i < input.length(); i++) {
			char currentChar = input.charAt(i);
			if (!" \\'\"".contains(String.valueOf(currentChar))) {
				currentString.append(currentChar);
				continue;
			}

			if (currentChar == '\'') {
				if (readingDoubleQuotes) {
					currentString.append(currentChar);
					continue;
				}


				readingSingleQuotes = !readingSingleQuotes;
				continue;
			}

			if (readingSingleQuotes) {
				currentString.append("\\").append(currentChar);
				continue;
			}

			if (currentChar == '\\') {
				if (i + 1 >= input.length() || !SPECIAL_CHARACTERS.contains(String.valueOf(input.charAt(i + 1))) && readingDoubleQuotes) {
					currentString.append("\\\\");
				}

				currentString.append('\\').append(input.charAt(i + 1));
				i++;
				continue;
			}

			if (currentChar == ' ') {
				if (readingSingleQuotes || readingDoubleQuotes) {
					currentString.append("\\ ");
				} else {
					currentString.append(currentChar);
				}

				continue;
			}

			readingDoubleQuotes = !readingDoubleQuotes;
		}

		if (readingDoubleQuotes || readingSingleQuotes) {
			throw new IllegalArgumentException("Unmatched quotes");
		}

		return currentString.toString();
	}

	public String process(String input) {
		StringBuilder result = new StringBuilder();

		ArrayList<Boolean> shouldBeReplaced = markVariablesToReplace(input);

		var splittedInput = input.split("\\$");
		int index = 0;
		for (int chunkIndex = 0; chunkIndex < splittedInput.length; chunkIndex++) {
			var chunk = splittedInput[chunkIndex];
			if (chunk.isBlank() || chunkIndex == 0) {
				result.append(chunk);
				continue;
			}

			if (!shouldBeReplaced.get(index++)) {
				result.append("$").append(chunk);
				continue;
			}

			int endOfVariableIndex = chunk.length();
			for (int i = 0; i < chunk.length(); i++) {
				var currentChar = chunk.charAt(i);
				if (" '\"|".contains(String.valueOf(currentChar))) {
					endOfVariableIndex = i;
					break;
				}
			}

			result.append(stringOrEmpty(variableStorage.get(chunk.substring(0, endOfVariableIndex))));
			if (endOfVariableIndex != chunk.length()) {
				result.append(stringOrEmpty(chunk.substring(endOfVariableIndex)));
			}
		}

		return removeQuotes(result.toString());
	}
}
