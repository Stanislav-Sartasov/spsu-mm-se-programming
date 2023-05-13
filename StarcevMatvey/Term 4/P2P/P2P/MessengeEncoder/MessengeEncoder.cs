using System.Text;
using P2P.MessengeTypes;

namespace P2P.MessengeEncoder
{
    public class MessengeEncoder
    {
        private const int NORMAL_MAX_MESSENGE_LENGTH = 1024; // bytes
        private readonly int _maxMessengeLength; //bytes

        public MessengeEncoder()
        {
            _maxMessengeLength = NORMAL_MAX_MESSENGE_LENGTH;
        }

        public MessengeEncoder(int maxMessengeLength)
        {
            _maxMessengeLength = maxMessengeLength <= 0 ? NORMAL_MAX_MESSENGE_LENGTH : maxMessengeLength;
        }

        public MessengeEncoder WithMessengeLength(int mexMessengeLength) => new MessengeEncoder(mexMessengeLength);

        public byte[] ToMessenge(string messenge, Reshuffle reshuffle, TypeOfData type)
        {
            //    0     1  2 3 ...
            // ReFlag type messengeData

            var mes = Encoding.UTF8.GetBytes(" " + " " + messenge);
            mes = mes.Length <= _maxMessengeLength + 2 ? mes : mes.Take(_maxMessengeLength + 2).ToArray();
            mes[0] = (byte)reshuffle;
            mes[1] = (byte)type;

            return mes;
        }

        public (string, Reshuffle, TypeOfData) FromMessenge(byte[] messenge)
        {
            var rFlag = (Reshuffle)messenge[0];
            var type = (TypeOfData)messenge[1];
            var getted = Math.Min(messenge.Length, _maxMessengeLength + 2);

            var mes = Encoding.UTF8.GetString(messenge, 2, getted);

            return (mes, rFlag, type);
        }
    }
}
