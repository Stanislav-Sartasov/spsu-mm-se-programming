package BashProject.util.VariableStorage;

import org.springframework.context.annotation.Scope;
import org.springframework.stereotype.Component;

import java.util.HashMap;
import java.util.Map;

@Component
@Scope("singleton")
public class SimpleVariableStorage implements VariableStorage {
	private Map<String, String> variables;

	public SimpleVariableStorage() {
		variables = new HashMap<>();
	}

	@Override
	public void set(String variableName, String value) {
		variables.put(variableName, value);
	}

	@Override
	public String get(String variableName) {
		return variables.get(variableName);
	}
}
