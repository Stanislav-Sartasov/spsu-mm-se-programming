using System.Net;
using System.Text;

namespace Task_1.Core
{
    public static class MessageConverter
    {
        public static byte[] CreateMessageQuery(string username, string message)
        {
            string query = "Message" + ";" + username + ";" + message;
            var serializedQuery = Encoding.UTF8.GetBytes(query);
            return serializedQuery;
        }

        public static byte[] CreateConnectionNotification(string username, string newUsername)
        {
            string msg = String.Format("User {0} is connected to User {1}",
                newUsername, username);
            return CreateMessageQuery(username, msg);
        }

        public static byte[] CreateDisconnectionNotification(string username, string disconnectedUsername)
        {
            string msg = String.Format("User {0} disconnected from User {1}",
                disconnectedUsername, username);
            return CreateMessageQuery(username, msg);
        }

        public static byte[] CreateAddUserQuery(IPEndPoint newUserEndPoint, string newUsername)
        {
            string query = "AddUser" + ";" + newUserEndPoint.ToString() + ";" + newUsername;
            var serializedQuery = Encoding.UTF8.GetBytes(query);
            return serializedQuery;
        }

        public static byte[] CreateConnectUsersQuery(IEnumerable<Tuple<IPEndPoint, string>> _users)
        {
            string query = "ConnectUsers" + ";" + String.Join(";", _users);
            var serializedQuery = Encoding.UTF8.GetBytes(query);
            return serializedQuery;
        }

        public static byte[] CreateDisconnectQuery(IPEndPoint userEndPoint, string username)
        {
            string query = "Disconnect" + ";" + userEndPoint.ToString() + ";" + username;
            var serializedQuery = Encoding.UTF8.GetBytes(query);
            return serializedQuery;
        }

        public static byte[] CreateConnectQuery(IPEndPoint userEndPoint, string username)
        {
            string query = "Connect" + ";" + userEndPoint.ToString() + ";" + username;
            var serializedQuery = Encoding.UTF8.GetBytes(query);
            return serializedQuery;
        }

        public static IPEndPoint ParseEndPoint(string strAddress)
        {
            var address = strAddress.Split(':');
            var endPoint = new IPEndPoint(IPAddress.Parse(address[0]), Int32.Parse(address[1]));
            return endPoint;
        }

        public static Tuple<IPEndPoint, string> ParseUserTuple(string strTuple)
        {
            var arrTuple = strTuple.Split(',');
            var addressString = arrTuple[0].Substring(1);
            string name = arrTuple[1].Substring(1, arrTuple[1].Length - 2);
            var endPoint = ParseEndPoint(addressString);
            var userTuple = new Tuple<IPEndPoint, string>(endPoint, name);
            return userTuple;
        }
    }
}
