namespace BashComponents;

public class VariableContainer
{
    public int Count { get; private set; } 
    private Dictionary<string, string> container;
    
    public VariableContainer()
    {
        container = new Dictionary<string, string>();
        Count = 0;
    }

    public void Add(string name, string value)
    {
        if (!container.ContainsKey(name))
        {
         container.Add(name, value);
         Count++;
        }
    }

    public string Get(string name)
    {
        if (container.ContainsKey(name)) return container[name];
        else return String.Empty;
    }
}