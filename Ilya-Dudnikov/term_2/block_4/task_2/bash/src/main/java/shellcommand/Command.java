package shellcommand;

import java.nio.ByteBuffer;
import java.util.ArrayList;
import java.util.Arrays;

public abstract class Command {
	protected ArrayList<String> args;
	public Command() {};

	public Command(String ... args) {
		this.args = new ArrayList<>(Arrays.asList(args));
	}

	public abstract ByteBuffer run();
}
