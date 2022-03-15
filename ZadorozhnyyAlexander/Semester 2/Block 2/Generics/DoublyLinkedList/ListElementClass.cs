namespace DoublyLinkedList
{
    public class ListElementClass<T>
    {
        public T Data { get; set; }
        public ListElementClass<T> Previous { get; set; }
        public ListElementClass<T> Next { get; set; }

        public ListElementClass(T data)
        {
            Data = data;
        }
    }
}
