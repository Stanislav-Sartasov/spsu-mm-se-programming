using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BABASH
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
            Words = Regex.Matches(str, @"\w+").Count;

        }

        public string GetAllInfo()
        {
            return $"{Strings} {Words} {Bytes}";
        }
    }
}
