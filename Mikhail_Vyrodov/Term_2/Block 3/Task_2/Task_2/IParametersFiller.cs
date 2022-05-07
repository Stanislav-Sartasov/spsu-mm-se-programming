using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2
{
    public interface IParametersFiller
    {
        public Dictionary<string, string> Parameters { get; }

        public bool FillParameters(IWebServerHelper webHelper, IResponseReader respReader);
    }
}
