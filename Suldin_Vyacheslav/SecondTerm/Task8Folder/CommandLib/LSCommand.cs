using System.IO;

namespace CommandLib
{
    public class LSCommand : ACommand
    {
        public LSCommand(string[] args)
        {
            name = CommandName.ls;
            parametres = args;
        }

        public override void Run()
        {
            if (parametres.Length == 0) parametres = new string[] {string.Empty};
            foreach (string arg in parametres)
            {
                string abcolutePath = Path.GetFullPath(arg, Environ.GetCurrentDirectory());
                if (File.Exists(abcolutePath))
                {
                    CopyAdd(arg + " ");
                    continue;
                }
                else if (Directory.Exists(abcolutePath))
                {
                    if (parametres.Length != 1) CopyAdd(arg + ":\n");

                    foreach (string directory in Directory.GetDirectories(abcolutePath))
                    {
                        string directoryName = directory.Split("\\")[^1];
                        CopyAdd(directoryName + " ");
                    }
                    foreach (string file in Directory.GetFiles(abcolutePath))
                    {
                        string fileName = file.Split("\\")[^1];
                        CopyAdd(fileName + " ");
                    }
                    CopyAdd("\n");
                }
                else
                {
                    error.StdErr = 2;
                    error.Message += $"{name}: cannot access \'{arg}\': No such directory\n";
                }
            }
            stdOut = stdOut == null? null : stdOut[..^1];
            error.Message = error.Message[..^1];
        }

        public void CopyAdd(string str)
        {
            stdOut += str;
            error.Message += str;
        }
    }
   
}