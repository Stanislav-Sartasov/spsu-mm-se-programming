package shellcommand.commands;

import shellcommand.Command;

import java.nio.ByteBuffer;
import java.util.ArrayList;
import java.util.Arrays;

public class Echo extends Command {
	public Echo(String ... args) {
		super(args);
	}

	@Override
	public ByteBuffer run() {
		return ByteBuffer.wrap((String.join(" ", args) + System.lineSeparator()).getBytes());
	}
}
