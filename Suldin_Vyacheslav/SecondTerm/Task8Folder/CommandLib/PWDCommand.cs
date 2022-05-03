namespace CommandLib
{
    public class PWDCommand : ACommand
    {
        public PWDCommand(string[] args)
        {
            Name = "pwd";
            parametres = args;
        }

        public override void Run()
        {
            stdOut = Environ.GetCurrentDirectory();
        }

    }
}