namespace DoubleLinkedList
{
    public class Program
    {
        public static void Main()
        {
            PrintDescription();
            Console.WriteLine("Наполнение списка:");
            DoubleLinkedList<int> testList = new(1);
            testList.Add(1);
            testList.Add(3);
            testList.Add(2);
            testList.Add(-3);
            testList.Add(-1);
            testList.Add(0);
            testList.PrintElements();
            Console.WriteLine("Добавление элементов:");
            testList.Add(-1);
            testList.Add(-3);
            testList.Add(3);
            testList.PrintElements();
            Console.WriteLine("Удаление элементов:");
            testList = testList.Remove(0);
            testList = testList.Remove(2);
            testList = testList.Remove(1);
            testList = testList.Remove(3);
            testList.PrintElements();
            Console.WriteLine("Поиск элементов:");
            Console.WriteLine("Поиск элемента 10: "+testList.Find(10));
            Console.WriteLine("Поиск элемента 1: " + testList.Find(1));
            Console.WriteLine("Поиск элемента -3: " + testList.Find(-3));
        }

        private static void PrintDescription()
        {
            Console.WriteLine("Данная программа показывает реализацию двусвязного списка");
            Console.WriteLine("Сначала список будет наполнен различными элементами, потом");
            Console.WriteLine("будет добавлено несколько элементов, потом будет удалено несколько элементов.");
            Console.WriteLine("После этого будет произведён поиск некоторых элементов");
        }
    }
}