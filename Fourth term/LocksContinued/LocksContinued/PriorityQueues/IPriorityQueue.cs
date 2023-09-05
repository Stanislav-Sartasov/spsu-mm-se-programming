using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocksContinued
{
    public interface IPriorityQueue<T>
    {
        void Add(T item, int priority);
        T RemoveMin();
    }
}
