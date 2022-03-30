namespace Task_2
{
    public class Node<T>
    {
        public T Value { get; private set; }
        public Node<T> Previous { get; private set; }
        public Node<T> Next { get; private set; }

        public Node(T element,  Node<T> prevNode = null, Node<T> nextNode = null)
        {
            Value = element;
            Next = nextNode;
            Previous = prevNode;
        }
        
        public void SetNodes(Node<T> prevNode, Node<T> nextNode = null)
        {
            if (prevNode != null)
            {
                Previous = prevNode;
            }
            if (nextNode != null)
            {
                Next = nextNode;
            }
        }
    }
}
