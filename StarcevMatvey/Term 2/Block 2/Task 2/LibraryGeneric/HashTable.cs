namespace LibraryGeneric
{
    public class HashTable<V>
    {
        public int Size { get; private set; }
        public int Divisior { get; private set; }
        private List<List<Element<V>>> FirstElements { get; set; }
        private bool IsRebalance 
        { 
            get
            {
                return FirstElements.Any(x => x.Count > (Size + 1) / 2);
            }
        }

        private int HashFunction(int key)
        {
            return key % Divisior;
        }

        public void Add(int key, V value)
        {
            FirstElements[HashFunction(key)].Add(new Element<V>(key, value));
            Size++;
            if (IsRebalance) Rebalance();
        }

        public void Remove(int key)
        {
            int hash = HashFunction(key);
            foreach (Element<V> element in FirstElements[hash])
            {
                if (element.Key == key)
                {
                    FirstElements[hash].Remove(element);
                    Size--;
                    return;
                }
            }
        }

        public V Get(int key)
        {
            foreach (Element<V> element in FirstElements[HashFunction(key)])
            {
                if (element.Key == key) return element.Value;
            }
            return default(V);
        }

        private void Rebalance()
        {
            Divisior *= 2;
            List<List<Element<V>>> newFirstElements = new List<List<Element<V>>>();

            // List<List<Element<V>>> newFirstElements = new List<List<Element<V>>>(Divisior);
            for (int _ = 0; _ < Divisior; _++)
            {
                newFirstElements.Add(new List<Element<V>>());
            }

            foreach (List<Element<V>> list in FirstElements)
            {
                foreach (Element<V> element in list)
                {
                    newFirstElements[HashFunction(element.Key)].Add(element);
                }
            }
            FirstElements = newFirstElements;
        }

        public HashTable(int divisior = 2)
        {
            // FirstElements = new List<List<Element<V>>>(divisior);
            FirstElements = new List<List<Element<V>>>();
            for (int _ = 0; _ < divisior; _++)
            {
                FirstElements.Add(new List<Element<V>>());
            }

            Divisior = divisior;
            Size = 0;
        }
    }
}
