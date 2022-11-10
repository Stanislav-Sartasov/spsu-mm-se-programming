using Commands;
using Commands.BashCommands;

namespace BashComponents;

public class CommandManager
{
    private VariableManager container;
    private LexemeConverter lexemeConverter;
    private OutputMessageManager messageManager;

    public CommandManager()
    {
        container = new VariableManager();
        lexemeConverter = new LexemeConverter();
        messageManager = new OutputMessageManager();
    }

    public String Run(List<String> lexemes)
    {
        lexemes = container.UnpackVariables(lexemes);
        List<CommandInfo> commands;
        
        try
        {
            commands = lexemeConverter.Run(lexemes);
        }
        catch (Exception e)
        {
            return e.Message;
        }

        if (commands.Count == 0)
            return String.Empty;
        else
        {
            var command = ExecuteCommands(commands);

            foreach (var record in command.Output.Message)
            {
                if (record.ErrorOccured)
                {
                    messageManager.AddNewMessage(command.Name + ": " + record.ErrorMessage);
                }
                else
                {
                    messageManager.AddNewMessage(record.Message);
                }
            }

            return messageManager.GetMessage();
        }
        
    }

    private ICommand? ExecuteCommands(List<CommandInfo> commands)
    {
        var additionalArguments = new List<String>();
        ICommand? executeCommand = null;
        
        foreach (var command in commands)
        {
            switch (command.Name)
            {
                case "cat":
                {
                    executeCommand = new Cat();
                    break;
                }
                case "cd":
                {
                    executeCommand = new Cd();
                    break;
                }
                case "echo":
                {
                    executeCommand = new Echo();
                    break;
                }
                case "exit":
                {
                    executeCommand = new Exit();
                    break;
                }
                case "ls":
                {
                    executeCommand = new Ls();
                    break;
                }
                case "pwd":
                {
                    executeCommand = new Pwd();
                    break;
                }
                case "wc":
                {
                    executeCommand = new Wc();
                    break;
                }
                case "addVariable":
                {
                    executeCommand = new EmptyCommand();
                    container.AddVariable(command.Arguments);
                    break;
                }
                default:
                {
                    executeCommand = new UnknownCommand();
                    command.Prepend(command.Name);
                    break;
                }
            }

            if (additionalArguments.Count != 0)
            {
                command.Arguments.AddRange(additionalArguments);
                additionalArguments.Clear();
            }
            
            var commandResult = executeCommand.Launch(command.Arguments);
            
            if (commandResult == CommandResult.Success)
            {
                foreach (var output in executeCommand.Output.Message)
                {
                    additionalArguments.Add(output.Message);
                }
            }
            else
            {
                return executeCommand;
            }
        }

        return executeCommand;
    }
}