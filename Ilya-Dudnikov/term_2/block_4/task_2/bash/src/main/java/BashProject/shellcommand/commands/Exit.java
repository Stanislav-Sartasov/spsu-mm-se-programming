package BashProject.shellcommand.commands;

import BashProject.shellcommand.Command;

import java.nio.ByteBuffer;

public class Exit extends Command {
	@Override
	public ByteBuffer run(String ... args) {
		if (args.length == 0) {
			Runtime.getRuntime().exit(0);
		}
		if (args.length > 1) {
			return ByteBuffer.wrap(("Too many arguments" + System.lineSeparator()) .getBytes());
		}

		try {
			Runtime.getRuntime().exit(Integer.parseInt(args[0]));
		} catch (NumberFormatException e) {
			return ByteBuffer.wrap(("Argument must be an integer" + System.lineSeparator()).getBytes());
		}
		return ByteBuffer.wrap(new byte[0]);
	}
}
