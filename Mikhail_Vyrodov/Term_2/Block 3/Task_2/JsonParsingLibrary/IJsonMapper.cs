using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JsonParsingLibrary
{
    interface IJsonMapper<T>
    {
        public Dictionary<string, string> GetParameters(T dataHolder);
    }
}
