namespace Interfaces
{
    public interface IReader
    {
        public bool IsRead { get; }
        public string ErrorMessenge { get; }
        public string Read();
    }
}