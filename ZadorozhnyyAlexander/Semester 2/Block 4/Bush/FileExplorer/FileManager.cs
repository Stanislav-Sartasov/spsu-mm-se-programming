namespace FileExplorer
{
    public static class FileManager
    {
        public static bool SetDirectory(string dir)
        {
            if (Directory.Exists(dir))
            {
                Directory.SetCurrentDirectory(dir);
                return true;
            }
            return false;
        }

        public static bool IsFileExist(string name)
        {
            return File.Exists(name);
        }

        public static string GetCurrentDirectory()
        {
            return Directory.GetCurrentDirectory();
        }

        public static List<String> GetDirectoryFiles(string path)
        {
            var files = Directory.GetFiles(path).ToList().Select(x => Path.GetFileName(x));
            var dirs = Directory.GetDirectories(path).ToList().Select(x => x[x.LastIndexOf('\\')..]);
            return dirs.Concat(files).ToList();
        }
    }
}