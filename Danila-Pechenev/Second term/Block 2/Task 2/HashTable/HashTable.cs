namespace Block2Task2
{
    public class HashTable<V>
    {
        private int HashFunctionDivisor;
        private List<List<Item<V>>> tableOfElements;

        public HashTable()
        {
            HashFunctionDivisor = Constants.StartDivisor;
            tableOfElements = new List<List<Item<V>>>(Constants.StartDivisor);
        }

        public void Add(int key, V value)
        {
            int HashValue = CountHashValue(key);
            tableOfElements[HashValue].Add(new Item<V>(key, value));
            if (tableOfElements[HashValue].Count > HashFunctionDivisor * Constants.RebalancePolicyValue) Rebalance();
        }

        public void Remove(int key)
        {
            int hash = CountHashValue(key);
            for (int index = 0; index < tableOfElements[hash].Count; index++)
            {
                if (tableOfElements[hash][index].Key == key) tableOfElements[hash].RemoveAt(index);
            }
        }

        public V Get(int key)
        {
            int hash = CountHashValue(key);
            for (int index = 0; index < tableOfElements[hash].Count; index++)
            {
                if (tableOfElements[hash][index].Key == key) return tableOfElements[hash][index].Value;
            }
            throw new Exception("Wrong key");
        }

        private void Rebalance()
        {
            int lastDivisor = HashFunctionDivisor;
            HashFunctionDivisor = FindNextDivisor();
            var newTable = new List<List<Item<V>>>(HashFunctionDivisor);
            for (int lastHashValue = 0; lastHashValue < lastDivisor; lastHashValue++)
            {
                for (int index = 0; index < tableOfElements[lastHashValue].Count; index++)
                {
                    newTable[CountHashValue(tableOfElements[lastHashValue][index].Key)].Add(tableOfElements[lastHashValue][index]);
                }
            }
            tableOfElements = newTable;
        }

        private int FindNextDivisor()
        {
	        int LastDivisor = HashFunctionDivisor;
	        for (int n = LastDivisor + 2; ; n += 2)
	        {
		        bool IsPrime = true;
                double SqRoot = Math.Sqrt(n);
		        for (int d = 3; d <= SqRoot; d += 2)
		        {
			        if (n % d == 0)
			        {
                        IsPrime = false;
				        break;
			        }
                }
                if (IsPrime) return n;
	        }
        }

        private int CountHashValue(int key) => key % HashFunctionDivisor;
    }
}
