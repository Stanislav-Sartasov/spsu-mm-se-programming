using CommandResolverLib;

namespace CommandLib
{
    public abstract class ACommand : ICommand
    {
        public string Name { get; set; }
        protected string[] parametres { get; set; }
        protected string stdIn { get; set; }
        protected string stdOut { get; set; }

        protected Error error = new Error();
        public abstract void Run();
        public void SetStdIn(string stdIn)
        {
            this.stdIn = stdIn;
        }
        public string GetStdOut()
        {
            return stdOut;
        }
        public string GetErrorMessage()
        {
            return error.Message;
        }
        public int GetErrorCode()
        {
            return error.StdErr;
        }

    }
}
