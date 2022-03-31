namespace HashTable
{
    public class Element<TKey, TValue>
    {
        public TKey Key { get; set; }
        public TValue Value { get; set; }

        public Element(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
