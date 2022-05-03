using CommandResolverLib;

namespace CommandLib
{
    public abstract class ACommand : ICommand
    {
        public string Name;

        protected string[] parametres;

        protected string stdIn;

        protected string stdOut;

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
