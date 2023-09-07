using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1.Collections
{
    using InputType = Tuple<long, long>;

    public class StripedCuckooHashSet : IExamSystem
    {
        // list is semi-full
        protected const int THRESHOLD = 15;
        // list is full
        protected const int LIST_SIZE = 30;
        // steps to relocate
        protected const int LIMIT = 40;

        protected volatile int capacity;
        protected volatile List<InputType>[,] table;
        Mutex[,] locks;
        private volatile int count;

        public StripedCuckooHashSet(int size)
        {
            count = 0;
            capacity = size;
            locks = new Mutex[2, size];
            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    locks[i, j] = new Mutex();
                }
            }

            table = new List<InputType>[2, capacity];

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < capacity; j++)
                {
                    table[i, j] = new List<InputType>(LIST_SIZE);
                }
            }
        }

        protected int Hash0(InputType i)
        {
            (long a, long b) = i;

            a ^= a >> 17;
            a *= 830770091;   // 0xed5ad4bb
            a ^= a >> 11;
            a *= -1404298415; // 0xac4c1b51
            a ^= a >> 15;
            a *= 830770091;   // 0x31848bab
            a ^= a >> 14;

            b ^= b >> 17;
            b *= 830770091;   // 0xed5ad4bb
            b ^= b >> 11;
            b *= -1404298415; // 0xac4c1b51
            b ^= b >> 15;
            b *= 830770091;   // 0x31848bab
            b ^= b >> 14;
                
            int hash = (int)(a + b);
            return Math.Abs(hash);
        }

        protected int Hash1(InputType i)
        {
            return i.GetHashCode();
        }

        public int Count
        {
            get
            {
                lock (this)
                {
                    return count;
                }
            }
        }

        public bool Contains(long studentId, long courseId)
        {
            InputType x = new InputType(studentId, courseId);

            Acquire(x);
            try
            {
                List<InputType> set0 = table[0, Hash0(x) % capacity];
                if (set0.Contains(x))
                {
                    return true;
                }
                else
                {
                    List<InputType> set1 = table[1, Hash1(x) % capacity];
                    if (set1.Contains(x))
                    {
                        return true;
                    }
                }
                return false;
            }
            finally
            {
                Release(x);
            }
        }

        protected void Acquire(InputType x)
        {
            locks[0, Hash0(x) % locks.GetLength(0)].WaitOne();
            locks[1, Hash1(x) % locks.GetLength(1)].WaitOne();
        }

        protected void Release(InputType x)
        {
            locks[0, Hash0(x) % locks.GetLength(0)].ReleaseMutex();
            locks[1, Hash1(x) % locks.GetLength(1)].ReleaseMutex();
        }

        protected void Resize()
        {
            int oldCapacity = capacity;
            for (int i = 0; i < oldCapacity; i++)
            {
                locks[0, i].WaitOne();
            }

            try
            {
                if (capacity != oldCapacity)
                {
                    return;
                }
                List<InputType>[,] oldTable = table;
                capacity = 2 * capacity;
                table = new List<InputType>[2, capacity];

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < capacity; j++)
                    {
                        table[i, j] = new List<InputType>(LIST_SIZE);
                    }
                }

                for (int i = 0; i < 2; i++)
                {
                    for (int j = 0; j < capacity; j++)
                    {
                        foreach (InputType z in oldTable[i, j])
                        {
                            (long student, long exam) = z;
                            Add(student, exam);
                        }
                    }
                }
            }
            finally
            {
                for (int i = 0; i < oldCapacity; i++)
                {
                    locks[0, i].ReleaseMutex();
                }
            }
        }

        public void Remove(long studentId, long courseId)
        {
            RemoveInternal(studentId, courseId);
        }

        private bool RemoveInternal(long studentId, long courseId)
        {
            InputType x = new InputType(studentId, courseId);

            Acquire(x);
            try
            {
                List<InputType> set0 = table[0, Hash0(x) % capacity];
                if (set0.Contains(x))
                {
                    set0.Remove(x);
                    count--;
                    return true;
                }
                else
                {
                    List<InputType> set1 = table[1, Hash1(x) % capacity];
                    if (set1.Contains(x))
                    {
                        set1.Remove(x);
                        count--;
                        return true;
                    }
                }
                return false;
            }
            finally
            {
                Release(x);
            }
        }

        public void Add(long studentId, long courseId)
        {
            AddInternal(studentId, courseId);
        }

        private bool AddInternal(long studentId, long courseId)
        {
            InputType x = new InputType(studentId, courseId);

            Acquire(x);
            int h0 = Hash0(x) % capacity, h1 = Hash1(x) % capacity;
            int i = -1, h = -1;
            bool mustResize = false;
            try
            {
                if (Contains(studentId, courseId)) return false;
                List<InputType> set0 = table[0, h0];
                List<InputType> set1 = table[1, h1];
                if (set0.Count < THRESHOLD)
                {
                    Interlocked.Add(ref count, 1);
                    set0.Add(x); return true;
                }
                else if (set1.Count < THRESHOLD)
                {
                    Interlocked.Add(ref count, 1);
                    set1.Add(x); return true;
                }
                else if (set0.Count < LIST_SIZE)
                {
                    Interlocked.Add(ref count, 1);
                    set0.Add(x); i = 0; h = h0;
                }
                else if (set1.Count < LIST_SIZE)
                {
                    Interlocked.Add(ref count, 1);
                    set1.Add(x); i = 1; h = h1;
                }
                else
                {
                    mustResize = true;
                }
            }
            finally
            {
                Release(x);
            }
            if (mustResize)
            {
                Resize(); Add(studentId, courseId);
            }
            else if (!Relocate(i, h))
            {
                Resize();
            }
            return true; // x must have been present
        }

        protected bool Relocate(int i, int hi)
        {
            int hj = 0;
            int j = 1 - i;
            for (int round = 0; round < LIMIT; round++)
            {
                List<InputType> iSet = table[i, hi];
                InputType y = iSet[0];
                switch (i)
                {
                    case 0: hj = Hash1(y) % capacity; break;
                    case 1: hj = Hash0(y) % capacity; break;
                }
                Acquire(y);
                List<InputType> jSet = table[j, hj];
                try
                {
                    if (iSet.Remove(y))
                    {
                        if (jSet.Count < THRESHOLD)
                        {
                            jSet.Add(y);
                            return true;
                        }
                        else if (jSet.Count < LIST_SIZE)
                        {
                            jSet.Add(y);
                            i = 1 - i;
                            hi = hj;
                            j = 1 - j;
                        }
                        else
                        {
                            iSet.Add(y);
                            return false;
                        }
                    }
                    else if (iSet.Count >= THRESHOLD)
                    {
                        continue;
                    }
                    else
                    {
                        return true;
                    }
                }
                finally
                {
                    Release(y);
                }
            }
            return false;
        }
    }
}