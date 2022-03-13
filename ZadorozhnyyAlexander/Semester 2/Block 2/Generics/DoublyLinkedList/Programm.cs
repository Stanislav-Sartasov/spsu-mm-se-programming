namespace DoublyLinkedList
{
    class Programm
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This program shows the realisation of a doubly linked list; \n");
            int[] res = new int[3];
            var testList = new DoubleLinkedListClass<int>();
            Console.WriteLine("Adding elements --> 1, 5, 9: ");
            testList.Add(1);
            testList.Add(9);
            testList.Add(5);
            
            res = testList.GetAllElements();
            Console.WriteLine("Now in list: ");
            for (int i = 0; i < testList.Length; i++)
                Console.WriteLine(res[i]);

            Console.WriteLine("Delete a few elements --> 0, 9: ");
            testList.Remove(9);
            testList.Remove(0);

            res = testList.GetAllElements();
            Console.WriteLine("Now in list: ");
            for (int i = 0; i < testList.Length; i++)
                Console.WriteLine(res[i]);

            Console.WriteLine("Let's check if there are some items in the list --> 1, 100: ");
            Console.WriteLine($"Element 1 in list : {testList.Find(1)}");
            Console.WriteLine($"Element 100 in list : {testList.Find(100)}");

            testList.Clear();
        }
    }
}
