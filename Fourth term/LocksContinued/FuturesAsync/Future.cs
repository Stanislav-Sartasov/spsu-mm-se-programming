using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace FuturesAsync
{
    internal class Future<T>
    {
        readonly Func<T> func;
        private Thread thread;
        object startLock = new object();
        object resultLock = new object();
        volatile bool isStarted = false;
        volatile bool isFinished = false;
        volatile Exception fault = null;


        T result = default(T);

        public Future(Func<T> func)
        {
            if (func == null) throw new ArgumentException("func");
            this.func = func;
        }

        public bool IsFaulted
        {
            get
            {
                if (!isFinished)
                {
                    thread.Join();
                }
                return fault != null;
            }
        }

        public bool IsCompleted
        {
            get
            {
                return isFinished;
            }
        }

        public T Result
        {
            get
            {
                if(!isFinished)
                {
                    thread.Join();
                }
                if (fault != null) throw fault;

                lock (resultLock)
                {
                    return result;
                }
            }
        }

        public Exception Fault
        {
            get
            {
                if (!isFinished)
                {
                    thread.Join();
                }
                return fault;
            }
        }

        private void ThreadFunc()
        {
            try
            {
                var intermediateResult = func();
                lock(resultLock)
                {
                    result = intermediateResult;
                }
            }
            catch(Exception ex)
            {
                fault = ex;
            }
            finally
            {
                isFinished = true;
            }
        }


        public void Start()
        {
            lock(startLock)
            {
                if (isStarted)
                {
                    throw new InvalidOperationException("Future is already started");
                }

                isStarted = true;
                thread = new Thread(ThreadFunc);
                thread.Start();
            }
        }
    }
}
