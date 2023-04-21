namespace Fibers.ProcessManager
{
    public struct TaskInfo
    {
        public uint Id { get; }
        public int Priority { get;  }
        public DateTime Time { get; }
        public bool Paused { get; set; }

        public TaskInfo(uint id, int priority, DateTime time)
        {
            Id = id;
            Priority = priority;
            Time = time;
            Paused = false;
        }
    }
}
