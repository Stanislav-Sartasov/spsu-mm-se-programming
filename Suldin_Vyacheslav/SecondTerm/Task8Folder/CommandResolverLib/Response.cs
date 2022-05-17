using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;

namespace CommandResolverLib
{
    public class Response : IResponse
    {
        public bool IsInterrupting { get; set; }
        public string Message { get; set; }
    }
}
