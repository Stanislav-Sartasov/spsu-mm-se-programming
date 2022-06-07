import org.junit.jupiter.api.Assertions.assertEquals
import org.junit.jupiter.api.BeforeEach
import org.junit.jupiter.api.Test
import simpleBashInterpreter.SimpleBashInterpreter
import java.nio.file.Paths

internal class SimpleBashInterpreterTests {

	private var interpreter = SimpleBashInterpreter()

	private val filePath =
		"\"${Paths.get(".").toAbsolutePath().toString().dropLast(2) + "\\src\\test\\resources\\main.py"}\""

	@BeforeEach
	fun init() {
		interpreter = SimpleBashInterpreter()
	}

	@Test
	fun `performing assignments and display variables`() {
		interpreter.interpret("a=10", "")
		interpreter.interpret("b=\"32178 y432898 23du23\"", "")
		interpreter.interpret("c=897993", "")

		val a = "10"
		val b = "32178 y432898 23du23"
		val c = "897993"

		assertEquals(
			"fkdsjklfjoierjfioej $a fjer fjer $b klf eorijf $c ioer\$f",
			interpreter.interpret(
				"echo \"fkdsjklfjoierjfioej \$a fjer fjer \$b klf eorijf \$c ioer\$f\"", ""
			)
		)
	}

	@Test
	fun `getting a working directory`() {
		assertEquals(
			Paths.get(".").toAbsolutePath().toString().dropLast(2),
			interpreter.interpret("pwd", "")
		)
	}

	@Test
	fun `getting the file contents`() {
		val expectedContent = StringBuilder()
			.append("wefjjrfiojerfiojre9043f934of34\n")
			.append("43f9u349fj43iod3oijd43pjzm490dzopj4\n")
			.append("d4z3j0djz3490j43pzod34podj34zzp3o4\n")
			.append("").toString()

		assertEquals(
			expectedContent,
			interpreter.interpret(
				"cat $filePath",
				""
			)
		)
	}

	@Test
	fun `getting information about a file`() {
		val expectedInformation =
			"3 3 103 ${filePath.slice(1 until filePath.length - 1)}"

		assertEquals(
			expectedInformation,
			interpreter.interpret(
				"wc $filePath",
				""
			)
		)
	}

	@Test
	fun `pipelined processing`() {
		assertEquals(
			"",
			interpreter.interpret("echo 123 | exit | cat", "")
		)
	}
}
