using NUnit.Framework;
using Dekanat.DekanatLib;

namespace DekanatUnitTests
{
    public class NodeUnitTests
    {
        [Test]
        public void WithStudentIdTest()
        {
            var node = new Node(1, 1).WithStudentId(2);

            Assert.AreEqual(2, node.StudentId);
        }

        [Test]
        public void WithCourseIdTest()
        {
            var node = new Node(1, 1).WithCourseId(2);

            Assert.AreEqual(2, node.CourseId);
        }

        [Test]
        public void Equals()
        {
            var node1 = new Node(1, 1);
            var node2 = new Node(1, 1);

            Assert.IsTrue(node1.Equals(node2));
            Assert.IsTrue(node1.Equals(node1));

            Assert.IsFalse(node1.Equals(null));
            Assert.IsFalse(node1.Equals(new object()));
        }
    }
}
