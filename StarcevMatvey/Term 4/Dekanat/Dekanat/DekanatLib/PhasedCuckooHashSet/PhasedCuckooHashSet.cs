using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dekanat.DekanatLib.PhasedCuckooHashSet
{
    public class PhasedCuckooHashSet : IExamSystem
    {
        private const int THRESHHOLD = 20;
        private const int LIST_SIZE = 40;
        private const int LIMIT = 50;

        private int _setSize;
        private int _capacity;
        private List<Node>[,] _table;
        private Mutex[,] _locks;

        private delegate int Hashing(Node x, int hash);
        private Hashing[] _hash;

        public PhasedCuckooHashSet(int size)
        {
            _setSize = 0;
            _capacity = size;
            _table = new List<Node>[2, size];
            _locks = new Mutex[2, size];

            for (var i = 0 ; i < 2; i++)
            {
                for (var j = 0; j < size; j++)
                {
                    _table[i, j] = new List<Node>(LIST_SIZE);
                    _locks[i, j] = new Mutex();
                }
            }

            _hash = new Hashing[2]
                .Select
                (
                    (x, i) => 
                        new Hashing((x, hash) => ((x.GetHashCode() + 7 * i) % hash))
                )
                .ToArray();
        }

        public void Add(long studentId, long courseId)
        {
            var node = new Node(studentId, courseId);

            if (!Add(node))
                throw new Exception($"I can't add a node with student id {studentId} and course id {courseId}");
        }

        public void Remove(long studentId, long courseId)
        {
            var node = new Node(studentId, courseId);

            if (!Remove(node))
                throw new Exception($"I can't remove a node with student id {studentId} and course id {courseId}");
        }

        public bool Contains(long studentId, long courseId)
            => Contains(new Node(studentId, courseId));

        public int Count() => _setSize;

        private void Acquire(Node x)
        {
            for (var i = 0; i < 2; i++)
                _locks[i, _hash[i](x, _locks.GetLength(i))].WaitOne();
        }

        private void Release(Node x)
        {
            for (var i = 0; i < 2; i++)
                _locks[i, _hash[i](x, _locks.GetLength(i))].ReleaseMutex();
        }

        private bool Contains(Node x)
        {
            Acquire(x);

            try
            {
                var set0 = _table[0, _hash[0](x, _capacity)];
                var set1 = _table[1, _hash[1](x, _capacity)];

                return set0.Contains(x) || set1.Contains(x);
            }
            finally
            {
                Release(x);
            }
        }

        private bool Add(Node x)
        {
            Acquire(x);

            int h0 = _hash[0](x, _capacity), h1 = _hash[1](x, _capacity);
            int i = -1, h = -1;
            var mustResize = false;

            try
            {
                if (Contains(x)) return false;

                var set0 = _table[0, h0];
                var set1 = _table[1, h1];

                if (set0.Count < THRESHHOLD)
                {
                    set0.Add(x);
                    _setSize++;
                    return true;
                }
                else if (set1.Count < THRESHHOLD)
                {
                    set1.Add(x);
                    _setSize++;
                    return true;
                }
                else if (set0.Count < LIST_SIZE)
                {
                    set0.Add(x);
                    _setSize++;
                    (i, h) = (0, 0);
                }
                else if (set1.Count < LIST_SIZE)
                {
                    set1.Add(x);
                    _setSize++;
                    (i, h) = (1, 1);
                }
                else mustResize = true;

                if (mustResize)
                {
                    Resize();
                    Add(x);
                }
                else if (!Relocate(i, h)) Resize();

                return true;
            }
            finally
            {
                Release(x);
            }
        }

        private bool Relocate(int i, int hi)
        {
            var j = 1 - i;

            for (var round = 0; round < LIMIT; round++)
            {
                var iSet = _table[i, hi];
                var y = iSet.First();

                var hj = _hash[j](y, _capacity);

                Acquire(y);

                var jSet = _table[j, hj];

                try
                {
                    if (iSet.Remove(y))
                    {
                        if (jSet.Count < THRESHHOLD)
                        {
                            jSet.Add(y);
                            return true;
                        }
                        else if (jSet.Count < LIST_SIZE)
                        {
                            jSet.Add(y);
                            (i, j, hi) = (1 - i, 1 - j, hj);
                        }
                        else
                        {
                            iSet.Add(y);
                            return true;
                        }
                    }
                    else if (iSet.Count >= THRESHHOLD) continue;
                    else return true;
                }
                finally
                {
                    Release(y);
                }
            }

            return true;
        }

        private bool Remove(Node x)
        {
            Acquire(x);

            try
            {
                var set0 = _table[0, _hash[0](x, _capacity)];

                if (set0.Contains(x))
                {
                    set0.Remove(x);
                    _setSize--;
                    return true;
                }
                else
                {
                    var set1 = _table[1, _hash[1](x, _capacity)];

                    if (set1.Contains(x))
                    {
                        set1.Remove(x);
                        _setSize--;
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

        private void Resize()
        {
            var oldCapa = _capacity;

            for (var i = 0; i < _locks.GetLength(0); i++)
                _locks[0, i].WaitOne();

            try
            {
                if (_capacity == oldCapa) return;

                var oldTable = _table;
                _capacity *= 2;

                for (var i = 0; i < 2; i++)
                {
                    for (var j = 0; j < _capacity; j++)
                    {
                        _table[i, j] = new List<Node>(LIST_SIZE);
                    }
                }

                foreach (var set in oldTable)
                {
                    foreach (var node in set)
                    {
                        Add(node);
                    }
                }
            }
            finally
            {
                for (var i = 0; i < _locks.GetLength(0); i++)
                    _locks[0, i].ReleaseMutex();
            }
        }
    }
}
