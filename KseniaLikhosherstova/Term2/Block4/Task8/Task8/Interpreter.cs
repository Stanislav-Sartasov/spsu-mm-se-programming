using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;
using Task8.Commands;


namespace Task8
{
    public class Interpreter
    {

        private const string ECHO_COMMAND = "echo";

        private const string EXIT_COMMAND = "exit";

        private const string PWD_COMMAND = "pwd";

        private const string CAT_COMMAND = "cat";

        private const string WC_COMMAND = "wc";

        private const string CD_COMMAND = "cd";

        private string filePath;
        private ScriptParser parser = new ScriptParser();
        

        public Interpreter(string argument)
        {
            if (string.IsNullOrEmpty(argument))
            {
                throw new ArgumentNullException("argument");
            }


            this.filePath = Path.GetFullPath(argument);

            if (!File.Exists(this.filePath))
            {
                parser.Parse(argument);
            }
            else 
                parser.ParseFromFile(this.filePath);
        }
       

        public bool ExecuteScript(out string result, out string error)
        {
            StringBuilder resultBuilder = new StringBuilder();

            result = string.Empty;
            error = string.Empty;

            try
            {
                foreach (var command in parser.Commands)
                {
                    if (command.Name == EXIT_COMMAND)
                        break;

                    var commandRes = ExecuteCommand(command, out string commandResult, out string commandError);

                    if (!string.IsNullOrEmpty(commandResult))
                    {
                        resultBuilder.AppendLine(commandResult);
                    }

                    error = commandError;

                    if (!commandRes) break;
                }
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            finally
            {
                result = resultBuilder.ToString()?.TrimEnd('\r', '\n');
            }

            return string.IsNullOrEmpty(error);
        }


        private bool ExecuteCommand(Command command, out string result, out string error)
        {
          
            StringBuilder resultBuilder = new StringBuilder();

            error = string.Empty;
          

            try
            {
                switch (command.Name)
                {
                    case ECHO_COMMAND:

                        resultBuilder.Append(IoCContainer.Container().First(x => x.Name == "echo").Motion(command.Arguments)); 
                        break;

                    case CAT_COMMAND:

                        resultBuilder.Append(IoCContainer.Container().First(x => x.Name == "cat").Motion(command.Arguments));
                        break;

                    case PWD_COMMAND:
                        resultBuilder.AppendLine(Environment.CurrentDirectory);

                        var fileNames = Directory.EnumerateFiles(Environment.CurrentDirectory, "*.*", SearchOption.TopDirectoryOnly)
                                                 .Select(path => Path.GetFileName(path)).ToList();

                        fileNames.ForEach(file => resultBuilder.AppendLine(file));
                        break;

                    case WC_COMMAND:

                        resultBuilder.Append(IoCContainer.Container().First(x => x.Name == "wc").Motion(command.Arguments));
                        break;

                    case CD_COMMAND:

                        resultBuilder.Append(IoCContainer.Container().First(x => x.Name == "cd").Motion(command.Arguments));
                        break;

                    default:
                        Process.Start(command.Name);
                        break;

                    case EXIT_COMMAND:
                        return false;
                }

                if (command.PipeCommand != null)
                {
                    command.PipeCommand.Arguments = resultBuilder.ToString()?.TrimEnd('\r', '\n');
                    ExecuteCommand(command.PipeCommand, out result, out error);

                    resultBuilder.Clear();
                    resultBuilder.AppendLine(result);
                }
            }
            catch (Exception e)
            {
                error = e.Message;
            }
            finally
            {
                result = resultBuilder.ToString();
            }
            return string.IsNullOrEmpty(error);
        }
    }
}
