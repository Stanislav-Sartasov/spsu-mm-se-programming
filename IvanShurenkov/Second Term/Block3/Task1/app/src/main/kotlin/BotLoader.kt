import lib.blackjack.base.IPlayer
import java.net.URL
import java.net.URLClassLoader
import java.util.jar.*
import java.io.FileInputStream

class BotLoader {
    fun loadBots(pathsToJars: List<String>): List<IPlayer> {
        var bots: List<IPlayer> = emptyList()
        for (path in pathsToJars) {
            val classesNames = getClassesNames(path)
            for (className in classesNames) {
                val cl = URLClassLoader.newInstance(arrayOf(URL("file:${path}"))).loadClass(className).kotlin
                bots += listOfNotNull((cl.objectInstance ?: cl.constructors.find
                { it.parameters.isEmpty() }?.call()) as? IPlayer)
            }
        }
        return bots
    }

    private fun getClassesNames(jarName: String): List<String> {
        // println("Jar $jarName")
        var classesNames: List<String> = emptyList()
        try {
            val jarFile = JarInputStream(FileInputStream(jarName))
            var jarEntry = jarFile.nextJarEntry
            while (jarEntry != null) {
                if (!jarEntry.isDirectory && jarEntry.name.endsWith(".class")) {
                    // println("Found " + jarEntry.name.replace('/', '.'))
                    classesNames += jarEntry.name.replace('/', '.').dropLast(6)
                }
                jarEntry = jarFile.nextJarEntry
            }
        } catch (e: Exception) {
            return emptyList()
            //e.printStackTrace()
        }
        return classesNames
    }
}