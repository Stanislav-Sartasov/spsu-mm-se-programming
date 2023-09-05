namespace CommandLib
{
    public class PWDCommand : ACommand
    {
        public PWDCommand(string[] args)
        {
            name = CommandName.pwd;
            parametres = args;
        }

        public override void Run()
        {
            stdOut = Environ.GetCurrentDirectory();
        }

    }
}