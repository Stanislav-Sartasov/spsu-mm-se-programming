package service.substitution

import exception.ElementaryBashException

class MapSubstitutionManager : SubstitutionManager {
	private val substitutions = mutableMapOf<String, String>()

	override operator fun get(name: String): String {
		if (name.isEmpty() || name.contains("\\s".toRegex()))
			throw ElementaryBashException(ElementaryBashException.INVALID_SUBSTITUTION, "\${$name}")
		return substitutions[name] ?: System.getenv(name) ?: ""
	}

	override operator fun set(name: String, value: String) {
		substitutions[name] = value
			.replace("\\\"", "\"")
			.replace("\\'", "'")
			.replace("\"", "\\\"")
			.replace("'", "\\'")
	}
}