using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocksContinued
{
    public class StripedHashSet<T> : BaseHashSet<T>
    {
        Mutex[] _locks;
        public StripedHashSet(int capacity) : base(capacity)
        {
            _locks = new Mutex[capacity];
            for (int i = 0; i < _locks.Length; i++)
            {
                _locks[i] = new Mutex();
            }
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
            _locks[Math.Abs(x.GetHashCode() % _locks.Length)].WaitOne();
        }

        protected override void Release(T x)
        {
            _locks[Math.Abs(x.GetHashCode() % _locks.Length)].ReleaseMutex();
        }

        protected override void Resize()
        {
            int oldCapacity = _table.Length;

            foreach (Mutex m in _locks)
            {
                m.WaitOne();
            }

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
                foreach (Mutex m in _locks)
                {
                    m.ReleaseMutex();
                }
            }
        }
    }
}
