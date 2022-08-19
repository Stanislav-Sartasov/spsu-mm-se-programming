namespace BashComponents;

public class CommandInfo
{
    public String Name { get; }
    public List<String> Arguments { get; private set; }

    public CommandInfo(String name, List<String> arguments)
    {
        Name = name;
        Arguments = arguments;
    }

    public void Prepend(String value)
    {
        Arguments = Arguments.Prepend(value).ToList();
    }

    public void Append(String value)
    {
        Arguments.Add(value);
    }
}