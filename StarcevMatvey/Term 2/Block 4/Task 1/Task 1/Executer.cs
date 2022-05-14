using Enumerations;
using Task_1.Commands;

namespace Task_1
{
    public class Executer
    {
        public Executer()
        {

        }

        public MyBash ExecuteCommand(Command commandName, MyBash bash)
        {
            var command = GetCommand(commandName);
            return command.Execute(bash);
        }

        public IExecutable GetCommand(Command commandName)
        {
            switch ((int)commandName)
            {
                case 0: return new ExitCommand();
                case 1: return new PwdCommand();
                case 2: return new EchoCommand();
                case 3: return new CatCommand();
                case 4: return new WcCommand();
                case 5: return new WhoamiCommand();
                case 6: return new CdCommand();
                case 7: return new PipelineCommand();
                case 8: return new SetLocalVarCommand();
                case 9: return new ClearCommand();
                case 10: return new SetArgumentCommand();
                case 11: return new ProcessStartCommand();
                default: return new ExitCommand();
            }
        }
    }
}
