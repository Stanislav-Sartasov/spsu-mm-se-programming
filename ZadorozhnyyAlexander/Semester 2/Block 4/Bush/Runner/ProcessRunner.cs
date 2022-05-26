using AbstractOperators;
using System.Diagnostics;


namespace Runner
{
    public class ProcessRunner : ARunner
    {
        public ProcessRunner(ALogger logger) : base(logger) {}

        public override List<String> Start(string name, string args)
        {
            Process mainProcess = new Process();
            mainProcess.StartInfo.FileName = name;
            mainProcess.StartInfo.Arguments = args;
            mainProcess.StartInfo.UseShellExecute = false;
            mainProcess.StartInfo.RedirectStandardOutput = true;

            try
            {
                mainProcess.Start();
                mainProcess.WaitForExit();
                return new List<String> () { mainProcess.StandardOutput.ReadToEnd() };
            }
            catch
            {
                logger.Log("Error with starting process. Something went worg.");
                return new List<string> ();
            }
        }
    }
}
