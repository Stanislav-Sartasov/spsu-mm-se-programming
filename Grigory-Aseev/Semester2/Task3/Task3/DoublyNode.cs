namespace Task3
{
    internal class DoublyNode<T>
    {
        public T Data { get; private set; }
        public DoublyNode<T>? Previous { get; set; }
        public DoublyNode<T>? Next { get; set; }

        public DoublyNode(T data)
        {
            Data = data;
            Previous = null;
            Next = null;
        }
    }
}
