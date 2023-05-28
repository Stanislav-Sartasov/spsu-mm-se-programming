using DeanOffice.Algorithms.LazySet;
using DeanOffice.Algorithms.StripedCuckooHashSet;

namespace DeanOffice.ExamSystems
{
	public class LazyExamSystem : IExamSystem
	{
		private readonly LazySet<StudentCoursePair> _set;

		public LazyExamSystem()
		{
			_set = new LazySet<StudentCoursePair>();
		}

		public int Count { get; private set; }

		public void Add(long studentId, long courseId)
		{
			var didAdd = _set.Add(new StudentCoursePair(studentId, courseId));

			if (didAdd)
			{
				lock (this)
				{
					Count++;
				}
			}
		}

		public void Remove(long studentId, long courseId)
		{
			var didRemove = _set.Remove(new StudentCoursePair(studentId, courseId));

			if (didRemove)
			{
				lock (this)
				{
					Count--;
				}
			}
		}

		public bool Contains(long studentId, long courseId)
		{
			var contains = _set.Contains(new StudentCoursePair(studentId, courseId));
			return contains;
		}
	}
}
