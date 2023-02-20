using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocksContinued
{
    public abstract class BaseHashSet<T>
    {
        protected List<T>[] _table;
        protected int _setSize;

        public BaseHashSet(int capacity)
        {
            _setSize = 0;
            _table = new List<T>[capacity];
            for (int i = 0; i < capacity; i++)
            {
                _table[i] = new List<T>();
            }
        }

        protected abstract bool PolicyDemandsResize { get; }

        protected abstract void Resize();
        protected abstract void Acquire(T x);
        protected abstract void Release(T x);

        public bool Contains(T x)
        {
            Acquire(x);
            try
            {
                int myBucket = Math.Abs(x.GetHashCode() % _table.Length);
                return _table[myBucket].Contains(x);
            }
            finally
            {
                Release(x);
            }
        }
        public bool Add(T x)
        {
            bool result = false;
            Acquire(x);
            try
            {
                int myBucket = Math.Abs(x.GetHashCode() % _table.Length);
                if (!_table[myBucket].Contains(x))
                {
                    _table[myBucket].Add(x);
                    result = true;
                    _setSize++;
                }
            }
            finally
            {
                Release(x);
            }
            if (PolicyDemandsResize)
                Resize();
            return result;
        }
    }
}
