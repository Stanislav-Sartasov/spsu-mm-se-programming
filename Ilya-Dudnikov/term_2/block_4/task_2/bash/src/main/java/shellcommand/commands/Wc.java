package shellcommand.commands;

import org.javatuples.Triplet;
import shellcommand.Command;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.nio.ByteBuffer;
import java.util.Scanner;

public class Wc extends Command {
	private static final String OUTPUT_FORMAT = "%7d%7d%7d";

	private Triplet<Integer, Integer, Integer> executeWordCount(File file) {
		if (file.isDirectory()) {
			return new Triplet<>(0, 0, 0);
		}

		Scanner scanner;
		if (file == null)
			scanner = new Scanner(System.in);
		else
			scanner = new Scanner(System.in);

		int newLineCount = 0;
		int wordCount = 0;
		int byteCount = 0;
		while (scanner.hasNextLine()) {
			newLineCount++;
			var currentLine = scanner.nextLine();
			byteCount += currentLine.getBytes().length;
			wordCount += currentLine.split(" ").length;
		}

		return new Triplet<>(newLineCount, byteCount, wordCount);
	}

	@Override
	public ByteBuffer run() {
		if (args.isEmpty()) {
			var wcOutput = executeWordCount(null);

			return ByteBuffer.wrap(String.format(OUTPUT_FORMAT, wcOutput.getValue0(), wcOutput.getValue1(), wcOutput.getValue2()).getBytes());
		}

		int totalNewLines = 0;
		int totalWords = 0;
		int totalBytes = 0;

		StringBuilder result = new StringBuilder();
		for (var fileName : args) {
			var file = new File(fileName);

			if (!file.exists()) {
				result
						.append("wc: ")
						.append(fileName)
						.append(": No such file or directory")
						.append(System.lineSeparator());
				continue;
			}

			if (file.isDirectory()) {
				result.append("wc: ")
						.append(fileName)
						.append(": Is a directory");
			}

			var wcOutput = executeWordCount(file);

			totalNewLines += wcOutput.getValue0();
			totalWords += wcOutput.getValue1();
			totalBytes += wcOutput.getValue2();

			result.append(
					String.format(OUTPUT_FORMAT, wcOutput.getValue0(), wcOutput.getValue1(), wcOutput.getValue2())
			).append(System.lineSeparator());
		}

		result.append(String.format(OUTPUT_FORMAT, totalNewLines, totalWords, totalBytes));
		return ByteBuffer.wrap(result.toString().getBytes());
	}
}
