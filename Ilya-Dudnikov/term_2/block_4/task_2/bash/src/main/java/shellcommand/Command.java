package shellcommand;

public abstract class Command {
	public Command() {};

	public Command(String ... args) {}

	public abstract void run();
}
