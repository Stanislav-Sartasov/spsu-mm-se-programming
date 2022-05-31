using System.IO;

namespace DirectoryManager
{
    public class DirectoryInfo
    {
        public string CurrentDirectory { get; private set; }
        public string[] FullFileNamesInDirectory { get; private set; }
        public string[] FileNamesInDirectory { get; private set; }


        public DirectoryInfo()
        {
            this.CurrentDirectory = Directory.GetCurrentDirectory();
            this.FullFileNamesInDirectory = Directory.GetFiles(this.CurrentDirectory);
            this.FileNamesInDirectory = new string[this.FullFileNamesInDirectory.Length];

            for (int i = 0; i < this.FullFileNamesInDirectory.Length; i++)
            {
                this.FileNamesInDirectory[i] = this.FullFileNamesInDirectory[i].Substring(this.CurrentDirectory.Length + 1);
            }
        }
    }
}