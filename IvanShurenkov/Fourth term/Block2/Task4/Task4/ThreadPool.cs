namespace Task4
{
    public class ThreadPool: IDisposable
    {
        private List<Thread> threads = new List<Thread>();
        private Queue<Action> actions = new Queue<Action>();
        private volatile bool stop = false;

        public ThreadPool(int cntThreads) 
        {
            for (int i = 0; i < cntThreads; i++)
            {
                threads.Add(new Thread(Run));
            }
            threads.ForEach(thread => thread.Start());
        }

        public void Enqueue(Action action)
        {
            Monitor.Enter(actions);
            try
            {
                actions.Enqueue(action);
                Monitor.Pulse(actions);
            }
            finally
            {
                Monitor.Exit(actions);
            }
        }

        public void Dispose() 
        {
            stop = true;
            Monitor.Enter(actions);
            try
            {
                actions.Clear();
                Monitor.PulseAll(actions);
            }
            finally
            {
                Monitor.Exit(actions);
            }
            threads.ForEach(thread => thread.Join());
            threads.Clear();
        }

        private void Run()
        {
            while (!stop)
            {
                Action action = null;
                Monitor.Enter(actions);
                try
                {
                    while (!stop && actions.Count == 0)
                    {
                        Monitor.Wait(actions);
                    }

                    if (actions.Count > 0)
                    {
                        action = actions.Dequeue();
                    }

                    Monitor.Pulse(actions);
                }
                finally
                {
                    Monitor.Exit(actions);
                }

                if (action != null)
                {
                    action();
                }
            }
        }
    }
}
