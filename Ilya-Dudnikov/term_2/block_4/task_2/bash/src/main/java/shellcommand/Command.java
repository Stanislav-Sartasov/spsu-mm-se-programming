package shellcommand;

import java.nio.ByteBuffer;
import java.util.ArrayList;
import java.util.Arrays;

public abstract class Command {
	protected ArrayList<String> args;
	public Command() {}

	public ByteBuffer run(String ... arguments) {
		this.args = new ArrayList<>(Arrays.asList(arguments));
		return null;
	}
}
