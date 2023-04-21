using System.Reflection;

namespace Bash.Commands
{
    public class ChangeDirectory : ICommand
    {
        public string Name { get; private set; }
        public string HomePath { get; private set; }

        public ChangeDirectory()
        {
            Name = "cd";
            HomePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ?? Environment.CurrentDirectory;
        }

        public string[]? Execute(string[] args)
        {

            try
            {
                if (args.Length == 0 || args.Length == 1 && args[0].Length == 1 && args[0][0] == '~')
                {
                    Directory.SetCurrentDirectory(HomePath);
                }
                else if (args.Length == 1)
                {
                    if (Directory.Exists(args[0]))
                    {
                        Directory.SetCurrentDirectory(args[0]);
                    }
                    else if (args[0][0] == '~')
                    {
                        string? newUser;
                        int indexOfNewFolder = args[0].IndexOf('\\');
                        int endUser = indexOfNewFolder != -1 ? indexOfNewFolder : args[0].Length;
                        int startDirectory = endUser + 1;

                        newUser = GetFullPathOfFolder(Directory.GetDirectories(Path.GetDirectoryName(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile)) ?? ""), args[0][1..endUser]);

                        if (newUser is null)
                        {
                            throw new Exception();
                        }

                        Directory.SetCurrentDirectory(newUser);

                        if (startDirectory < args[0].Length)
                        {
                            ProcessDirectory(args[0][startDirectory..]);
                        }
                    }
                    else
                    {
                        ProcessDirectory(args[0]);
                    }

                }
                else
                {
                    throw new ArgumentException();
                }
            }
            catch (Exception)
            {
                return new string[1] { "Failed to put the directory, the reasons may be: too many arguments or the wrong path" };
            }

            return null;
        }


        private void ProcessDirectory(string directory)
        {
            string resultPath = Environment.CurrentDirectory;

            foreach (var folder in directory.Split("\\").Where(x => x != "" && x != " ").ToArray())
            {
                if (folder == "..")
                {
                    resultPath = Path.GetDirectoryName(resultPath) ?? resultPath;
                }
                else
                {
                    var result = GetFullPathOfFolder(Directory.GetDirectories(resultPath), folder);
                    if (result is not null)
                    {
                        resultPath = result;
                    }
                    else
                    {
                        throw new Exception();
                    }
                }
            }

            Directory.SetCurrentDirectory(resultPath);
        }

        private string? GetFullPathOfFolder(string[] directories, string nameFolder)
        {
            foreach (var newFolder in directories)
            {
                if (newFolder.Substring(newFolder.LastIndexOf('\\') + 1) == nameFolder)
                {
                    return newFolder;
                }
            }

            return null;
        }
    }
}