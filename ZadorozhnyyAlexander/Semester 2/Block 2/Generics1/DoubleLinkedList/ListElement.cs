namespace DoubleLinkedList
{
    public class ListElement<T>
    {
        public T Data { get; set; }
        public ListElement<T> Previous { get; set; }
        public ListElement<T> Next { get; set; }

        public ListElement(T data)
        {
            Data = data;
        }
    }
}
