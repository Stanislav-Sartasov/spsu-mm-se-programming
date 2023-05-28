using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeanOffice.ExamSystems
{
	public class StudentCoursePair
	{
		public long Student;
		public long Course;

		public StudentCoursePair(long student, long course)
		{
			Student = student;
			Course = course;
		}

		public override int GetHashCode()
		{
			return Math.Abs((int)(Student + 2346421 * Course));
		}

		public override bool Equals(object other)
		{
			if (other is not StudentCoursePair) return false;

			return ((StudentCoursePair)other).Student == Student && ((StudentCoursePair)other).Course == Course;
		}
	}
}
