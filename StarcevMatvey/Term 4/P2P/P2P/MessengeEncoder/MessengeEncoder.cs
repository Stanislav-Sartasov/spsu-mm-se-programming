using System.Net;
using System.Text;
using P2P.MessengeTypes;
using P2P.Net;
using P2P.Utils;

namespace P2P.MessengeEncoder
{
    public class MessengeEncoder
    {
        private const int NORMAL_MAX_MESSENGE_LENGTH = 1024; // bytes
        public readonly int MaxMessengeLength; //bytes

        public MessengeEncoder()
        {
            MaxMessengeLength = NORMAL_MAX_MESSENGE_LENGTH;
        }

        public MessengeEncoder(int maxMessengeLength)
        {
            MaxMessengeLength = maxMessengeLength <= 0 ? NORMAL_MAX_MESSENGE_LENGTH : maxMessengeLength;
        }

        public MessengeEncoder WithMessengeLength(int mexMessengeLength) => new MessengeEncoder(mexMessengeLength);

        public byte[] ToMessenge(Messenge messenge)
        {
            //   0     1  2 3 ...
            // union type messengeData

            var mes = Encoding.UTF8.GetBytes(" " + " " + messenge.Data);
            mes = mes.Length <= MaxMessengeLength ? mes : mes.Take(MaxMessengeLength).ToArray();
            mes[0] = (byte)messenge.Union;
            mes[1] = (byte)messenge.Type;

            return mes;
        }

        public Messenge FromMessenge(byte[] messenge)
        {
            var union = (Union)messenge[0];
            var type = (TypeOfData)messenge[1];
            var getted = Math.Min(messenge.Length, MaxMessengeLength) - 2;

            var data = "";
            if (getted > 0) data = Encoding.UTF8.GetString(messenge.Skip(2).ToArray(), 0, getted);

            return new Messenge(data.Trim(), union, type);
        }

        public int GetPort(Messenge messenge)
        {
            if (messenge.Type != MessengeTypes.TypeOfData.Listeners) 
                throw new Exception("Type of messenge is not a LISTENERS");

            var port = Utils.Utils.GetPositiveInt(messenge.Data);

            if (port == 0) throw new Exception("Port must be possitive");

            return port;
        }

        public List<IPEndPoint> GetConnections(Messenge mes)
        {
            if (mes.Type != MessengeTypes.TypeOfData.Listeners)
                throw new Exception("Type of messenge is not a LISTENERS");

            var strs = mes.Data.Split().ToList();
            var rez = new List<IPEndPoint>();

            strs.ForEach(x => rez.Add(IPEndPoint.Parse(x)));

            return rez;
        }
    }
}
