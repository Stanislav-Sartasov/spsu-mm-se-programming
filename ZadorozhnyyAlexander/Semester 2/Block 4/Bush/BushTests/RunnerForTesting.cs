using AbstractOperators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BushTests
{
    public class RunnerForTesting : ARunner
    {
        public RunnerForTesting(ALogger logger) : base(logger) {}

        public override List<string> Start(string name, string args)
        {
            return new List<string>() { "Process was complited!" };
        }
    }
}
