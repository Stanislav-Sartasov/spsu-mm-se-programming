namespace FileManager
{
    public static class FileManager
    {
        public static bool IsReaded { get; private set; }
        public static bool IsWrited { get; private set; }

        static FileManager()
        {
            IsReaded = false;
            IsWrited = false;
        }

        public static string FileToString(string path)
        {
            IsReaded = false;
            var data = "";
            try
            {
                using (var sr = new StreamReader(path))
                {
                    data = sr.ReadToEnd();
                }

                IsReaded = true;
            }
            catch
            {
                IsReaded = false;
            }

            return data;
        }

        public static List<string> FileToList(string path, string sep)
        {
            var data = FileToString(path);

            if (!IsReaded) return new List<string>();

            return data.Split(sep).ToList();
        }

        public static List<int> FileToIntList(string path, string sep)
        {
            var data = FileToList(path, sep);

            if (!IsReaded) return new List<int>();

            var rez = new List<int>();
            foreach (var s in data)
            {
                int n;
                if (int.TryParse(s, out n)) rez.Add(n);
                else
                {
                    IsReaded = false;
                    break;
                }
            }

            return rez;
        }

        public static void StringToFile(string data, string path)
        {
            IsWrited = false;
            try
            {
                using (var sw = new StreamWriter(path))
                {
                    sw.WriteLine(data);
                    IsWrited = true;
                }
            }
            catch
            {
                IsWrited = false;
            }
        }
    }
}

