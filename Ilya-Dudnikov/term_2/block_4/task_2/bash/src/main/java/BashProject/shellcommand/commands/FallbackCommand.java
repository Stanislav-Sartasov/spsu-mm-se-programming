package BashProject.shellcommand.commands;

import BashProject.shellcommand.Command;
import BashProject.util.StreamGobbler.StreamGobbler;

import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.PrintStream;
import java.nio.ByteBuffer;

public class FallbackCommand extends Command {
	private String name;

	public FallbackCommand(String name) {
		this.name = name;
	}

	@Override
	public ByteBuffer run(String ... args) {
		String[] cmdArray = new String[args.length + 1];
		cmdArray[0] = name;
		System.arraycopy(args, 0, cmdArray, 1, args.length);

		PrintStream stdout = System.out;

		ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
		PrintStream printStream = new PrintStream(outputStream);
		System.setOut(printStream);

		ByteBuffer returnValue;
		try {
			Process process = Runtime.getRuntime().exec(name, args);
			StreamGobbler streamGobbler = new StreamGobbler(process.getInputStream(), printStream);

			streamGobbler.run();
			returnValue = ByteBuffer.wrap(outputStream.toByteArray());
		} catch (IOException e) {
			returnValue = ByteBuffer.wrap((name + ": command not found" + System.lineSeparator()).getBytes());
		}

		System.setOut(stdout);
		return returnValue;
	}
}
