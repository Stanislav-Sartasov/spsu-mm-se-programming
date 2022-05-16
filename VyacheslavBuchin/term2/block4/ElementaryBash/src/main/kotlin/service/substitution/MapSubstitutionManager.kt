package service.substitution

class MapSubstitutionManager : SubstitutionManager {
	private val substitutions = mutableMapOf<String, String>()

	override operator fun get(name: String): String {
		if (name.isEmpty() || name.contains("\\s".toRegex()))
			throw SubstitutionException("\${$name}: bad substitution")
		return substitutions[name] ?: ""
	}

	override operator fun set(name: String, value: String) {
		substitutions[name] = value
	}
}