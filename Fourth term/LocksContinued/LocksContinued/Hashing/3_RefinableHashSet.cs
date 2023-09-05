using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocksContinued
{
    public class RefinableHashSet<T>:BaseHashSet<T>
    {
        AtomicMarkableReference<Thread> _owner;
        volatile Mutex[] _locks;
        public RefinableHashSet(int capacity) : base(capacity)
        {

            _locks = new Mutex[capacity];
            for (int i = 0; i < capacity; i++)
            {
                _locks[i] = new Mutex();
            }
            _owner = new AtomicMarkableReference<Thread>(null, false);
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
            while (true)
            {
                bool mark;
                do
                {
                    _owner.Get(out mark);
                } while (mark);
                Mutex[] oldLocks = _locks;
                Mutex oldLock = oldLocks[Math.Abs(x.GetHashCode() % oldLocks.Length)];
                oldLock.WaitOne();
                _owner.Get(out mark);
                if (!mark && _locks == oldLocks)
                {
                    return;
                }
                else
                {
                    oldLock.ReleaseMutex();
                }
            }
        }
        protected override void Release(T x)
        {
            _locks[Math.Abs(x.GetHashCode() % _locks.Length)].ReleaseMutex();
        }

        protected override void Resize()
        {
            int oldCapacity = _table.Length;
            int newCapacity = 2 * oldCapacity;

            Thread me = Thread.CurrentThread;
            if (_owner.CompareAndSet(null, me, false, true))
            {
                try
                {
                    if (_table.Length != oldCapacity)
                    { // someone else resized first
                        return;
                    }

                    foreach (Mutex m in _locks)
                    {
                        m.WaitOne();
                        m.ReleaseMutex();
                    }

                    _locks = new Mutex[newCapacity];
                    for (int i = 0; i < newCapacity; i++)
                    {
                        _locks[i] = new Mutex();
                    }

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
                    _owner.Set(null, false);
                }
            }
        }
    }
}
