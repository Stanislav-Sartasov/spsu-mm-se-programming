using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Task6.Network;

namespace Task6Test
{
    public class ClienTest
    {
        private List<Client> clients = new List<Client>();
        private List<TestReceiver> receivers = new List<TestReceiver>();
        private List<string> sendedMessages = new List<string>();
        [SetUp]
        public void Setup()
        {
        }

        private void InitLists(int n)
        {
            clients = new List<Client>(n);
            receivers = new List<TestReceiver>(n);
            sendedMessages = new List<string>(n);
            for (int i = 0; i < n; i++)
            {
                receivers.Add(new TestReceiver(i));
                clients.Add(new Client(5000 + i, receivers[i]));
                sendedMessages.Add("TestClient" + i.ToString());
            }
        }

        private void AssertLists(int n, List<int> choiseClient)
        {
            for (int i = 0; i < n; i++)
            {
                Assert.That(receivers[i].Log.Count, Is.EqualTo(choiseClient.Count + n - 1));

                for (int j = n - 1; j < receivers[i].Log.Count; j++)
                {
                    int choiseId = j - n + 1;
                    string actual = String.Empty;
                    if (choiseClient[choiseId] == i)
                    {
                        actual = "S" + sendedMessages[choiseClient[choiseId]];
                    }
                    else
                    {
                        actual = "R" + sendedMessages[choiseClient[choiseId]];
                    }
                    Assert.That(actual, Is.EqualTo(receivers[i].Log[j]));
                }
            }
        }

        [Test]
        public void TestTwoClients()
        {
            int n = 2;
            InitLists(n);

            clients[0].Connect(clients[1].IPAddr, clients[1].Port);

            List<int> choiseClient = new List<int> { 0, 0, 1, 1, 0, 1, 0, 1 };
            Thread.Sleep(10);
            for (int i = 0; i < choiseClient.Count; i++)
            {
                int clientId = choiseClient[i];
                clients[clientId].Send(sendedMessages[clientId]);
                Thread.Sleep(10);
            }

            for (int i = 0; i < sendedMessages.Count; i++)
            {
                sendedMessages[i] += "\n";
            }

            AssertLists(n, choiseClient);

            for (int i = 0; i < n; i++)
            {
                string sEndPoint = String.Format("{0}:{1}", clients[(i + 1) % 2].IPAddr, clients[(i + 1) % n].Port);
                Assert.That(receivers[i].Log[0], Is.EqualTo(sEndPoint));
            }

            for (int i = 0; i < n; i++)
            {
                clients[i].Stop();
            }
        }

        [Test]
        public void TestThreeClientsConnectToMulti()
        {
            int n = 3;
            InitLists(n);

            clients[0].Connect(clients[1].IPAddr, clients[1].Port);
            clients[2].Connect(clients[0].IPAddr, clients[0].Port);

            List<int> choiseClient = new List<int> { 0, 0, 1, 1, 2, 2, 0, 1, 2, 2, 1, 0 };
            Thread.Sleep(10);
            for (int i = 0; i < choiseClient.Count; i++)
            {
                int clientId = choiseClient[i];
                clients[clientId].Send(sendedMessages[clientId]);
                Thread.Sleep(10);
            }

            for (int i = 0; i < sendedMessages.Count; i++)
            {
                sendedMessages[i] += "\n";
            }

            AssertLists(n, choiseClient);

            for (int i = 0; i < n; i++)
            {
                clients[i].Stop();
            }
        }

        [Test]
        public void TestThreeClientsConnectToSingle()
        {
            int n = 3;
            InitLists(n);

            clients[0].Connect(clients[2].IPAddr, clients[2].Port);
            Thread.Sleep(10);
            clients[0].Connect(clients[1].IPAddr, clients[1].Port);

            List<int> choiseClient = new List<int> { 0, 0, 1, 1, 2, 2, 0, 1, 2, 2, 1, 0 };
            Thread.Sleep(10);
            for (int i = 0; i < choiseClient.Count; i++)
            {
                int clientId = choiseClient[i];
                clients[clientId].Send(sendedMessages[clientId]);
                Thread.Sleep(10);
            }

            for (int i = 0; i < sendedMessages.Count; i++)
            {
                sendedMessages[i] += "\n";
            }

            AssertLists(n, choiseClient);

            for (int i = 0; i < n; i++)
            {
                clients[i].Stop();
            }
        }

        [Test]
        public void TestStop()
        {
            int n = 2;
            InitLists(n);
            clients[0].Connect(clients[1].IPAddr, clients[1].Port);
            List<int> choiseClient = new List<int> { 0, 0, 1, 1, 0, 1, 0, 1 };
            for (int i = 0; i < choiseClient.Count; i++)
            {
                int clientId = choiseClient[i];
                clients[clientId].Send(sendedMessages[clientId]);
                Thread.Sleep(10);
            }

            clients[0].Stop();
            Thread.Sleep(10);
            clients[1].Send("NotSended");
            clients[1].Send("NotSended");
            clients[1].Stop();
        }
    }
}
