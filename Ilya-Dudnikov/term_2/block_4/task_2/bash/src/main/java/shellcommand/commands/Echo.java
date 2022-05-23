package shellcommand.commands;

import shellcommand.Command;

import java.nio.ByteBuffer;
import java.util.ArrayList;
import java.util.Arrays;

public class Echo extends Command {
	@Override
	public ByteBuffer run(String ... arguments) {
		super.run(arguments);
		return ByteBuffer.wrap((String.join(" ", args) + System.lineSeparator()).getBytes());
	}
}
