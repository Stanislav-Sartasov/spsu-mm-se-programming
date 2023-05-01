namespace Dekanat.DekanatLib
{
    public class Node : IEquatable<Node>
    {
        public long StudentId { get; private set; }
        public long CourseId { get; private set; }

        public Node(long studentId, long courseId)
        {
            StudentId = studentId;
            CourseId = courseId;
        }

        public Node WithStudentId(long studentId)
        {
            return new Node(studentId, CourseId);
        }

        public Node WithCourseId(long courseId)
        {
            return new Node(StudentId, courseId);
        }

        public override bool Equals(object o) => this.Equals(o as Node);

        public bool Equals(Node node)
        {
            if (node == null) return false;

            if (Object.ReferenceEquals(this, node)) return true;

            if (this.GetType() != node.GetType()) return false;

            return this.StudentId == node.StudentId && this.CourseId == node.CourseId;
        }
    }
}
