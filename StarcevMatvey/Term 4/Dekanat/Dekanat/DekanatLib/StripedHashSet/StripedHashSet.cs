using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dekanat.DekanatLib.StripedHashSet
{
    public class StripedHashSet : ABaseHashSet, IExamSystem
    {
        private Mutex[] _locks;

        public StripedHashSet(int size) : base(size)
        {
            _locks = new Mutex[size]
                .Select(x => new Mutex())
                .ToArray();
        }

        protected override bool PolicyDemandsResize => _setSize / _table.Length > 4;

        public void Add(long studentId, long courseId)
        {
            var node = new Node(studentId, courseId);

            if (!Add(node))
                throw new Exception($"I can't add a node with student id {studentId} and course id {courseId}");
        }

        public bool Contains(long studentId, long courseId)
            => Contains(new Node(studentId, courseId));

        public void Remove(long studentId, long courseId)
        {
            var node = new Node(studentId, courseId);

            if (!Remove(node))
                throw new Exception($"I can't remove a node with student id {studentId} and course id {courseId}");
        }

        protected override int Hash(Node x)
        {
            return (int)x.StudentId % _table.Length;
        }

        protected override void Acquire(Node x)
        {
            _locks[Hash(x)].WaitOne();
        }

        protected override void Release(Node x)
        {
            _locks[Hash(x)].ReleaseMutex();
        }

        protected override void Resize()
        {
            var oldSize = _table.Length;

            _locks
                .ToList()
                .ForEach(x => x.WaitOne());

            try
            {
                if (oldSize != _table.Length) return;
                var newSize = 2 * oldSize;

                var oldTable = _table;
                _table = new List<Node>[newSize]
                    .Select(x => new List<Node>())
                    .ToArray();
                oldTable
                    .ToList()
                    .ForEach(x => x.ForEach(x => _table[Hash(x)].Add(x)));
            }
            finally
            {
                _locks
                    .ToList()
                    .ForEach(x => x.ReleaseMutex());
            }
        }
    }
}
