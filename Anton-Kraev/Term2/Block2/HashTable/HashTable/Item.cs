namespace HashTable
{
    public class Item<T> : ICloneable
    {
        private T data;
        public T Data
        {
            get => data;
            set => data = value ?? throw new ArgumentNullException(nameof(value));
        }

        public Item<T>? Next { get; set; }

        public Item(T data)
        {
            Data = data;
            Next = null;
        }

        public object Clone()
        {
            var clone = new Item<T>(data);
            if (Next != null)
                clone.Next = (Item<T>)Next.Clone();
            return clone;
        }
    }
}