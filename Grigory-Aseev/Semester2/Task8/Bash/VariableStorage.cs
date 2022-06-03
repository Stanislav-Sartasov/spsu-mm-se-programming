namespace Bash
{
    public class VariableStorage
    {
        private Dictionary<string, string> storage;

        public VariableStorage()
        {
            storage = new Dictionary<string, string>();
        }

        public void Add(string name, string value)
        {
            if (storage.ContainsKey(name))
            {
                storage[name] = value;
            }
            else
            {
                storage.Add(name, value);
            }
        }

        public string Get(string name) => storage.ContainsKey(name) ? storage[name] : string.Empty;
    }
}