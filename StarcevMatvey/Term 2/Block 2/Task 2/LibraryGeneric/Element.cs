namespace LibraryGeneric
{
    internal class Element<V>
    {
        internal int Key { get; private set; }
        internal V Value { get; private set; }

        internal Element(int key, V value)
        {
            Key = key;
            Value = value;
        }
    }
}
