namespace Block2Task2
{
    public class HashTable<V>
    {
        private int HashFunctionDivisor;
        private List<List<Item<V>>> tableOfElements;

        public HashTable()
        {
            this.HashFunctionDivisor = Constants.StartDivisor;
            this.tableOfElements = new List<List<Item<V>>>(Constants.StartDivisor);
        }

        public void Add(int key, V value)
        {
            int HashValue = CountHashValue(key);
            this.tableOfElements[HashValue].Add(new Item<V>(key, value));
            if (this.tableOfElements[HashValue].Count > this.HashFunctionDivisor * Constants.RebalancePolicyValue) Rebalance();
        }

        public void Remove(int key)
        {
            int hash = CountHashValue(key);
            for (int index = 0; index < this.tableOfElements[hash].Count; index++)
            {
                if (this.tableOfElements[hash][index].Key == key) this.tableOfElements[hash].RemoveAt(index);
            }
        }

        public V Get(int key)
        {
            int hash = CountHashValue(key);
            for (int index = 0; index < this.tableOfElements[hash].Count; index++)
            {
                if (this.tableOfElements[hash][index].Key == key) return this.tableOfElements[hash][index].Value;
            }
            throw new Exception("Wrong key");
        }

        private void Rebalance()
        {
            int lastDivisor = this.HashFunctionDivisor;
            this.HashFunctionDivisor = FindNextDivisor();
            var newTable = new List<List<Item<V>>>(this.HashFunctionDivisor);
            for (int lastHashValue = 0; lastHashValue < lastDivisor; lastHashValue++)
            {
                for (int index = 0; index < this.tableOfElements[lastHashValue].Count; index++)
                {
                    newTable[CountHashValue(this.tableOfElements[lastHashValue][index].Key)].Add(this.tableOfElements[lastHashValue][index]);
                }
            }
            this.tableOfElements = newTable;
        }

        private int FindNextDivisor()
        {
	        int LastDivisor = this.HashFunctionDivisor;
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

        private int CountHashValue(int key) => key % this.HashFunctionDivisor;
    }
}
