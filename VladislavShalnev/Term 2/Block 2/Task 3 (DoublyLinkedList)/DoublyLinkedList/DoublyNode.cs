namespace DoublyLinkedList
{
	internal class DoublyNode<T>
	{
		internal T Data { get; set; }

		internal DoublyNode<T>? Next;
		internal DoublyNode<T>? Prev;

		internal DoublyNode(T data)
		{
			Data = data;
		}
	}
}