using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocksContinued.Hashing
{
    class SequentialCuckooHashTable<T>
    {
        const int LIMIT = 5;

        public bool Add(T x)
        {
            if (Contains(x))
            {
                return false;
            }
            for (int i = 0; i < LIMIT; i++)
            {
                if ((x = Swap(0, Hash0(x), x)) == null)
                {
                    return true;
                }
                else if ((x = Swap(1, Hash1(x), x)) == null)
                {
                    return true;
                }
            }
            Resize();
            return Add(x);
        }

        private int Hash0(T x)
        {
            throw new NotImplementedException();
        }

        private int Hash1(T x)
        {
            throw new NotImplementedException();
        }

        private T Swap(int v, object p, T x)
        {
            throw new NotImplementedException();
        }

        private bool Contains(T x)
        {
            throw new NotImplementedException();
        }

        private void Resize()
        {
            throw new NotImplementedException();
        }
    }
}
