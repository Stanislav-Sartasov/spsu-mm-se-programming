package loader

interface Loader<T> {
	fun load(path: String): List<T>
}