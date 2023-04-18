using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThreadPool
{
    public static class Utils
    {
        public static int GetPositiveInt(string s)
        {
            int rez;

            if (Int32.TryParse(s, out rez)) return rez > 0 ? rez : 0;

            return 0;
        }
    }
}
