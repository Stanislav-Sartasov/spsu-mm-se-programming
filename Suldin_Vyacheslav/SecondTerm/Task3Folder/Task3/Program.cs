using System;
using System.Collections.Generic;
using MyListLibrary;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            MyList<int> lost = new MyList<int>();
            int[] arr = new int[] { 4, 5, 6 };
            lost.AddFirst(1);
            lost.AddLast(2);
            lost.Delete(2);
            lost.Find(1);
            lost.Length();
            lost.AddRange(arr);

        }
    }
}
