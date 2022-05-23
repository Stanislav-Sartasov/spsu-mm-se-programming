package BashProject.shellcommand.commands;

import BashProject.shellcommand.Command;

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

		try {
			PrintStream stdout = System.out;

			ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
			PrintStream printStream = new PrintStream(outputStream);
			System.setOut(printStream);

			Runtime.getRuntime().exec(cmdArray);

			System.setOut(stdout);
			return ByteBuffer.wrap(outputStream.toByteArray());
		} catch (IOException e) {
			return ByteBuffer.wrap((name + ": command not found").getBytes());
		}
	}
}
