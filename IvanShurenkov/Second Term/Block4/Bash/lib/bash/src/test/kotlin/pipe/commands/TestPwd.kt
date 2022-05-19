package pipe.commands

import lib.pipe.Output
import lib.pipe.commands.PwdCommand
import org.junit.jupiter.api.Test
import java.io.File
import java.nio.file.Paths
import kotlin.test.assertEquals
import kotlin.test.assertNull

class TestPwd {
    @Test
    fun `Test pwd`() {
        val path = Paths.get("").toRealPath().toString()

        var ls = ""
        val folders = File(path).listFiles { file -> file.isDirectory }
        folders?.forEach { folder -> ls += "${folder.name}/${System.lineSeparator()}" }
        val files = File(path).listFiles { file -> file.isFile }
        files?.forEach { file -> ls += "${file.name}${System.lineSeparator()}" }
        val ans = "$path${System.lineSeparator()}$ls"

        val output = PwdCommand("pwd").run(emptyArray())
        assertEquals(ans, output.output)
        assertNull(output.error)
        assert(!output.exit)
    }
}