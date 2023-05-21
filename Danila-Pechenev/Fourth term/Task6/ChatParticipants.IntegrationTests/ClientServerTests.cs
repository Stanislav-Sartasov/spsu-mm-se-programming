using NUnit.Framework;

namespace ChatParticipants.IntegrationTests
{
    public class ClientServerTests
    {
        [Test]
        public void ClientServerInteractionTest()
        {
            var server = new Server("127.0.0.1", 10000);
            server.Start();

            var firstClient = new Client("127.56.13.75", 4040, Mock);
            firstClient.Start();

            var secondClient = new Client("127.29.156.01", 4567, Mock);
            secondClient.Start("127.56.13.75", 4040);

            server.Dispose();  // This is a P2P chat!

            secondClient.SendMessage("Hi!");
            firstClient.SendMessage("Hello!");
            secondClient.SendMessage("How are you?");
            firstClient.SendMessage("Very fine, thanx =)");

            firstClient.Dispose();
            secondClient.Dispose();

            Assert.Pass();
        }

        private void Mock(string message)
        {
        }
    }
}
