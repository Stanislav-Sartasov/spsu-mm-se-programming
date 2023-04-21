namespace Bash.Interfaces
{
    public interface IVariableManager
    {
        public void SetVariableValue(string name, string value);
        public string? GetVariableValue(string name);
    }
}