namespace Core.Chat;

public interface IChat<T>
{
    public IEnumerable<T> GetAll();
    public IEnumerable<T> Get(Predicate<T> predicate);
    public void SaveMessage(T message);
   
}