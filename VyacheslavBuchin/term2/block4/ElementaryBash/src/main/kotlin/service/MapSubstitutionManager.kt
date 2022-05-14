package service

class MapSubstitutionManager : SubstitutionManager {
	private val substitutions = mutableMapOf<String, String>()

	override operator fun get(name: String) = substitutions[name] ?: ""

	override operator fun set(name: String, value: String) {
		substitutions[name] = value
	}
}