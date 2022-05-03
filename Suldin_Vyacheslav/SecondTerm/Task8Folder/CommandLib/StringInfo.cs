using System;
using System.Text;
using System.Text.RegularExpressions;

namespace CommandLib
{
    public class StringInfo
    {
        public int Words;
        public int Strings;
        public int Bytes;
        public StringInfo(string str)
        {
            if (str == null) return;
            Bytes = new ASCIIEncoding().GetByteCount(str);
            Strings = str.Split("\r").Length;
            Words = str.Split(new char[] { ' ', '\n', '\r'}, StringSplitOptions.RemoveEmptyEntries).Length;

        }

        public string GetAllInfo()
        {
            return $"{Strings} {Words} {Bytes}";
        }
    }
}
