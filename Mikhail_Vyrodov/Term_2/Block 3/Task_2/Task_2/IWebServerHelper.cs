using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Task_2
{
    public interface IWebServerHelper
    {
        public bool MakeRequest();

        public Uri RequestURL { get; set; }
        public string Site { get; }
        public HttpWebResponse Response { get; }
    }
}
