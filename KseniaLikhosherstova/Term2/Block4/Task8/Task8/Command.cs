namespace Task8
{
    public class Command
    {
        public string? Name { get; set; }
        public string? Arguments { get; set; }
        public Command? PipeCommand { get; set; }
    }
}
