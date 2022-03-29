namespace MyHashTable
{
    internal class Node<KeyType, ValueType>
    {
        internal KeyType? Key { get; set; }
        internal ValueType? Value { get; set; }
        internal Node<KeyType, ValueType>? Next { get; set; }

        internal Node(KeyType? key, ValueType? value, Node<KeyType, ValueType>? next = null)
        {
            this.Key = key;
            this.Value = value;
            this.Next = next;
        }
    }
}
