namespace RequestManagement
{
    public interface IRequest
    {
        public string Response { get; }
        public bool Connected { get; }
        public string Get();
    }
}
