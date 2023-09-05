namespace Commands
{
    public interface ICommand
    {
        public bool RequiredArgs { get; }
        public bool NeedToBePrinted { get; }
        public bool IsSystem { get; }

        public string[] Run(string[] args);
    }
}