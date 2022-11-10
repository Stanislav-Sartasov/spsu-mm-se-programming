namespace BashComponents;

public class LexemeConverter
{
    private bool commandFoundFlag;
    private bool variableAssignmentFlag;

    public LexemeConverter()
    {
        commandFoundFlag = false;
        variableAssignmentFlag = false;
    }
    public List<CommandInfo> Run(List<String> lexemes)
    {
        commandFoundFlag = true;
        variableAssignmentFlag = false;
        var commands = new List<CommandInfo>();
        var commandName = String.Empty;
        var commandArguments = new List<String>();
        var counter = 0;
        
        foreach (var lexeme in lexemes)
        {
            counter++;
            
            if (lexeme == "|")
            {
                commands.Add(new CommandInfo(commandName, commandArguments));
                commandArguments = new List<string>();
                commandFoundFlag = true;
                variableAssignmentFlag = false;
            } 
            else
            {
                if (variableAssignmentFlag)
                    throw new WrongSyntaxException("Invalid command syntax");
                
                if (commandFoundFlag)
                {
                    if (lexeme[0] == '$' && lexeme.Contains('=') && lexeme.Split('=').Length == 2)
                    {
                        var commandInfo = lexeme.Split('=');
                        commandName = "addVariable";
                        commandArguments.AddRange(commandInfo.ToList());
                        variableAssignmentFlag = true;
                    }
                    else
                    {
                        commandName = lexeme;
                        commandFoundFlag = false;
                    }
                }
                else
                {
                    commandArguments.Add(lexeme);
                }
            }
        }
        
        commands.Add(new CommandInfo(commandName, commandArguments));
        return commands;
    }
}