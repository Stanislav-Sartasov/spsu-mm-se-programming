namespace Tools;

public interface ICommand
{
    public string Name { get; }

    public bool RequiresArgumentsInStdinMode { get; }

    public bool Execute(List<string>? arguments, out string result, bool stdin, string stdinLine);
}
