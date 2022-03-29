using System;
using MyHashTable;

namespace GenericsTask
{
    public class Program
    {
        static void Main()
        {
            MyHashTable<char, int> ht = new MyHashTable<char, int>(10);
            string keys = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            for (int i = 0; i < keys.Length; i++)
            {
                ht[keys[i]] = (int)keys[i];
            }

            ht.Print();
            Console.WriteLine("--------------------");

            Console.Write($"\nA's value before updating is {ht['A']}; ");
            ht['A'] = 99999999;
            Console.Write($"and A's value after updating {ht['A']}\n");

            Console.WriteLine($"Is B in HashTable: {ht.Include('B')}");
            ht.Remove('B');
            Console.WriteLine("Removing B......");
            Console.WriteLine($"Is B in HashTable: {ht.Include('B')}");
        }
    }
}