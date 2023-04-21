namespace Commands.BashCommands;

public class UnknownCommand : ICommand
{
    public string Name { get; private set; }
    public string Description { get; }
    public bool Launched { get; private set; }
    public CommandOutput Output { get; }
    
    public UnknownCommand()
    {
        Name = "unknownCommand";
        Description = "Attempts to start an operating system process";
        Launched = false;
        Output = CommandOutput.EmptyNotBashCommand;
    }
    
    public CommandResult Launch(IList<string> parameters)
    {
        Launched = true;

        using (var process = new System.Diagnostics.Process())
        {
            process.StartInfo.FileName = parameters.First();
            Name = parameters.First();
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.RedirectStandardError = true;

            if (parameters.Count > 1)
            {
                foreach (var param in parameters.Skip(1))
                {
                    process.StartInfo.ArgumentList.Add(param);
                }
            }

            try
            {
                process.Start();
                Output.AddRecord(new Record(process.StandardOutput.ReadToEnd(), false, String.Empty));
                return CommandResult.Success;
            }
            catch (Exception)
            {
                Output.AddRecord(new Record(String.Empty, true, "Command was not found"));
                return CommandResult.Failed;
            }
        }
    }
}