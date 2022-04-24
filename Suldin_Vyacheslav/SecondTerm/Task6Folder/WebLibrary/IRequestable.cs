using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebLibrary
{
    public interface IRequestable
    {
        string GetAddres();

        IReadOnlyList<string> GetHeaders();
    }
}
