using LocksContinued.WorkStealing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocksContinued.Stacks
{

    public class LockFreeExchanger<T>
    {
        const int Empty = 0;
        const int Waiting = 1;
        const int Busy = 2;
        AtomicStampedReference<T> slot = new AtomicStampedReference<T>(default(T), 0);
        public T Exchange(T myItem, TimeSpan timeout)
        {
            DateTime timeBound = DateTime.Now.Add(timeout);
            while (true)
            {
                if (DateTime.Now > timeBound)
                    throw new TimeoutException();
                int stamp;
                T yrItem = slot.Get(out stamp);

                switch (stamp)
                {
                    case Empty:
                        if (slot.CompareAndSet(yrItem, myItem, Empty, Waiting))
                        {
                            while (DateTime.Now < timeBound)
                            {
                                yrItem = slot.Get(out stamp);
                                if (stamp == Busy)
                                {
                                    slot.Set(default(T), Empty);
                                    return yrItem;
                                }
                            }
                            if (slot.CompareAndSet(myItem, default(T), Waiting, Empty))
                            {
                                throw new TimeoutException();
                            }
                            else
                            {
                                yrItem = slot.Get(out stamp);
                                slot.Set(default(T), Empty);
                                return yrItem;
                            }
                        }
                        break;
                    case Waiting:
                        if (slot.CompareAndSet(yrItem, myItem, Waiting, Busy))
                            return yrItem;
                        break;
                    case Busy:
                        break;
                    default: // impossible
                        throw new InvalidOperationException();
                }
            }
        }
    }
}
