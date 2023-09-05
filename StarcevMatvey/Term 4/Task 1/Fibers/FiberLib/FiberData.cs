using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace FiberLib
{
    public class FiberData
    {
        public int Prio { get; }
        public int Id { get; }
        public Fiber Fiber { get; }

        public FiberData(int prio, int id, Fiber fiber)
        {
            this.Prio = prio;
            this.Id = id;
            this.Fiber = fiber;
        }
    }
}
