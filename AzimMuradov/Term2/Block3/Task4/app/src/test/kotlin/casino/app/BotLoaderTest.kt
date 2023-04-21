package casino.app

import casino.lib.blackjack.bots.basic.BasicStrategy
import org.junit.jupiter.api.Test
import org.junit.jupiter.params.ParameterizedTest
import org.junit.jupiter.params.provider.Arguments.arguments
import org.junit.jupiter.params.provider.MethodSource
import java.io.File
import java.util.jar.*
import kotlin.io.path.Path
import kotlin.io.path.name
import kotlin.test.assertEquals
import kotlin.test.assertIs

internal class BotLoaderTest {

    @ParameterizedTest
    @MethodSource("pathsWithJarNames")
    fun `find jar files recursively`(path: String, jarNames: Set<String>) {
        val files = BotLoader.findJarFilesRecursively(path)
        assertEquals(
            expected = jarNames,
            actual = files.mapTo(mutableSetOf()) { Path(it.name).fileName.name }
        )
    }


    // Load bots from jar file

    @Test
    fun `load basic strategy from bot-basic lib`() {
        val botBasic = BotLoader.loadBotsFromJarFile(
            JarFile("$ROOT${SEP}bot-basic.jar")
        ).first()
        assertIs<BasicStrategy>(botBasic)
    }

    @Test
    fun `load no bots from base lib`() {
        val bots = BotLoader.loadBotsFromJarFile(
            JarFile("$ROOT${SEP}base.jar")
        )
        assertEquals(expected = emptyList(), actual = bots)
    }

    @Test
    fun `load no bots from empty jar`() {
        val bots = BotLoader.loadBotsFromJarFile(
            JarFile("$ROOT${SEP}empty.jar")
        )
        assertEquals(expected = emptyList(), actual = bots)
    }


    private companion object {

        val SEP = File.separatorChar

        val ROOT = "src${SEP}test${SEP}resources${SEP}jars"

        @JvmStatic
        fun pathsWithJarNames() = listOf(
            ROOT to setOf("base.jar", "bot-basic.jar", "empty.jar"),
            "$ROOT${SEP}base.jar" to setOf("base.jar"),
            "$ROOT${SEP}---" to emptySet()
        ).map { (a, b) -> arguments(a, b) }
    }
}
