namespace Task3
{
    public class Node<T>
    {
        public T Data { get; set; }
        public Node<T>? Next { get; set; }
        public Node<T>? Previous { get; set; }

        public Node(T data)
        {
            Data = data;
        }
    }
}