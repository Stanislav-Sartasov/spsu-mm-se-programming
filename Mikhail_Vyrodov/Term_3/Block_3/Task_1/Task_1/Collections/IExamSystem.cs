using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1.Collections
{
    public interface IExamSystem
    {
        public void Add(long studentId, long courseId);
        public void Remove(long studentId, long courseId);
        public bool Contains(long studentId, long courseId);
        public int Count { get; }

    }
}
