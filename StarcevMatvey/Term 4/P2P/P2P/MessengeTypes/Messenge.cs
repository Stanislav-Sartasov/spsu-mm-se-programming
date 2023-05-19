namespace P2P.MessengeTypes
{
    public class Messenge
    {
        public readonly string Data;
        public readonly Union Union;
        public readonly TypeOfData Type;

        public Messenge (string data, Union union, TypeOfData type)
        {
            Data = data;
            Union = union;
            Type = type;
        }

        public Messenge WithData (string data) => new Messenge(data, Union, Type);

        public Messenge WithReshuffle(Union union) => new Messenge(Data, union, Type);

        public Messenge WithType(TypeOfData type) => new Messenge(Data, Union, type);

        public static Messenge Empty => new Messenge("empty", Union.NoUnion, TypeOfData.Empty);

        public static Messenge NoUnoion => new Messenge("nounion", Union.NoUnion, TypeOfData.Empty);

        public static Messenge YesUnion => new Messenge("union", Union.Union, TypeOfData.Empty);
    }
}
