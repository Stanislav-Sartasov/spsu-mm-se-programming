package casino.app

import casino.lib.blackjack.PlayerStrategy
import java.io.File
import java.net.URL
import java.net.URLClassLoader
import java.util.jar.*

object BotsLoader {

    fun loadAllBots(path: String): List<PlayerStrategy> = findAllJarFiles(path).flatMap(::loadAllBotsFromJarFile)


    private fun findAllJarFiles(path: String): List<String> =
        File(path).walk().filter { it.isFile }.map { it.path }.toList()

    private fun loadAllBotsFromJarFile(jarPath: String): List<PlayerStrategy> = runOrNull {
        JarFile(jarPath).use { jar ->
            URLClassLoader.newInstance(arrayOf(URL("jar:file:$jarPath!/"))).use { loader ->
                jar
                    .entries()
                    .asSequence()
                    .mapNotNull { jarEntry -> loader.loadClassOrNull(jarEntry) }
                    .mapNotNull { clazz -> clazz.instantiatePlayerStrategyOrNull() }
                    .toList()
            }
        }
    } ?: emptyList()

    private fun ClassLoader.loadClassOrNull(jarEntry: JarEntry): Class<*>? = jarEntry
        .takeIf { !it.isDirectory && it.name.endsWith(suffix = ".class") }
        ?.name?.dropLast(n = 6)?.replace(oldChar = '/', newChar = '.')
        ?.let { className -> runOrNull { loadClass(className) } }

    private fun Class<*>.instantiatePlayerStrategyOrNull(): PlayerStrategy? = runOrNull {
        val isKotlinClass = declaredAnnotations.any { it.annotationClass == Metadata::class }

        if (isKotlinClass) {
            with(kotlin) {
                objectInstance ?: constructors.find { it.parameters.isEmpty() }?.call()
            }
        } else {
            constructors.find { it.parameters.isEmpty() }?.newInstance()
        }
    } as? PlayerStrategy

    private fun <T> runOrNull(block: () -> T): T? = try {
        block()
    } catch (e: Throwable) {
        null
    }
}
