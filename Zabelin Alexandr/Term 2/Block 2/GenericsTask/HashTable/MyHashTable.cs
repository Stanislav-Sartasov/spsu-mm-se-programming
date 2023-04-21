using System;

namespace MyHashTable
{
    public class MyHashTable<KeyType, ValueType>
        where  KeyType : IEquatable<KeyType>
    {
        private HashTableList<KeyType, ValueType>[] buckets;

        public MyHashTable(int numOfBuckets)
        {
            this.buckets = new HashTableList<KeyType, ValueType>[numOfBuckets];
        }

        public ValueType? this[KeyType key]
        {
            get => Get(key);

            set => Add(key, value);
        }

        public void Remove(KeyType key)
        {
            if (!IsNull(key))
            {
                uint ind = HashFoo(key);
                
                if (buckets[ind] != null)
                {
                    buckets[ind].Remove(key);
                }
            }
        }

        public bool Include(KeyType key)
        {
            if (IsNull(key))
            {
                Console.WriteLine("Null as key is impossible, false will be returned");

                return false;
            }
            else
            {
                uint ind = HashFoo(key);

                if (this.buckets[ind] == null)
                {
                    return false;
                }
                else
                {
                    return this.buckets[ind].Include(key);
                }
            }
        }

        public void Print()
        {
            for (int i = 0; i < buckets.Length; i++)
            {
                if (buckets[i] != null)
                {
                    buckets[i].Print();
                }
            }
        }

        private ValueType? Get(KeyType key)
        {
            if (!IsNull(key))
            {
                uint ind = HashFoo(key);

                if (buckets[ind] == null)
                {
                    Console.WriteLine($"There is not {key} element, the default value will be returned");

                    return default(ValueType);
                }
                else
                {
                    return buckets[ind].Get(key);
                }
            }
            else
            {
                Console.WriteLine("Null as key is impossible, the default value will be returned");

                return default(ValueType);
            }
        }

        private void Add(KeyType key, ValueType value)
        {
            if (IsNull(key))
            {
                Console.WriteLine("Null as key is impossible, no element will be added");
            }
            else
            {
                uint ind = HashFoo(key);

                if (this.buckets[ind] == null)
                {
                    this.buckets[ind] = new HashTableList<KeyType, ValueType>(key, value);
                }
                else
                {
                    this.buckets[ind].Add(key, value);
                }
            }
        }

        private bool IsNull(KeyType key) => key == null;

        private uint HashFoo(KeyType key) => (uint)((uint)key.GetHashCode() % this.buckets.Length);
    }
}