package shellcommand.commands;

import org.javatuples.Triplet;
import shellcommand.Command;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.nio.ByteBuffer;
import java.util.Arrays;
import java.util.Scanner;



public class Cat extends Command {
	private String executeCat(File file) {
		if (file.isDirectory()) {
			return "";
		}

		Scanner scanner;
		if (file == null)
			scanner = new Scanner(System.in);
		else
			scanner = new Scanner(System.in);

		StringBuilder result = new StringBuilder();
		while (scanner.hasNextLine()) {
			result.append(scanner.nextLine());
		}

		return result.toString();
	}

	@Override
	public ByteBuffer run() {
		if (args.isEmpty()) {
			return ByteBuffer.wrap((executeCat(null).getBytes()));
		}

		StringBuilder result = new StringBuilder();
		for (var fileName : args) {
			File file = new File(fileName);

			if (!file.exists()) {
				result
						.append("cat: ")
						.append(fileName)
						.append(": No such file or directory")
						.append(System.lineSeparator());
				continue;
			}

			if (file.isDirectory()) {
				result
						.append("wc: ")
						.append(fileName)
						.append(": Is a directory");
			}

			result.append(executeCat(file)).append(System.lineSeparator());
		}

		return ByteBuffer.wrap(result.toString().getBytes());
	}
}
