using System.Diagnostics;

namespace Commands
{
    public class SystemProcess : ICommand
    {
        public bool RequiredArgs { get; private set; } = true;
        public bool NeedToBePrinted { get; private set; } = false;
        public bool IsSystem { get; private set; } = true;


        public string[] Run(string[] args)
        {
            try
            {
                if (args.Length == 1)
                {
                    Process.Start(new ProcessStartInfo(args[0]));
                }
                else if (args.Length == 2)
                {
                    Process.Start(new ProcessStartInfo(args[0], args[1]));
                }
            }
            catch
            {
                Console.WriteLine("Something wrong with System Process. Please try again");
            }

            return args;
        }
    }
}
