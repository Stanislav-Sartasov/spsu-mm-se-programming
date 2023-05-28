using System.Net;

namespace P2PChat
{
    public static class Program
    {
        static int Main(string[] args)
        {
            const string serverIp = "127.0.0.1";
            const int serverPort = 8000;

            Console.WriteLine("Starting server...");

            var p1 = new Peer(serverIp, serverPort);
            p1.Activate();

            var p2 = new Peer(serverIp, serverPort + 1);
            p2.Activate();

            Console.WriteLine("Server is running");
            Console.WriteLine("If you want to stop server, just type 'stop'");

            p1.Connect(IPEndPoint.Parse($"{serverIp}:{serverPort + 1}"));

            p2.Send("Hello world!");

            while (Console.ReadLine() != "stop") { }

            p1.Dispose();
            p2.Dispose();

            return 0;
        }
    }
}