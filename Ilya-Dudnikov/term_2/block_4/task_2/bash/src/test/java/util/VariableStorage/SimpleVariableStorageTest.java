package util.VariableStorage;

import BashProject.util.VariableStorage.SimpleVariableStorage;
import BashProject.util.VariableStorage.VariableStorage;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class SimpleVariableStorageTest {
	private VariableStorage variableStorage;

	@BeforeEach
	void setUp() {
		variableStorage = new SimpleVariableStorage();
	}

	@Test
	void getExistentVariable() {
		variableStorage.set("Hahaha", "123");

		assertEquals("123", variableStorage.get("Hahaha"));
	}

	@Test
	void getEnvironmentVariable() {
		assertNotNull(variableStorage.get("PATH"));
	}

	@Test
	void getNonExistentVariable() {
		assertNull(variableStorage.get("NoSuchVariable"));
	}
}