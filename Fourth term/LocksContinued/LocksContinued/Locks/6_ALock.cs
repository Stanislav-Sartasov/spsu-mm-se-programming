using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocksContinued.Locks
{
    public class ALock : ILock
    {
        ThreadLocal<int> mySlotIndex = new ThreadLocal<int>(() => 0);

        volatile int tail;
        bool[] flag;
        int size;

        public ALock(int capacity)
        {
            size = capacity;
            tail = 0;
            flag = new bool[capacity];
            flag[0] = true;
        }

        public void Lock()
        {
            int slot = (Interlocked.Increment(ref tail) - 1) % size;
            mySlotIndex.Value = slot;
            while (!flag[slot]) { };
        }

        public void Unlock()
        {
            int slot = mySlotIndex.Value;
            flag[slot] = false;
            flag[(slot + 1) % size] = true;
        }
    }
}
