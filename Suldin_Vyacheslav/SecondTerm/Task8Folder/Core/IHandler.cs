namespace Core
{
    public interface IHandler
    {
        public string GetLine();
        public void Show(string line);
    }
}