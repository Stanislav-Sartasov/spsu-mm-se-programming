namespace DoublyLinkedList
{
    public class DoublyNode<T>
    {
        public T Data { get; set; }

        public DoublyNode<T>? Next;
        public DoublyNode<T>? Prev;

        public DoublyNode(T data)
        {
            Data = data;
        }
    }
}

