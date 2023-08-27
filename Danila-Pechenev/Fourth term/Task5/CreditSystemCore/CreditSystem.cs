namespace CreditSystem;

public class CreditSystem : ICreditSystem
{
    private readonly DataStructures.ISet<Credit> set;

    public CreditSystem(DataStructures.ISet<Credit> set)
    {
        this.set = set;
    }

    public int Count()
    {
        return set.Count();
    }

    public void Add(long studentId, long courseId)
    {
        var credit = new Credit(studentId, courseId);
        set.Add(credit);
    }

    public void Remove(long studentId, long courseId)
    {
        var credit = new Credit(studentId, courseId);
        set.Remove(credit);
    }

    public bool Contains(long studentId, long courseId)
    {
        var credit = new Credit(studentId, courseId);
        return set.Contains(credit);
    }
}