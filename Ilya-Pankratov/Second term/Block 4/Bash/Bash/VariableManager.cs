namespace Bash
{
    public class VariableManager
    {
        private LocalVariablesStorage storage;

        public VariableManager()
        {
            storage = new LocalVariablesStorage();
        }

        public void AddVariable(string name, string value)
        {
            storage.Add(name, value, false);
        }

        public string GetVariable(string name)
        {
            var variable = Environment.GetEnvironmentVariable(name);

            if (variable == null)
            {
                variable = storage.Get(name);
            }

            return variable;
        }
    }
}
