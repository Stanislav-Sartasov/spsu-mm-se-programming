namespace HashTable
{
	public class Cell<TKey, TValue>
	{
		public bool Remote { get; set; } 
		public TValue Value { get; set; }
		public TKey Key { get; set; }

		public Cell (TKey key, TValue value)
		{
			Value = value;
			Key = key;
		}
	}
}
	