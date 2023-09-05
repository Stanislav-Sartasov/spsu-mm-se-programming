namespace Plugins
{
    public interface IPlugin
    {
        public string Title { get; }
        public string Description { get; }
        public void Do();
    }
}