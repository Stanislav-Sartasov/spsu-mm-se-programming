namespace Fibers.ProcessManager
{
    public interface IScheduler : IDisposable
    {
        public void Add(Process task);
        public void Run();
        public void RemoveFinished();
    }
}
