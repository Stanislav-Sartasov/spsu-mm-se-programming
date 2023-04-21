using System.Text;

namespace BashComponents;

public class InputParser
{
    public List<String> Parse(String userInput)
    {
        var quotationMark = false;
        var addLastArgument = true;
        var builder = new StringBuilder();
        var lexemes = new List<String>();

        foreach (var ch in userInput)
        {
            if (quotationMark && ch != '"')
            {
                builder.Append(ch);
            }
            else if (ch == ' ')
            {
                if (builder.Length != 0)
                {
                    lexemes.Add(builder.ToString());
                    builder.Clear();
                }
            }
            else if (ch == '"')
            {
                if (quotationMark)
                {
                    if (builder.Length != 0)
                    {
                        lexemes.Add(builder.ToString());
                        builder.Clear();
                    }
                    addLastArgument = false;
                }
                else addLastArgument = true;
                
                quotationMark = !quotationMark;
            }
            else 
            {
                builder.Append(ch);
            }
        }
        
        if (addLastArgument) 
            lexemes.Add(builder.ToString());
        
        return lexemes;
    }
}