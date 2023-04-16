using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task3
{
    public class Consumer<T> : Participant<T>
    {
        public Consumer(List<T> buffer) : base(buffer)
        {
        }

        public override void Operate()
        {
            while (!stop)
            {
                lock (Sync)
                {
                    while (buffer.Count == 0)
                    {
                        Monitor.Wait(Sync);
                    }
                    Consume();
                }
            }
        }

        public void Consume()
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} consumes");
            Thread.Sleep(pause);
            buffer.RemoveAt(0);
        }
    }
}
