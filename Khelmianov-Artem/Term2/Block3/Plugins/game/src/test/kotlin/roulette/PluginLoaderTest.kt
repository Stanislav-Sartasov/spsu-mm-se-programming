package roulette

import org.junit.jupiter.api.Test
import org.junit.jupiter.api.assertThrows
import java.io.FileNotFoundException
import kotlin.test.assertTrue

internal class PluginLoaderTest {

    @Test
    fun `load bot from file`() {

        assertTrue(PluginLoader.loadBotsFromFile(resources + "MartingaleBot.jar").size == 1)
    }

    @Test
    fun `file doesn't exist`() {
        assertThrows<FileNotFoundException> { PluginLoader.loadBotsFromFile(resources + "void.jar").isEmpty() }
    }

    @Test
    fun `file without bots`() {
        assertTrue(PluginLoader.loadBotsFromFile(resources + "gradle-wrapper.jar").isEmpty())
    }

    @Test
    fun `load bots from directory`() {
        println(PluginLoader.loadBotsFromDirectory(resources).size)
        assertTrue(PluginLoader.loadBotsFromDirectory(resources).size == 3)
    }

    companion object {
        const val resources = "src/test/resources/"
    }
}