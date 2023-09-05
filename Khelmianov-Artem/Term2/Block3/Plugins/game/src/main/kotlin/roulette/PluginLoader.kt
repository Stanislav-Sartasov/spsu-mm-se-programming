package roulette

import java.io.File
import java.io.FileNotFoundException
import java.net.URL
import java.net.URLClassLoader
import java.util.jar.JarFile

object PluginLoader {
    private fun listJarFiles(path: String): List<JarFile> {
        return File(path).walk().filter { it.extension == "jar" }.map { JarFile(it) }.toList()
    }

    private fun loadBotsFromJarFile(jarFile: JarFile): List<Class<*>> = buildList {
        val url = URL("jar:file:${jarFile.name}!/")
        val classLoader = URLClassLoader(arrayOf(url))
        for (className in getClassesFromJar(jarFile)) {
            val cls = classLoader.loadClass(className)
            if (APlayer::class.java.isAssignableFrom(cls)) {
                add(cls)
            }
        }
    }

    private fun getClassesFromJar(jar: JarFile) = buildList {
        for (entry in jar.entries()) {
            if (!entry.isDirectory && entry.name.endsWith(".class") && !entry.name.contains("Companion")) {
                add(entry.name.removeSuffix(".class").replace('/', '.'))
            }
        }
    }

    fun loadBotsFromFile(path: String): List<Class<*>> {
        if (File(path).exists()) {
            return loadBotsFromJarFile(JarFile(path))
        } else {
            throw FileNotFoundException()
        }
    }

    fun loadBotsFromDirectory(path: String): List<Class<*>> = buildList {
        for (jarFile in listJarFiles(path)) {
            addAll(loadBotsFromJarFile(jarFile))
        }
    }
}