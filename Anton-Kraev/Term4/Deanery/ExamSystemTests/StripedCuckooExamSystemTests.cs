using ExamSystem.Implementation;

namespace ExamSystemTests
{
    public class StripedCuckooExamSystemTests
    {
        [Test]
        public void AddTest()
        {
            var examSystem = new StripedCuckooExamSystem();

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
            var examSystem = new StripedCuckooExamSystem();

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
            var examSystem = new StripedCuckooExamSystem();

            examSystem.Add(3, 4);
            examSystem.Add(18, 19);
            examSystem.Add(33, 34);
            examSystem.Remove(3, 4);

            Assert.IsTrue(examSystem.Contains(18, 19));
            Assert.IsTrue(examSystem.Contains(33, 34));
            Assert.IsFalse(examSystem.Contains(3, 4));
        }

        [Test]
        public void ResizeTest()
        {
            var examSystem = new StripedCuckooExamSystem();

            for (int i = 0; i < 9; i++)
            {
                examSystem.Add(15 * i, 30 * i);
            }

            Assert.AreEqual(9, examSystem.Count);
        }
    }
}