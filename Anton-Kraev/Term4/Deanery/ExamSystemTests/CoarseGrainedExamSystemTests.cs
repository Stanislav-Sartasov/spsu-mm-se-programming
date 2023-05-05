using System.Reflection;
using ExamSystem.Implementation;

namespace ExamSystemTests
{
    public class CoarseGrainedExamSystemTests
    {
        [Test]
        public void AddTest()
        {
            var examSystem = new CoarseGrainedExamSystem();

            for (int i = 0; i < 10; i++)
            {
                examSystem.Add(i, i + 1);
            }

            Assert.AreEqual(10, examSystem.Count);
            Assert.IsTrue(examSystem.Contains(1, 2));
            Assert.IsFalse(examSystem.Contains(2, 1));
        }

        [Test]
        public void RemoveTest()
        {
            var examSystem = new CoarseGrainedExamSystem();

            for (int i = 0; i < 10; i++)
            {
                examSystem.Add(i, i + 1);
            }

            Assert.AreEqual(10, examSystem.Count);
            Assert.IsTrue(examSystem.Contains(3, 4));

            for (int i = 3; i < 7; i++)
            {
                examSystem.Remove(i, i + 1);
            }

            Assert.AreEqual(6, examSystem.Count);
            Assert.IsFalse(examSystem.Contains(3, 4));
        }

        [Test]
        public void CollisionTest()
        {
            var examSystem = new CoarseGrainedExamSystem();

            examSystem.Add(3, 4);
            examSystem.Add(4, 1);
            examSystem.Add(6, 7);
            examSystem.Remove(3, 4);

            Assert.IsTrue(examSystem.Contains(4, 1));
            Assert.IsTrue(examSystem.Contains(6, 7));
            Assert.IsFalse(examSystem.Contains(3, 4));
        }
        
        [Test]
        public void ResizeTest()
        {
            var examSystem = new CoarseGrainedExamSystem();

            for (int i = 0; i < 121; i++)
            {
                examSystem.Add(i, i + 1);
            }

            Assert.AreEqual(121, examSystem.Count);
        }
    }
}