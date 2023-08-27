namespace CreditSystem;

public interface ICreditSystem
{
    public int Count();

    public void Add(long studentId, long courseId);

    public void Remove(long studentId, long courseId);

    public bool Contains(long studentId, long courseId);
}