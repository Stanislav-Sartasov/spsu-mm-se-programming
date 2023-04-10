using System;
using ProducerConsumer;

namespace Task3;

class Program
{
    static void Main(string[] args)
    {
        Manager manager = new Manager(3, 3);
        Console.WriteLine("Run");
        manager.Start();

        Console.ReadKey(true);
        manager.Stop();
        Console.WriteLine("Stop");
    }
}