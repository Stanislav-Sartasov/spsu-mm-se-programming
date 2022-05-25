namespace Tools;

public static class Runtime
{
    public static Dictionary<string, string> LocalVariables { get; private set; } = new Dictionary<string, string>();

    public static void AddVariable(string name, string value)
    {
        if (LocalVariables.ContainsKey(name))
        {
            LocalVariables[name] = value;
        }
        else
        {
            LocalVariables.Add(name, value);
        }
    }

    public static string? GetVariable(string name)
    {
        if (LocalVariables.ContainsKey(name))
        {
            return LocalVariables[name];
        }
        
        return Environment.GetEnvironmentVariable(name);
    }
}
