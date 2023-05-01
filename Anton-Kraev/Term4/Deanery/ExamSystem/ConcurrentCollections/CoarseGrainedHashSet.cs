using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamSystem.Interface;

namespace ExamSystem.ConcurrentCollections
{
    public class CoarseGrainedHashSetExamSystem : IExamSystem
    {
        public int Count => throw new NotImplementedException();

        public void Add(long studentId, long courseId)
        {
            throw new NotImplementedException();
        }

        public bool Contains(long studentId, long courseId)
        {
            throw new NotImplementedException();
        }

        public void Remove(long studentId, long courseId)
        {
            throw new NotImplementedException();
        }
    }
}
