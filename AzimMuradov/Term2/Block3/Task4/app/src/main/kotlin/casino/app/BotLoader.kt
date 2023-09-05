package casino.app

import casino.lib.blackjack.PlayerStrategy
import java.io.File
import java.net.URL
import java.net.URLClassLoader
import java.util.jar.*
import kotlin.reflect.KClass

object BotLoader {

    fun findJarFilesRecursively(path: String): List<JarFile> = File(path)
        .walk()
        .filter { it.isFile }
        .mapNotNull { runOrNull { JarFile(it) } }
        .toList()

    fun loadBotsFromJarFile(jar: JarFile): List<PlayerStrategy> = runOrNull {
        jar.use { jar ->
            URLClassLoader.newInstance(arrayOf(URL("jar:file:${jar.name}!/"))).use { loader ->
                jar
                    .entries()
                    .asSequence()
                    .mapNotNull { jarEntry -> loader.loadClassOrNull(jarEntry) }
                    .map(Class<*>::kotlin)
                    .mapNotNull(BotLoader::instantiatePlayerStrategyOrNull)
                    .toList()
            }
        }
    } ?: emptyList()


    private fun ClassLoader.loadClassOrNull(jarEntry: JarEntry): Class<*>? = jarEntry
        .takeIf { !it.isDirectory && it.name.endsWith(suffix = ".class") }
        ?.name?.dropLast(n = 6)?.replace(oldChar = '/', newChar = '.')
        ?.let { className -> runOrNull { loadClass(className) } }

    private fun instantiatePlayerStrategyOrNull(kClass: KClass<*>): PlayerStrategy? = runOrNull {
        kClass.objectInstance ?: kClass.constructors.find { it.parameters.isEmpty() }?.call()
    } as? PlayerStrategy

    private fun <T> runOrNull(block: () -> T): T? = try {
        block()
    } catch (e: Throwable) {
        null
    }
}
