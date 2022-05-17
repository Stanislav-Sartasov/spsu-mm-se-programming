using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public interface IResponse
    {
        bool IsInterrupting { get; set; }

        string Message { get; set; }
    }
}
