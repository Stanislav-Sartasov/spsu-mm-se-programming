using System;

namespace ChatParticipants
{
    public class Program
    {
        static int Main(string[] args)
        {
            var server = new Server(Constants.ServerIP, Constants.ServerPort);
            server.Start();

            Console.WriteLine("The server is running.");
            Console.WriteLine("To stop the server, write: exit");

            string input;
            while (true)
            {
                input = Console.ReadLine();
                if (input == "exit")
                {
                    server.Dispose();
                    break;
                }
            }

            return 0;
        }
    }
}
