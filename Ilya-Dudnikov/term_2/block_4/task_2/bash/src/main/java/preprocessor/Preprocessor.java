package preprocessor;

import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.stereotype.Component;
import util.VariableStorage;

@Component
public class Preprocessor {
	private VariableStorage variableStorage;

	@Autowired
	public Preprocessor(VariableStorage variableStorage) {
		this.variableStorage = variableStorage;
	}

	private String stringOrEmpty(String str) {
		if (str == null)
			return "";
		return str;
	}

	public String process(String input) {
		StringBuilder result = new StringBuilder();
		var splittedInput = input.split("\\$");
		for (var chunk : splittedInput) {
			int endOfVariableIndex = chunk.length();
			for (int i = 0; i < chunk.length(); i++) {
				var currentChar = chunk.charAt(i);
				if (currentChar == ' ' || currentChar == '|' || currentChar == '=') {
					endOfVariableIndex = i;
					break;
				}
			}

			result.append(stringOrEmpty(variableStorage.get(chunk.substring(0, endOfVariableIndex))));
			if (endOfVariableIndex != chunk.length())
				result.append(stringOrEmpty(chunk.substring(endOfVariableIndex)));
		}

		return result.toString();
	}
}
