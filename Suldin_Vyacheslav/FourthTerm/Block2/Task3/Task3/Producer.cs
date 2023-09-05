using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Task3
{
    public class Producer<T> : Participant<T>
    {

        public Producer(List<T> buffer) : base(buffer)
        {
        }
        public override void Operate()
        {
            while (!stop)
            {
                lock (Sync)
                {
                    buffer.Add(Produce());
                    Monitor.PulseAll(Sync);
                }
            }
        }

        public T Produce()
        {
            Console.WriteLine($"{Thread.CurrentThread.ManagedThreadId} produces");
            Thread.Sleep(pause);
            return default;
        }
    }
}
