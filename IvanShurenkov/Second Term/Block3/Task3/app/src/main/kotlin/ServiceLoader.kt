import lib.weather.IWeatherApi
import java.io.FileInputStream
import java.net.URL
import java.net.URLClassLoader
import java.util.jar.JarInputStream

object ServiceLoader {
    fun loadService(pathToJar: String): IWeatherApi? {
        val classesNames = getClassesNames(pathToJar)
        for (className in classesNames) {
            try {
                val cl = URLClassLoader.newInstance(arrayOf(URL("file:${pathToJar}"))).loadClass(className).kotlin
                val service =
                    (cl.objectInstance ?: cl.constructors.find { it.parameters.isEmpty() }?.call()) as? IWeatherApi
                if (service != null)
                    return service
            }
            catch (e: Exception)
            {
                return null
            }
        }
        return null
    }

    private fun getClassesNames(jarName: String): List<String> {
        var classesNames: List<String> = emptyList()
        try {
            val jarFile = JarInputStream(FileInputStream(jarName))
            var jarEntry = jarFile.nextJarEntry
            while (jarEntry != null) {
                if (!jarEntry.isDirectory && jarEntry.name.endsWith(".class")) {
                    classesNames += jarEntry.name.replace('/', '.').dropLast(6)
                }
                jarEntry = jarFile.nextJarEntry
            }
        } catch (e: Exception) {
            return emptyList()
        }
        return classesNames
    }
}
