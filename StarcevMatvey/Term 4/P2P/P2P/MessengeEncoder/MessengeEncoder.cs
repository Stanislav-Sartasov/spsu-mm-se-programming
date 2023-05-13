using System.Text;
using P2P.MessengeTypes;

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
            mes = mes.Length <= MaxMessengeLength + 2 ? mes : mes.Take(MaxMessengeLength + 2).ToArray();
            mes[0] = (byte)messenge.Union;
            mes[1] = (byte)messenge.Type;

            return mes;
        }

        public Messenge FromMessenge(byte[] messenge)
        {
            var union = (Union)messenge[0];
            var type = (TypeOfData)messenge[1];
            var getted = Math.Min(messenge.Length, MaxMessengeLength + 2);

            var data = Encoding.UTF8.GetString(messenge, 2, getted);

            return new Messenge(data, union, type);
        }
    }
}
