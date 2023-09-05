using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BashCommands
{
    public interface ICommand
    {
        public string FullName { get;  }
        public string ShortName { get; }
        public IEnumerable<string>? Execute(IEnumerable<string>? arguments);
    }
}
