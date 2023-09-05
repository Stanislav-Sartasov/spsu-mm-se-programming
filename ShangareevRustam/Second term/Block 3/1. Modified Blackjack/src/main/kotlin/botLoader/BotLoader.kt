package botLoader

import java.net.URL
import java.net.URLClassLoader

object BotLoader {

	fun load(paths: List<Pair<String, String>>): List<Class<*>> {
		val bots = mutableListOf<Class<*>>()
		val urlClassLoader: URLClassLoader
		try {
			urlClassLoader = URLClassLoader(Array(paths.size) { i -> URL(paths[i].first) })
		}
		catch (ex: Exception) {
			throw IllegalArgumentException("Error! One of the URLs passed is invalid!")
		}
		for ((filePath, importPath) in paths) {
			try {
				bots.add(urlClassLoader.loadClass(importPath))
			}
			catch (e: Exception) {
				throw ClassNotFoundException(
					"Error! One of the passed import paths is invalid: the specified class does not exist!"
				)
			}
		}
		return bots
	}

	fun getClassObjects(classes: List<Class<*>>): List<Any> {
		return classes.map {it.newInstance()}
	}
}
