namespace DoublyLinkedList;

internal class Node<T>
{
    internal readonly T Data;
    internal Node<T>? Previous;
    internal Node<T>? Next;

    public Node(T value)
    {
        Data = value;
        Previous = null;
        Next = null;
    }

    public Node(Node<T> previousNode, T value)
    {
        Data = value;
        Previous = previousNode;
        Next = null;
    }
}