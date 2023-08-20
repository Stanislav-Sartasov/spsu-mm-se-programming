using System.Net;
using Task_1.Core;

namespace Task_1.Tests
{
    public class Tests
    {
        [Test]
        public void ExceptionsTest()
        {
            List<string> first = new List<string>();
            List<string> second = new List<string>();

            var firstAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1000);
            var secondAddress = new IPEndPoint(IPAddress.Parse("127.0.0.2"), 2000);

            User firstUser = new User(firstAddress, (_, m) => first.Add(m), "first");
            User secondUser = new User(secondAddress, (_, m) => second.Add(m), "second");

            try
            {
                firstUser.SendMessage("must be error");
            }
            catch (Exception e)
            {
                string actualMessage = "User first cannot send message because it isn't connected to anyone";
                Assert.AreEqual(e.Message, actualMessage);
            }

            try
            {
                firstUser.Connect(firstUser.UserEndpoint);
            }
            catch (Exception e)
            {
                string actualMessage = "You cannot connect to yourself";
                Assert.AreEqual(e.Message, actualMessage);
            }

            firstUser.Connect(secondUser.UserEndpoint);
            Thread.Sleep(100);

            try
            {
                firstUser.Connect(secondUser.UserEndpoint);
            }
            catch (Exception e)
            {
                string actualMessage = "You are already connected to user with such address";
                Assert.AreEqual(e.Message, actualMessage);
            }

            firstUser.Dispose();
            secondUser.Dispose();
        }

        [Test]
        public void TestCasualChatScenario()
        {
            List<string> first = new List<string>();
            List<string> second = new List<string>();
            List<string> third = new List<string>();

            var firstAddress = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 1000);
            var secondAddress = new IPEndPoint(IPAddress.Parse("127.0.0.2"), 2000);
            var thirdAddress = new IPEndPoint(IPAddress.Parse("127.0.0.3"), 3000);

            User firstUser = new User(firstAddress, (_, m) => first.Add(m), "first");
            User secondUser = new User(secondAddress, (_, m) => second.Add(m), "second");
            User thirdUser = new User(thirdAddress, (_, m) => third.Add(m), "third");

            firstUser.Connect(secondUser.UserEndpoint);
            Thread.Sleep(100);
            firstUser.SendMessage("msg1");
            thirdUser.Connect(secondUser.UserEndpoint);
            Thread.Sleep(100);
            thirdUser.SendMessage("msg2");
            thirdUser.Disconnect();
            Thread.Sleep(100);
            secondUser.SendMessage("msg3");

            firstUser.Dispose();
            secondUser.Dispose();
            thirdUser.Dispose();

            string[] firstMessages =
            {
                "second: User first is connected to User second",
                "first: msg1",
                "User third is connected to User first",
                "third: msg2",
                "User third disconnected from User first",
                "second: msg3"
            };

            string[] secondMessages =
            {
                "User first is connected to User second",
                "first: msg1",
                "User third is connected to User second",
                "third: msg2",
                "User third disconnected from User second",
                "second: msg3",
                "User first disconnected from User second"
            };

            string[] thirdMessages =
            {
                "second: User third is connected to User second",
                "first: User third is connected to User first",
                "third: msg2",
                "first: User third disconnected from User first",
                "second: User third disconnected from User second"
            };

            Assert.AreEqual(first.Count, firstMessages.Length);
            Assert.AreEqual(second.Count, secondMessages.Length);
            Assert.AreEqual(third.Count, thirdMessages.Length);

            for (int i = 0; i < first.Count; i++)
            {
                Assert.AreEqual(first[i], firstMessages[i]);
            }

            for (int i = 0; i < second.Count; i++)
            {
                Assert.AreEqual(second[i], secondMessages[i]);
            }

            for (int i = 0; i < third.Count; i++)
            {
                // When 3rd user is disconnected, the order of users from which it is disconnecting
                // can differ from one test start to another.
                if (i >= third.Count - 2)
                {
                    bool isOk = (third[i] == thirdMessages[third.Count - 2]) ||
                                (third[i] == thirdMessages[third.Count - 1]);
                    Assert.IsTrue(isOk);
                }
                else
                    Assert.AreEqual(third[i], thirdMessages[i]);
            }

            Assert.AreNotEqual(third[third.Count - 2], third[third.Count - 1]);
        }
    }
}