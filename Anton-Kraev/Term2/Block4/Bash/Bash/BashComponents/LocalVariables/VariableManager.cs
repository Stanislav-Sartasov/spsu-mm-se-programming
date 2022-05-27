using Bash.Exceptions;
using Bash.Interfaces;

namespace Bash.LocalVariables
{
    public class VariableManager : IVariableManager
    {
        private List<Variable> variablesContainer;

        public VariableManager()
        {
            variablesContainer = new List<Variable>();
        }

        public void SetVariableValue(string name, string value)
        {
            if (name.Count(c => c == '_') + name.Count(char.IsLetterOrDigit) != name.Length)
            {
                throw new BadRequestException("Invalid variable name, the variable name must consist of letters, numbers and underscores");
            }

            var variable = variablesContainer.Find(x => x.Name == name);
            if (variable != null)
            {
                variable.Value = value;
            }
            else
            {
                variablesContainer.Add(new Variable(name, value));
            }
        }

        public string? GetVariableValue(string name)
        {
            var variable = variablesContainer.Find(x => x.Name == name);
            return variable?.Value ?? Environment.GetEnvironmentVariable(name);
        }
    }
}