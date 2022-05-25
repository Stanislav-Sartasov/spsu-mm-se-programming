package BashProject.shellcommand.commands;

import BashProject.shellcommand.Command;

import java.io.File;
import java.nio.ByteBuffer;
import java.nio.file.Path;
import java.util.Arrays;
import java.util.stream.Collectors;

public class Ls extends Command {
	private String listFiles(String directory) {
		if (directory.isBlank() || directory.charAt(0) != '/') {
			directory = Path.of(System.getProperty("user.dir"), directory).toString();
		}

		File file = new File(directory);
		if (!file.exists()) {
			throw new IllegalArgumentException("No such file or directory");
		}

		if (!file.isDirectory()) {
			return directory;
		}

		var filesInDirectory = file.listFiles();
		if (filesInDirectory == null) {
			return "";
		}


		return Arrays.stream(filesInDirectory)
				.map(File::getName)
				.collect(Collectors.joining(" "));
	}

	@Override
	public ByteBuffer run(String ... args) {
		if (args.length == 0) {
			return ByteBuffer.wrap((listFiles("") + System.lineSeparator()).getBytes());
		}

		StringBuilder result = new StringBuilder();
		for (String arg : args) {
			try {
				String listedFiles = listFiles(arg);

				if (args.length > 1) {
					result
							.append(arg)
							.append(":")
							.append(System.lineSeparator());
				}
				result.append(listedFiles);
			} catch (IllegalArgumentException e) {
				result
						.append("ls: cannot access '")
						.append(arg)
						.append("': No such file or directory");
			}

			result.append(System.lineSeparator());
		}

		return ByteBuffer.wrap(result.toString().getBytes());
	}
}
