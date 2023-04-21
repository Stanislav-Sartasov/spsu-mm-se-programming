package loader

import org.junit.jupiter.api.Assertions.*
import org.junit.jupiter.api.Test

internal class JarFileBotLoaderTest {
	private val loader = JarFileBotLoader()
	private val resourcesPath = "src/test/resources"
	private val correctJar = "correct_bots.jar"
	private val nonexistentJar = "keklol.jar"
	private val emptyJar = "empty.jar"
	private val noClassesJar = "no_class.jar"

	@Test
	fun `loader should load all objects which are instances of given generic and have empty parameter constructor`() {
		val bots = loader.load("$resourcesPath/$correctJar")

		assertEquals(4, bots.size)
		assertNotNull(bots.find { it.name == "Martingale strategy bot" })
		assertNotNull(bots.find { it.name == "Anti-Martingale strategy bot" })
		assertNotNull(bots.find { it.name == "Only even bets bot" })
		assertNotNull(bots.find { it.name == "Only zero bets bot" })
	}

	@Test
	fun `load() should throw NoSuchFileException if there is no file by given path`() {
		assertThrows(java.nio.file.NoSuchFileException::class.java) {
			loader.load("$resourcesPath/$nonexistentJar")
		}
	}

	@Test
	fun `load() should return empty list if given jar file contains no classes or objects of given generic type`() {
		val bots = loader.load("$resourcesPath/$noClassesJar")

		assertTrue(bots.isEmpty())
	}

	@Test
	fun `load() should return empty list if given jar file is empty`() {
		val bots = loader.load("$resourcesPath/$emptyJar")

		assertTrue(bots.isEmpty())
	}


}