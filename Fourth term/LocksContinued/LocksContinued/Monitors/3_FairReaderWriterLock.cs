using System.Threading;

namespace LocksContinued.Monitors
{
    public class FifoReadWriteLock
    {
        volatile int readAcquires, readReleases;
        volatile bool writer;
        object sync = new object();
        ILock readLock, writeLock;

        public FifoReadWriteLock()
        {
            readAcquires = readReleases = 0;
            writer = false;
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
            private FifoReadWriteLock parent;

            public ReadLock(FifoReadWriteLock parent)
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
                    parent.readAcquires++;
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
                    parent.readReleases++;
                    if (parent.readAcquires == parent.readReleases)
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
            private FifoReadWriteLock parent;

            public WriteLock(FifoReadWriteLock parent)
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
                    parent.writer = true;
                    while (parent.readAcquires != parent.readReleases)
                    {
                        Monitor.Wait(parent.sync);
                    }
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
