using System.Net;
using PeerToPeerChat.Chat;

namespace PeerToPeerChat
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Введите порт");
            var port = int.Parse(Console.ReadLine()!);

            try
            {
                NotMain(port);
            }
            catch (Exception e)
            {
                File.WriteAllText("Error-" + port + ".txt", e.Message);
                File.WriteAllText("Error-" + port + ".txt", e.StackTrace);
                Console.ReadKey();
            }
        }

        public static void NotMain(int port)
        {
            var chat = new ChatClient(port);

            while (true)
            {
                var cmd = Console.ReadLine();
                var t = cmd.Split()[0];
                var m = string.Join(" ", cmd.Split().Skip(1));

                switch (t)
                {
                    case "connect":
                        chat.ConnectTo(IPEndPoint.Parse("127.0.0.1:" + m));
                        break;

                    default:
                        chat.Send(cmd);
                        break;
                }

            }
        }
    }
}