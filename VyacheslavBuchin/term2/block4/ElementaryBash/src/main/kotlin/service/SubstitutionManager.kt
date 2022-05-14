package service

interface SubstitutionManager {
	operator fun get(name: String): String
	operator fun set(name: String, value: String)
}