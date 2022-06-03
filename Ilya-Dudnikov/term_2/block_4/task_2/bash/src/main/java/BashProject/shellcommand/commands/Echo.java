package BashProject.shellcommand.commands;

import BashProject.shellcommand.Command;

import java.nio.ByteBuffer;

public class Echo extends Command {
	@Override
	public ByteBuffer run(String ... arguments) {
		super.run(arguments);
		return ByteBuffer.wrap((String.join(" ", args) + System.lineSeparator()).getBytes());
	}
}
