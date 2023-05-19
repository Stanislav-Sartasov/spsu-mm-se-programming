using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task5.Lock;

namespace Task5.ExamSystem
{
    public class ExamHashTable : IExamSystem
    {
        private FifoReadWriteLock[] _locks;
        volatile private List<Tuple<long, long>>[] _table;
        private volatile int _size;

        public ExamHashTable(int capacity)
        {
            _size = 0;
            _table = new List<Tuple<long, long>>[capacity];
            _locks = new FifoReadWriteLock[capacity];
            for (int i = 0; i < capacity; i++)
            {
                _table[i] = new List<Tuple<long, long>>();
                _locks[i] = new FifoReadWriteLock();
            }
        }

        public void Add(long studentId, long courseId)
        {
            Tuple<long, long> credit = new Tuple<long, long>(studentId, courseId);
            int lockId = Math.Abs(credit.GetHashCode() % _locks.Length);
            int tableId = Math.Abs(credit.GetHashCode() % _table.Length);

            _locks[lockId].WriterLock.Lock();
            if (!_table[tableId].Contains(credit))
            {
                _table[tableId].Add(credit);
                _size++;
            }
            _locks[lockId].WriterLock.Unlock();
            Resize();
        }

        public void Remove(long studentId, long courseId)
        {
            Tuple<long, long> credit = new Tuple<long, long>(studentId, courseId);
            int lockId = Math.Abs(credit.GetHashCode() % _locks.Length);
            int tableId = Math.Abs(credit.GetHashCode() % _table.Length);

            _locks[lockId].WriterLock.Lock();
            if (_table[tableId].Contains(credit))
            {
                _table[tableId].Remove(credit);
                _size--;
            }
            _locks[lockId].WriterLock.Unlock();
        }

        public bool Contains(long studentId, long courseId)
        {
            Tuple<long, long> credit = new Tuple<long, long>(studentId, courseId);
            int lockId = Math.Abs(credit.GetHashCode() % _locks.Length);
            int tableId = Math.Abs(credit.GetHashCode() % _table.Length);

            _locks[lockId].ReaderLock.Lock();
            bool res = _table[tableId].Contains(credit);
            _locks[lockId].ReaderLock.Unlock();

            return res;
        }

        public int Count
        {
            get
            {
                return _size;
            }
        }

        private void Resize()
        {
            if (_size / _table.Length < 8)
                return;

            int oldCapacity = _table.Length;

            for (int i = 0; i < _locks.Length; i++)
            {
                _locks[i].WriterLock.Lock();
            }

            try
            {
                if (oldCapacity != _table.Length)
                    return;

                List<Tuple<long, long>>[] oldTable = _table;
                int newCapacity = oldCapacity * 2;
                _table = new List<Tuple<long, long>>[newCapacity];
                for (int i = 0; i < newCapacity; i++)
                {
                    _table[i] = new List<Tuple<long, long>>();
                }
                for (int i = 0; i < oldTable.Length; i++)
                {
                    for (int j = 0; j < oldTable[i].Count; j++)
                    {
                        _table[oldTable[i][j].GetHashCode() % _table.Length].Add(oldTable[i][j]);
                    }
                }
            }
            finally
            {
                for (int i = 0; i < _locks.Length; i++)
                {
                    _locks[i].WriterLock.Unlock();
                }
            }
        }
    }
}
