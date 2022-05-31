using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VarManager
{
    public class Var : IVar
    {
        public string Key { get; private set; }
        public string Value { get; set; }


        public Var(string key, string value)
        {
            this.Key = key;
            this.Value = value;
        }
    }
}
