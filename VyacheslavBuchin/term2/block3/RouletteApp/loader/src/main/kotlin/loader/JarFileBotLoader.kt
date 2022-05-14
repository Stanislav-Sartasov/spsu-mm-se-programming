package loader

import bot.BotPlayer
import java.io.File
import java.net.URL
import java.net.URLClassLoader
import java.util.jar.JarFile
import java.util.zip.ZipException
import kotlin.reflect.KClass
import kotlin.reflect.full.isSubclassOf

class JarFileBotLoader : Loader<BotPlayer> {

	private val classFileExtension = ".class"

	override fun load(path: String) =
		try {
			loadAllInstances(path)
		} catch (zipe: ZipException) {
			emptyList()
		}

	private fun loadAllInstances(path: String): List<BotPlayer> {
		JarFile(File(path)).use { jarFile ->
			URLClassLoader.newInstance(arrayOf(URL("jar:file:${jarFile.name}!/"))).use { loader ->
				return jarFile
					.classNames
					.mapNotNull { loader.loadInstance(it) }
			}
		}
	}

	private val JarFile.classNames: List<String>
		get() =
			entries()
				.asSequence()
				.filter { !it.isDirectory && it.name.endsWith(classFileExtension) }
				.map {
					it.name
						.dropLast(classFileExtension.length)
						.replace('/', '.')
				}
				.toList()

	private fun URLClassLoader.loadInstance(className: String): BotPlayer? =
		try {
			val kClass = this.loadClass(className).kotlin
			getInstanceOrNull(kClass)
		} catch (e: NoClassDefFoundError) {
			null
		} catch (e: ClassNotFoundException) {
			null
		}

	private fun getInstanceOrNull(kClass: KClass<*>): BotPlayer? {
		if (!kClass.isSubclassOf(BotPlayer::class))
			return null
		return (kClass.objectInstance ?: kClass.constructors.find { it.parameters.isEmpty() }?.call()) as? BotPlayer
	}
}
