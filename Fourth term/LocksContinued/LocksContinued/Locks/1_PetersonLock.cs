using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocksContinued.Locks
{
    public class PetersonLock : ILock
    {
        private volatile bool[] flag = new bool[2];
        private volatile int victim;

        public void Lock()
        {
            int i = ThreadID.Get();
            int j = 1 - i;
            flag[i] = true; // I’m interested
            victim = i; // you go first
            //Interlocked.MemoryBarrier();
            while (
                flag[j]  
                && 
                victim == i
                ) { }; // wait
        }
        public void Unlock()
        {
            int i = ThreadID.Get();
            flag[i] = false; // I’m not interested
        }
    }
}

