namespace Requests
{
    public interface IGetRequest
    {
        public bool Connect { get; }
        public string Accept { get; }
        public string Host { get; }
        public string Referer { get; }
        public string Response { get; }
        public Dictionary<string, string> Headers { get; }
        public void AddToHeaders(string key, string value);
        public void Run();
    }
}