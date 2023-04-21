namespace Bash.Utils;

public interface IIOManager
{
	public string Read();

	public void Write(string message);
}