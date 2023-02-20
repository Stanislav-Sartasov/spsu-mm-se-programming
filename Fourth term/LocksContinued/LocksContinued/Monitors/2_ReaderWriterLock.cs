using System.Threading;

namespace LocksContinued.Monitors
{
    public class SimpleReadWriteLock
    {
        volatile int readers = 0;
        volatile bool writer = false;

        object sync = new object();
        ILock readLock, writeLock;

        public SimpleReadWriteLock()
        {
            readLock = new ReadLock(this);
            writeLock = new WriteLock(this);
        }

        public ILock ReaderLock
        {
            get
            {
                return readLock;
            }
        }

        public ILock WriterLock
        {
            get
            {
                return writeLock;
            }
        }

        class ReadLock : ILock
        {
            private SimpleReadWriteLock parent;

            public ReadLock(SimpleReadWriteLock parent)
            {
                this.parent = parent;
            }

            public void Lock()
            {
                Monitor.Enter(parent.sync);
                try
                {
                    while (parent.writer)
                    {
                        Monitor.Wait(parent.sync);
                    }
                    parent.readers++;
                }
                finally
                {
                    Monitor.Exit(parent.sync);
                }
            }

            public void Unlock()
            {
                Monitor.Enter(parent.sync);
                try
                {
                    parent.readers--;
                    if (parent.readers == 0)
                        Monitor.PulseAll(parent.sync);
                }
                finally
                {
                    Monitor.Exit(parent.sync);
                }
            }
        }

        class WriteLock : ILock
        {
            private SimpleReadWriteLock parent;

            public WriteLock(SimpleReadWriteLock parent)
            {
                this.parent = parent;
            }

            public void Lock()
            {
                Monitor.Enter(parent.sync);
                try
                {
                    while (parent.readers > 0 || parent.writer)
                    {
                        Monitor.Wait(parent.sync);
                    }
                    parent.writer = true;
                }
                finally
                {
                    Monitor.Exit(parent.sync);
                }
            }

            public void Unlock()
            {
                Monitor.Enter(parent.sync);
                try
                {
                    parent.writer = false;
                    Monitor.PulseAll(parent.sync);
                }
                finally
                {
                    Monitor.Exit(parent.sync);
                }
            }
        }
    }
}
