using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task3
{
    public abstract class Participant<T>
    {
        public static readonly object Sync = new object();

        protected int pause = 1000;
        protected volatile bool stop;
        protected Thread thread;
        protected List<T> buffer;

        public abstract void Operate();

        public Participant(List<T> buffer)
        {
            this.buffer = buffer;
            thread = new Thread(Operate);
        }

        public void Start()
        {
            Volatile.Write(ref stop, false);
            thread.Start();
        }

        public void Stop()
        {
            Volatile.Write(ref stop, true);
            thread.Join();
        }
    }
}
