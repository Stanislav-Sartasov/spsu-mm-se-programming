namespace Bash
{
    public class LocalVariablesStorage
    {
        private readonly Dictionary<string, string> localVariables;
        private readonly List<string> constVariables;

        public LocalVariablesStorage()
        {
            localVariables = new Dictionary<string, string>();
            constVariables = new List<string>();
        }

        public void Add(string varName, string varValue, bool isConst)
        {
            if (localVariables.ContainsKey(varName))
            {
                if (!constVariables.Contains(varName))
                {
                    localVariables[varName] = varValue;
                }
            }
            else
            {
                localVariables.Add(varName, varValue);

                if (isConst)
                {
                    constVariables.Add(varName);
                }
            }
        }

        public string Get(string varName)
        {
            if (localVariables.ContainsKey(varName))
            {
                return localVariables[varName];
            }
            else
            {
                return String.Empty;
            }
        }
    }
}
