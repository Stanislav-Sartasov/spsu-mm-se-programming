package BashProject.shellcommand.commands;

import BashProject.shellcommand.Command;

import java.io.*;
import java.nio.ByteBuffer;

public class Cat extends Command {
	private String executeCat(File file) {
		if (file != null && file.isDirectory()) {
			return "";
		}

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
			String result = new String(inputStream.readAllBytes());
			inputStream.close();
			return result;
		} catch (IOException e) {
			throw new RuntimeException(e);
		}
	}

	@Override
	public ByteBuffer run(String ... arguments) {
		super.run(arguments);
		if (args.isEmpty()) {
			return ByteBuffer.wrap((executeCat(null) + System.lineSeparator()).getBytes());
		}

		StringBuilder result = new StringBuilder();
		for (int i = 0; i < args.size(); i++) {
			var fileName = args.get(i);
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
						.append("cat: ")
						.append(fileName)
						.append(": Is a directory");
			}

			result.append(executeCat(file)).append(System.lineSeparator());
		}

		return ByteBuffer.wrap(result.toString().getBytes());
	}
}
