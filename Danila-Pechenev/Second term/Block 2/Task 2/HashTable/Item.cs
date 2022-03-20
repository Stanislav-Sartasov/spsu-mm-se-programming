namespace Block2Task2
{
    internal class Item<V>
    {
        public int Key { get; set; }
        public V Value { get; set; }
        public Item(int key, V value)
        {
            Key = key;
            Value = value;
        }
    }
}
