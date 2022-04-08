using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Parsers
{
    public abstract class JSONParser
    {
        public IReadOnlyList<string> ParsedInfo { get; protected set; }
        protected string[] parsingParams;


        public virtual IReadOnlyList<string> Parse(JObject json)
        {
            return null;
        }
    }
}
