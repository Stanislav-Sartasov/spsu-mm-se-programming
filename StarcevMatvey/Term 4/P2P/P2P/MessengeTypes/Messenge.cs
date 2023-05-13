namespace P2P.MessengeTypes
{
    public class Messenge
    {
        public readonly string Data;
        public readonly Reshuffle Reshuffle;
        public readonly TypeOfData Type;

        public Messenge (string data, Reshuffle reshuffle, TypeOfData type)
        {
            Data = data;
            Reshuffle = reshuffle;
            Type = type;
        }

        public Messenge WithData (string data) => new Messenge(data, Reshuffle, Type);

        public Messenge WithReshuffle(Reshuffle reshuffle) => new Messenge(Data, reshuffle, Type);

        public Messenge WithType(TypeOfData type) => new Messenge(Data, Reshuffle, type);
    }
}
