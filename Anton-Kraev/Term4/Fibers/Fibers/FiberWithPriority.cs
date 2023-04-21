namespace Fibers;

public class FiberWithPriority : Fiber
{
    public int Priority { get; }

    public FiberWithPriority(Action action, int priority) : base(action)
    {
        Priority = priority;
    }
}