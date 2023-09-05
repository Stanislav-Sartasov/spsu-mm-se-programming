using System.Runtime.CompilerServices;

namespace BashComponents;

public class VariableManager
{
    private VariableContainer container;

    public VariableManager()
    {
        container = new VariableContainer();
    }

    public List<String> UnpackVariables(List<String> lexemes)
    {
        var unpackedList = new List<String>();
        
        foreach (var lexeme in lexemes)
        {
            var newString = lexeme;
            
            foreach (var word in lexeme.Split(' '))
            {
                if (word != String.Empty && word[0] == '$' && !word.Contains('='))
                {
                    newString = newString.Replace(word, container.Get(word));
                }
            }
            
            unpackedList.Add(newString);
        }

        return unpackedList;
    }

    public void AddVariable(List<String> command)
    {
        container.Add(command.First(), command.Last());
    }
}