namespace GameTable.BetCreator
{
    public static class RandomEnumValue
    {
        public static T RandomEnumVal<T>(this Random rnd)
        {
            var value = Enum.GetValues(typeof(T));
            return (T)value.GetValue(rnd.Next(value.Length));
        }
    }
}
