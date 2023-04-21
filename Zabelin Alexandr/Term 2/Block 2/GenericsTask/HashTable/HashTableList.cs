using System;

namespace MyHashTable
{
    internal class HashTableList<KeyType, ValueType>
        where KeyType : IEquatable<KeyType>
    {
        internal Node<KeyType, ValueType>? Head { get; private set; }
        
        internal HashTableList(KeyType key, ValueType value)
        {
            this.Head = new Node<KeyType, ValueType>(key, value);
        }

        internal ValueType? Get(KeyType key)
        {
            Node<KeyType, ValueType>? node = GetNode(key);

            if (node != default(Node<KeyType, ValueType>))
            {
                return node.Value;
            }
            else
            {
                Console.WriteLine($"There is not {key} element, the default value will be returned");

                return default(ValueType);
            }
        }

        internal void Add(KeyType key, ValueType value)
        {
            Node<KeyType, ValueType>? node = GetNode(key);
            Node<KeyType, ValueType> newNode;

            if (node == default(Node<KeyType, ValueType>))                  
            {
                newNode = new Node<KeyType, ValueType>(key, value, this.Head);

                this.Head = newNode;
            }
            else
            {
                node.Value = value;
            }
        }

        internal void Remove(KeyType key)
        {
            Node<KeyType, ValueType>? curNode = this.Head;

            if (curNode != null)
            {
                if (curNode.Key.Equals(key))
                {
                    this.Head = curNode.Next;
                }
                else
                {
                    while (curNode.Next != null)
                    {
                        if (curNode.Next.Key.Equals(key))
                        {
                            curNode.Next = curNode.Next.Next;

                            break;
                        }

                        curNode = curNode.Next;
                    }
                }
            }
            
        }

        internal void Print()
        {
            Node<KeyType, ValueType>? curNode = this.Head;

            while (curNode != null)
            {
                Console.WriteLine($"{curNode.Key} : {curNode.Value}");

                curNode = curNode.Next;
            }
        }

        internal bool Include(KeyType key)
        {
            Node<KeyType, ValueType>? node = GetNode(key);

            return (node != default(Node<KeyType, ValueType>));
        }

        private Node<KeyType, ValueType>? GetNode(KeyType key)
        {
            Node<KeyType, ValueType>? curNode = this.Head;

            while (curNode != null)
            {
                if (curNode.Key.Equals(key))
                { 
                    return curNode;
                }

                curNode = curNode.Next;
            }

            return default(Node<KeyType, ValueType>);
        }
    }
}
