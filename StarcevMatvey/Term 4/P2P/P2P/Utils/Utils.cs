using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace P2P.Utils
{
    public static class Utils
    {
        public static int GetPositiveInt(string s)
        {
            int rez;

            if (Int32.TryParse(s, out rez)) return rez > 0 ? rez : 0;

            return 0;
        }

        public static IPEndPoint TryPort(string s)
        {
            IPEndPoint rez;

            return IPEndPoint.TryParse(s, out rez) ? rez : null;
        }
    }
}
