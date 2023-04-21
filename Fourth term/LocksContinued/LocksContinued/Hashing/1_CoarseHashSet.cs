using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocksContinued
{
    public class CoarseHashSet<T> : BaseHashSet<T>
    {
        public Mutex _lock = new Mutex();
        public CoarseHashSet(int capacity) : base(capacity)
        {
        }

        protected override bool PolicyDemandsResize
        {
            get
            {
                return _setSize / _table.Length > 4;
            }
        }
        protected override void Acquire(T x)
        {
            _lock.WaitOne();
        }

        protected override void Release(T x)
        {
            _lock.ReleaseMutex();
        }

        protected override void Resize()
        {
            int oldCapacity = _table.Length;
            _lock.WaitOne();
            try
            {
                if (oldCapacity != _table.Length)
                {
                    return; // someone beat us to it
                }
                int newCapacity = 2 * oldCapacity;
                List<T>[] oldTable = _table;
                _table = new List<T>[newCapacity];
                for (int i = 0; i < newCapacity; i++)
                    _table[i] = new List<T>();
                foreach (List<T> bucket in oldTable)
                {
                    foreach (T x in bucket)
                    {
                        _table[x.GetHashCode() % _table.Length].Add(x);
                    }
                }
            }
            finally
            {
                _lock.ReleaseMutex();
            }
        }
    }
}
