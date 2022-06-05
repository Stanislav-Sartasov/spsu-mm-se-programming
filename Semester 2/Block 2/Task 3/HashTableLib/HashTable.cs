using System;
using System.Collections.Generic;

namespace HashTableLib
{
	public class HashTable<T>
	{
		private List<List<Link<T>>> chains;
		private int divisor = 3;
		private const int rebalanceCriterion = 2;
		private const int divisorMultiplier = 2;

		public HashTable()
		{
			chains = new List<List<Link<T>>>();

			for (int i = 0; i < divisor; i++)
			{
				chains.Add(new List<Link<T>>());
			}
		}

		public void Add(int key, T value)
		{
			int index = HashFunction(key);
			
			foreach (Link<T> link in chains[index])
			{
				if (link.Key == key)
				{
					link.Value = value;
					return;
				}
			}

			chains[index].Add(new Link<T>(key, value));
			
			if ((double)divisor / chains[index].Count < rebalanceCriterion)
			{
				Rebalance();
			}
		}

		public bool Delete(int key)
		{
			int index = HashFunction(key);

			foreach (Link<T> link in chains[index])
			{
				if (link.Key == key)
				{
					chains[index].Remove(link);
					return true;
				}
			}

			return false;
		}

		public T Get(int key)
		{
			int index = HashFunction(key);

			foreach (Link<T> link in chains[index])
			{
				if (link.Key == key)
				{
					return link.Value;
				}
			}

			return default(T);
		}

		private void Rebalance()
		{
			divisor *= divisorMultiplier;
			
			List<List<Link<T>>> newchains = new List<List<Link<T>>>();
			for (int i = 0; i < divisor; i++)
			{
				newchains.Add(new List<Link<T>>());
			}

			foreach (List<Link<T>> chain in chains)
			{
				foreach (Link<T> link in chain)
				{
					newchains[HashFunction(link.Key)].Add(new Link<T>(link.Key, link.Value));
				}
			}

			chains = newchains;
		}

		private int HashFunction(int key)
		{
			return key % divisor;
		}

		public bool IsInChain(int index, int key)
		{
			try
			{
				foreach (Link<T> link in chains[index])
				{
					if (link.Key == key)
						return true;
				}
			}
			catch (ArgumentOutOfRangeException)
			{
				return false;
			}

			return false;
		}
	}
}
