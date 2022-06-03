namespace Bash
{
    public class CommandInfo
    {
        public string ShortName { get; }
        public List<string>? Arguments{ get; private set; }

        public CommandInfo(string shortName, List<string>? arguments)
        {
            ShortName = shortName;
            Arguments = arguments;
        }

        public bool AddArguments(IEnumerable<string>? arguments)
        {
            if (arguments is null || !arguments.Any())
            {
                return false;
            }

            if (Arguments is null)
            {
                Arguments = new List<string>();
            }
            
            Arguments.AddRange(arguments.Where(x => x is not null));

            return true;
        }

        public bool InsertArgumentsFirst(IEnumerable<string>? arguments)
        {
            if (arguments is null || !arguments.Any())
            {
                return false;
            }

            if (Arguments is null)
            {
                Arguments = new List<string>();
            }

            Arguments.InsertRange(0, arguments.Where(x => x is not null));

            return true;
        }

        public bool InsertArgumentFirst(string? argument)
        {
            if (argument is null)
            {
                return false;
            }

            if (Arguments is null)
            {
                Arguments = new List<string>();
            }

            Arguments.Insert(0, argument);

            return true;
        }
    }
}
