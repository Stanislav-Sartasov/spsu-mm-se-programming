namespace DoublyLinkedListLibrary
{
	internal class Node<T>
	{
		internal T Data;
		internal Node<T>? Next;
		internal Node<T>? Previous;

		internal Node(T data)
		{
			Data = data;
			Next = null;
			Previous = null;
		}
	}
}
