

namespace DeansOffice
{

    public class Program
    {
        public static int Main(string[] args)
        {
            IExamSystem examSystemS = new ExamSystemS();
            IExamSystem examSystemH = new ExamSystemH(10000, 8);

            Console.WriteLine("LockFreeSet Exam System example");
            ExamSystemExample(examSystemS);

            Console.WriteLine("LockFreeHashSet Exam System example");
            ExamSystemExample(examSystemH);

            return 0;
        }

        public static void ExamSystemExample(IExamSystem examSystem)
        {
            for (int i = 0; i < 5000; i++)
                examSystem.Add(i, i);

            Console.WriteLine(examSystem.Contains(1, 1));

            examSystem.Remove(1, 1);

            Console.WriteLine(examSystem.Contains(1, 1));

            Console.WriteLine(examSystem.Count);
        }
    }
}
