namespace ExamSystem.Core.Sets.Interface;

public interface ISet<in T>
{
	bool Add(T item);
	bool Remove(T item);
	bool Contains(T item);
}