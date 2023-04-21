namespace Task3
{
    internal class DoublyLinkedNode<T>
    {
        public T Data { get; private set; }
        public DoublyLinkedNode<T>? Previous { get; set; }
        public DoublyLinkedNode<T>? Next { get; set; }

        public DoublyLinkedNode(T data)
        {
            Data = data;
            Previous = null;
            Next = null;
        }
    }
}
