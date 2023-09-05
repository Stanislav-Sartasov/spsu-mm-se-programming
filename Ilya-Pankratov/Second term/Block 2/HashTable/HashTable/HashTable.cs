namespace HashTable
{
    public class HashTable<TKey, TValue>
    {
        private int hashMax = 256;
        private const int firstHashParam = 61;
        private const int secondHashParam = 63;
        private const int thirdHashParam = 83311;
        private List<Element<TKey, TValue>>[] elements;

        public int KeyCount { get; private set; }
        public int ElementCount { get; private set; }

        public HashTable()
        {
            elements = new List<Element<TKey, TValue>>[hashMax];
        }

        public List<Element<TKey, TValue>>? this[TKey key]
        {
            get
            {
                int hashValue = HashFunction(key);

                if (elements[hashValue] != null)
                {
                    return elements[hashValue];
                }

                return null;
            }
        }

        public void Add(TKey key, TValue value)
        {
            int hashValue = HashFunction(key);
            var addElement = new Element<TKey, TValue>(key, value);

            if (elements[hashValue] == null)
            {
                KeyCount++;
                elements[hashValue] = new List<Element<TKey, TValue>>();
            }

            if (!elements[hashValue].Contains(addElement))
            {
                ElementCount++;
                elements[hashValue].Add(addElement);
                if (!IsBalanced(elements[hashValue]))
                {
                    Rebalance();
                }
            }
        }

        public void Add(Element<TKey, TValue> pair)
        {
            Add(pair.Key, pair.Value);
        }

        public void AddRange(Element<TKey, TValue>[] pairs)
        {
            foreach(var pair in pairs)
            {
                Add(pair);
            }
        }

        public void AddRange(List<Element<TKey, TValue>> pairs)
        {
            foreach (var pair in pairs)
            {
                Add(pair);
            }
        }

        public Element<TKey, TValue>? Find(TKey key, TValue value)
        {
            int hashValue = HashFunction(key);

            if (elements[hashValue] != null)
            {
                foreach (Element<TKey, TValue> element in elements[hashValue])
                {
                    if (element.Value.Equals(value))
                    {
                        return element;
                    }
                }
            }

            return null;
        }

        public Element<TKey, TValue>? Find(Element<TKey, TValue> element)
        {
            return Find(element.Key, element.Value);
        }

        public bool Contains(TKey key, TValue value)
        {

            int hashValue = HashFunction(key);

            if (elements[hashValue] != null)
            {
                foreach (Element<TKey, TValue> element in elements[hashValue])
                {
                    if (element.Value.Equals(value))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public bool Contains(Element<TKey, TValue> element)
        {
            return Contains(element.Key, element.Value);
        }

        public bool Remove(TKey key, TValue value)
        {
            int hashValue = HashFunction(key);

            if (elements[hashValue] != null)
            {
                foreach (Element<TKey, TValue> element in elements[hashValue])
                {
                    if (element.Value.Equals(value))
                    {
                        ElementCount--;
                        elements[hashValue].Remove(element);

                        if(elements[hashValue].Count == 0)
                        {
                            elements[hashValue] = null;
                            KeyCount--;
                        }

                        return true;
                    }
                }
            }

            return false;
        }

        public bool Remove(Element<TKey, TValue> element)
        {
            return Remove(element.Key, element.Value);
        }

        public void Clear()
        {
            for (int i = 0; i < elements.Length; i++)
            {
                elements[i] = null;
            }

            KeyCount = 0;
            ElementCount = 0;
        }

        public bool ContainsKey(TKey key)
        {
            int hashValue = HashFunction(key);

            return elements[hashValue] != null;
        }

        public bool ContainsValue(TValue value)
        {
            for (int i = 0; i < elements.Length; i++)
            {
                if (elements[i] == null)
                {
                    continue;
                }

                foreach (var element in elements[i])
                {
                    if (element.Value.Equals(value))
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private int HashFunction(TKey key)
        {
            return Math.Abs((secondHashParam * key.GetHashCode()) + thirdHashParam) / firstHashParam % hashMax;
        }

        private bool IsBalanced(List<Element<TKey, TValue>> list)
        {
            return list.Count < Math.Log2(hashMax);
        }

        private void Rebalance()
        {
            hashMax *= 4;
            List<Element<TKey, TValue>>[] newElements = new List<Element<TKey, TValue>>[hashMax];
            List<Element<TKey, TValue>>[] oldElements = elements;
            elements = newElements;

            for (int i = 0; i < oldElements.Length; i++)
            {
                if (oldElements[i] != null)
                {
                    foreach (var element in oldElements[i])
                    {
                        Add(element);
                    }
                }
            }
        }
    }
}