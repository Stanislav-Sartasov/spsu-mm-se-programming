namespace DoublyLinkedList
{
    internal class Node<T>
    {
        internal readonly T data;
        internal Node<T>? previous;
        internal Node<T>? next;
        public Node(T value)
        {
            data = value;
            previous = null;
            next = null;
        }

        public Node(Node<T> previousNode, T value)
        {
            data = value;
            previous = previousNode;
            next = null;
        }
    }
}