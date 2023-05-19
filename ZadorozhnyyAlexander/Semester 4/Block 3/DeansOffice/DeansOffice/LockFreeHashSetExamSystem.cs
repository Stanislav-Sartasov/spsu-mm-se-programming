namespace DeansOffice
{
    public class ExamSystemH : IExamSystem
    {
        private LockFreeHashSet<(long, long)> lockFreeSet;

        public ExamSystemH(int capacity = 1000, int treshold = 4)
        {
            lockFreeSet = new LockFreeHashSet<(long, long)>(capacity, treshold);
        }

        public int Count => lockFreeSet.Count();

        public void Add(long studentId, long courseId)
        {
            lockFreeSet.Add((studentId, courseId));
        }

        public void Remove(long studentId, long courseId)
        {
            lockFreeSet.Remove((studentId, courseId));
        }

        public bool Contains(long studentId, long courseId)
        {
            return lockFreeSet.Contains((studentId, courseId));
        }
    }
}
