using System.Text;

namespace Commands.BashCommands;

public class Ls : ICommand
{
    public string Name { get; }
    public string Description { get; }
    public bool Launched { get; private set; }
    public CommandOutput Output { get; }

    public Ls()
    {
        Name = "ls";
        Description = "List entities in the current or specified directory";
        Output = CommandOutput.EmptyBashCommand;
    }
    
    public CommandResult Launch(IList<string> parameters)
    {
        Launched = true;
        
        if (parameters.Count == 0)
        {
            return ListEntities(Directory.GetCurrentDirectory());
        }
        else
        {
            var finResult = CommandResult.Success;
            foreach (var param in parameters)
            {
                var result = ListEntities(param);

                if (finResult != CommandResult.Failed)
                {
                    finResult = result;
                }
                else if (param.Length > 1 && result != CommandResult.Success)
                {
                    finResult = CommandResult.Failed;
                }
                else
                {
                    finResult = result;
                }
            }

            return finResult;
        }
    }

    private CommandResult ListEntities(string pathToDir)
    {
        var stringBuilder = new StringBuilder();
        if (pathToDir != Directory.GetCurrentDirectory())
            stringBuilder.Append(pathToDir + ": ");

        if (Path.IsPathFullyQualified(pathToDir))
        {
            var result = ProcessDirectory(stringBuilder, pathToDir);
            stringBuilder.Clear();
            return result;
        }
        else
        {
            var newPath = Path.Combine(Directory.GetCurrentDirectory(), pathToDir);
            var result = ProcessDirectory(stringBuilder, newPath);
            stringBuilder.Clear();
            return result;
        }
    }

    private CommandResult ProcessDirectory(StringBuilder builder, string pathToDir)
    {
        if (Directory.Exists(pathToDir))
        {
            foreach (var entity in Directory.EnumerateFileSystemEntries(pathToDir))
            {
                builder.Append(entity.Split('/').Last());
                builder.Append('\t');
            }
            
            Output.AddRecord(new Record(builder.ToString(), false, String.Empty));
            return CommandResult.Success;
        }
        else
        {
            builder.Append("directory does not exists");
            Output.AddRecord(new Record(String.Empty, true, builder.ToString()));
            return CommandResult.Failed;
        }
    }
}