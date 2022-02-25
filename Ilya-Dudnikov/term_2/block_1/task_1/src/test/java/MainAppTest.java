import org.junit.jupiter.api.Test;

import java.io.ByteArrayOutputStream;
import java.io.PrintStream;

import static org.junit.jupiter.api.Assertions.*;

class MainAppTest {
	String introMessage = "This app applies filter to a .bmp file\n";

	@Test
	void testHelp1() {
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		PrintStream newPrintStream = new PrintStream(byteArrayOutputStream);
		System.setOut(newPrintStream);

		MainApp.run(new String[] {"--help"});

		assertEquals(
				(introMessage +
				"Usage: gradlew clean build run <filter name> <input file> <output file>\n" +
				"Available filters: grayscale, average, gaussian, sobelX, sobelY\n")
						.replaceAll("\\n|\\r\\n", System.getProperty("line.separator")),
				byteArrayOutputStream.toString()
						.replaceAll("\\n|\\r\\n", System.getProperty("line.separator"))
		);
	}

	@Test
	void testHelp2() {
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		PrintStream newPrintStream = new PrintStream(byteArrayOutputStream);
		System.setOut(newPrintStream);

		MainApp.run(new String[] {"-h"});

		assertEquals(
				(introMessage +
				"Usage: gradlew clean build run <filter name> <input file> <output file>\n" +
				"Available filters: grayscale, average, gaussian, sobelX, sobelY\n")
						.replaceAll("\\n|\\r\\n", System.getProperty("line.separator")),
				byteArrayOutputStream.toString()
						.replaceAll("\\n|\\r\\n", System.getProperty("line.separator"))
		);
	}

	@Test
	void runLessArguments() {
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		PrintStream newPrintStream = new PrintStream(byteArrayOutputStream);
		System.setErr(newPrintStream);

		MainApp.run(new String[] {"haha"});

		assertEquals(
				"Got less arguments than expected: use -h or --help for more information\n"
						.replaceAll("\\n|\\r\\n", System.getProperty("line.separator")),
				byteArrayOutputStream.toString()
						.replaceAll("\\n|\\r\\n", System.getProperty("line.separator"))
		);
	}

	@Test
	void runMoreArguments() {
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		PrintStream newPrintStream = new PrintStream(byteArrayOutputStream);
		System.setErr(newPrintStream);

		MainApp.run(new String[] {"haha", "argument", "another one", "one more"});

		assertEquals(
				"Got more arguments than expected: use -h or --help for more information\n"
						.replaceAll("\\n|\\r\\n", System.getProperty("line.separator")),
				byteArrayOutputStream.toString()
						.replaceAll("\\n|\\r\\n", System.getProperty("line.separator"))
		);
	}

	@Test
	void filterNotFound() {
		ByteArrayOutputStream byteArrayOutputStream = new ByteArrayOutputStream();
		PrintStream newPrintStream = new PrintStream(byteArrayOutputStream);
		System.setErr(newPrintStream);

		MainApp.run(new String[] {"NoSuchFilter", "src/test/resources/ok_test.bmp", "src/test/resources/output.bmp"});

		assertEquals(
				"No such filter provided, use -h or --help to see more information\n"
						.replaceAll("\\n|\\r\\n", System.getProperty("line.separator")),
				byteArrayOutputStream.toString()
						.replaceAll("\\n|\\r\\n", System.getProperty("line.separator"))
		);
	}
}