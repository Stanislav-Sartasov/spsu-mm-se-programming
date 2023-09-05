package BashProject.shellcommand.commands;

import org.javatuples.Triplet;
import BashProject.shellcommand.Command;

import java.io.*;
import java.nio.ByteBuffer;
import java.util.Arrays;

public class Wc extends Command {
	private static final String STDIN_OUTPUT_FORMAT = "%d %d %d";
	private static final String OUTPUT_FORMAT = "%d %d %d %s";

	private Triplet<Integer, Integer, Integer> executeWordCount(File file) {
		if (file != null && file.isDirectory()) {
			return new Triplet<>(0, 0, 0);
		}

		int newLineCount = 0;
		int wordCount = 0;
		int byteCount = 0;

		InputStream inputStream;
		if (file == null) {
			inputStream = System.in;
		} else {
			try {
				inputStream = new FileInputStream(file);
			} catch (FileNotFoundException e) {
				throw new RuntimeException(e);
			}
		}

		try {
			byte[] inputBytes = inputStream.readAllBytes();
			String input = new String(inputBytes);
			newLineCount = (int) input.chars().filter(elem -> elem == '\n').count();
			wordCount = (int) Arrays.stream(input.split("[ \n]")).filter(elem -> !elem.isBlank()).count();
			byteCount = input.length();

			inputStream.close();
		} catch (IOException e) {
			throw new RuntimeException(e);
		}

		return new Triplet<>(newLineCount, wordCount, byteCount);
	}

	@Override
	public ByteBuffer run(String ... arguments) {
		super.run(arguments);

		if (args.isEmpty()) {
			var wcOutput = executeWordCount(null);

			return ByteBuffer.wrap(
					(String.format(STDIN_OUTPUT_FORMAT, wcOutput.getValue0(), wcOutput.getValue1(), wcOutput.getValue2())
					+ System.lineSeparator()).getBytes()
			);
		}

		int totalNewLines = 0;
		int totalWords = 0;
		int totalBytes = 0;

		StringBuilder result = new StringBuilder();
		for (int i = 0; i < args.size(); i++) {
			String fileName = args.get(i);
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
				result
						.append("wc: ")
						.append(fileName)
						.append(": Is a directory")
						.append(System.lineSeparator());
			}

			var wcOutput = executeWordCount(file);

			totalNewLines += wcOutput.getValue0();
			totalWords += wcOutput.getValue1();
			totalBytes += wcOutput.getValue2();

			result.append(
					String.format(OUTPUT_FORMAT, wcOutput.getValue0(), wcOutput.getValue1(), wcOutput.getValue2(), fileName)
			);

			result.append(System.lineSeparator());
		}

		if (args.size() > 1) {
			result.append(String.format(OUTPUT_FORMAT, totalNewLines, totalWords, totalBytes, "total")).append(System.lineSeparator());
		}
		return ByteBuffer.wrap(result.toString().getBytes());
	}
}
