using NUnit.Framework;
using System.Net;

namespace ChatParticipants.UnitTests
{
    public class ValidatorTests
    {
        [Test]
        public void EmptyUserNameTest()
        {
            string userName = "";
            bool success = Validator.ValidateUserName(userName, out _);
            Assert.AreEqual(false, success);
        }

        [Test]
        public void LongUserNameTest()
        {
            string userName = "***********************************************************************************************";
            bool success = Validator.ValidateUserName(userName, out string _);
            Assert.AreEqual(false, success);
        }

        [Test]
        public void ValidateIPTest()
        {
            string ip = "0127.255.01.001";
            bool success = Validator.ValidateIP(ip, out string _, out IPAddress _);
            Assert.AreEqual(true, success);
        }

        [Test]
        public void PortSmallValueTest()
        {
            string port = "1";
            bool success = Validator.ValidatePort(port, out string _, out int intPort);
            Assert.AreEqual(false, success);
            Assert.AreEqual(1, intPort);
        }

        [Test]
        public void PortBigValueTest()
        {
            string port = "100000";
            bool success = Validator.ValidatePort(port, out string _, out int intPort);
            Assert.AreEqual(false, success);
            Assert.AreEqual(100000, intPort);
        }

        [Test]
        public void PortCorrectValueTest()
        {
            string port = "3000";
            bool success = Validator.ValidatePort(port, out string _, out int intPort);
            Assert.AreEqual(true, success);
            Assert.AreEqual(3000, intPort);
        }

        [Test]
        public void EmptyMessageTest()
        {
            string message = "";
            bool success = Validator.ValidateMessage(message, out string _);
            Assert.AreEqual(false, success);
        }
    }
}
