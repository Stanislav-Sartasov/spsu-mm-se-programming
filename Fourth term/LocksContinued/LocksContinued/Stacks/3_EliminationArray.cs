using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocksContinued.Stacks
{
    public class EliminationArray<T>
    {
        private const int duration = ...;
        LockFreeExchanger<T>[] exchanger;
        Random random;
        public EliminationArray(int capacity)
        {
            exchanger = new LockFreeExchanger<T>[capacity];
            for (int i = 0; i < capacity; i++)
            {
                exchanger[i] = new LockFreeExchanger<T>();
            }
            random = new Random();
        }
        public T Visit(T value, int range)
        {
            int slot = random.Next(range);
            return (exchanger[slot].Exchange(value, new TimeSpan(0, 0, 0, 0, duration)));
        }
    }
}
