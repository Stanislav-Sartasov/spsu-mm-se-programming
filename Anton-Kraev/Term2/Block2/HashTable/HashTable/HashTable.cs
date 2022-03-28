namespace HashTable
{
    public class HashTable<T> : ICloneable
    {
        public LinkedList<T>[] Buckets { get; private set; }
        public int Count { get; private set; }

        private const int DefaultSize = 30;
        public HashTable(int size)
        {
            Buckets = new LinkedList<T>[size];
            for (int i = 0; i < size; i++)
                Buckets[i] = new LinkedList<T>();
            Count = 0;
        }

        public HashTable()
        {
            Buckets = new LinkedList<T>[DefaultSize];
            for (int i = 0; i < DefaultSize; i++)
                Buckets[i] = new LinkedList<T>();
            Count = 0;
        }

        public object Clone()
        {
            var clone = new HashTable<T>(Buckets.Length);
            clone.Count = Count;
            for (int i = 0; i < Buckets.Length; i++)
                clone.Buckets[i] = (LinkedList<T>)Buckets[i].Clone();
            return clone;
        }

        public void Add(T data)
        {
            int key = GetHash(data);
            Count += Buckets[key].Add(data) ? 1 : 0;
            Rebalance(key);
        }

        public void Delete(T data)
        {
            int key = GetHash(data);
            Count -= Buckets[key].Delete(data) ? 1 : 0;
        }

        public bool Search(T data)
        {
            int key = GetHash(data);
            return Buckets[key].Search(data);
        }

        private int GetHash(T data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));
            return Math.Abs(data.GetHashCode() % Buckets.Length);
        }

        private void Rebalance(int key)
        {
            if (Buckets[key].Count < Buckets.Length / 3 || Buckets[key].Count < Count / 3)
                return;

            var newTable = new HashTable<T>(GetNewSize());
            for (int i = 0; i < Buckets.Length; i++)
            {
                var current = Buckets[i].Head;
                for (int j = 0; j < Buckets[i].Count; j++)
                {
                    newTable.Add(current.Data);
                    current = current.Next;
                }
            }

            Buckets = newTable.Buckets;
        }

        private int GetNewSize()
        {
            int newSize = Buckets.Length * 3 + 1;
            bool isPrime = false;
            while (!isPrime)
            {
                for (int i = 2; i * i <= newSize; i++)
                {
                    if (newSize % i == 0)
                    {
                        newSize++;
                        break;
                    }
                }
                isPrime = true;
            }
            return newSize;
        }

        public void PrintTable()
        {
            for (int i = 0; i < Buckets.Length; i++)
            {
                Console.Write($"\n[{i}]:");
                foreach (var el in Buckets[i])
                    Console.Write(el + "  ");
            }
        }
    }
}