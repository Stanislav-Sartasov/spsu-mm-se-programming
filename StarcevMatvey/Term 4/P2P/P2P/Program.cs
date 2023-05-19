using P2P.Chat;
using System.Net;

namespace P2P
{
    class Progarm
    {
        public static void Main (string[] args)
        {
            var port = Utils.Utils.GetPositiveInt(args[0]);

            if (args.Length < 0) throw new Exception($"I think that you forgot some arguments of command line (port)");
            if (port == 0) throw new Exception($"Uh, sorry, port must be possitive integer (>0)");

            var client = new Client(port);

            while (true)
            {
                var cmd = Console.ReadLine();

                var tokens = cmd.Split();

                if (tokens.Length < 2)
                {
                    Console.WriteLine($"I recognize only next patterns:\n" +
                        $"\\any char exept 'c' and 'q'\\ \\your messenge to send\\\n" +
                        $"c \\port that you want to connect\\\n" +
                        $"q q - to quit");
                }
                else if (tokens[0] == "c") client.Conect(IPEndPoint.Parse($"127.0.0.1:{tokens[1]}"));
                else if (tokens[0] == "q")
                {
                    Console.WriteLine($"Quiting... (");
                    break;
                }
                else client.Send(tokens[1]);
            }
        }
    }
}