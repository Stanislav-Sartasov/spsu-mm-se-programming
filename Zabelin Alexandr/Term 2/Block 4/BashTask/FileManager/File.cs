using System.IO;
using System.Text;

namespace FileManager
{
    public class File
    {
        public string FileText { get; private set; }
        public string FilePath { get; private set; }
        public long WordsCount { get; private set; }
        public long LinesCount { get; private set; }
        public long Weight { get; private set; }
 

        public File(string path)
        {
            this.FilePath = path;

            this.ReadFile();
            this.CalculateFileStat();
        }

        private void ReadFile()
        {
             string? fileText = System.IO.File.ReadAllText(this.FilePath, Encoding.UTF8);

            if (fileText == null)
            {
                fileText = "";
            }

            this.FileText = fileText;
        }

        private void CalculateFileStat()
        {
            FileInfo fileInfo = new FileInfo(this.FilePath);
            
            this.Weight = fileInfo.Length;

            this.CountWords();
            this.CountLines();
        }

        private void CountWords()
        {
            long wordsCounter = 0;
            bool didWordStart = false; 

            foreach (char c in this.FileText)
            {
                if (char.IsLetter(c))
                {
                    didWordStart = true;
                } 
                else if (didWordStart)
                {
                    wordsCounter++;
                    didWordStart = false;
                }
                else
                {
                    didWordStart = false;
                }
            }

            if (didWordStart)
            {
                wordsCounter++;
            }

            this.WordsCount = wordsCounter;
        }

        private void CountLines()
        {
            this.LinesCount = this.FileText.Count(symbol => symbol == '\n') + 1;
        }
    }
}
