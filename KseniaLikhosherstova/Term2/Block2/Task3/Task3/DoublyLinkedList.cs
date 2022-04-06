namespace Task3
{
    public class DoublyLinkedList<T>
    {
        public Node<T>? Head;
        public Node<T>? Tail;
        public int Length;

        public DoublyLinkedList()
        {
            Head = null;
            Tail = null;
            Length = 0;
        }

        public void AddToEnd(T data)
        {
            Node<T> node = new Node<T>(data);

            if (Head == null)
            {
                Head = node;
            }

            else
            {
                Tail.Next = node;
                node.Previous = Tail;
            }

            Tail = node;
            Length++;
        }


        public void AddToStart(T data)
        {
            Node<T> node = new Node<T>(data);

            if (Head == null)
            {
                Tail = node;
            }

            else
            {
                node.Next = Head;
                Head.Previous = node;
            }

            Head = node;
            Length++;
        }



        public bool RemoveByData(T data)
        {
            Node<T> currentNode = Head;

            while (currentNode != null)
            {
                if (currentNode.Data.Equals(data))
                {
                    if (currentNode.Next == null)
                    {
                        Tail = currentNode.Previous;
                    }

                    else
                    {
                        currentNode.Next.Previous = currentNode.Previous;
                    }

                    if (currentNode.Previous == null)
                    {
                        Head = currentNode.Next;
                    }

                    else
                    {
                        currentNode.Previous.Next = currentNode.Next;
                    }

                    currentNode = null;
                    Length--;
                    return true;
                }

                currentNode = currentNode.Next;
            }

            return false;
        }


        public void RemoveByIndex(int index)
        {
            if (index >= Length || index < 0)
            {
                throw new ArgumentOutOfRangeException("Index does not exist in the list.");
            }

            int currentIndex = 0;
            Node<T> currentNode = Head;
            Node<T> previousNode = null;

            while (currentIndex < index)
            {
                previousNode = currentNode;
                currentNode = currentNode.Next;
                currentIndex++;
            }

            if (Length == 0)
            {
                Head = null;
            }

            else if (previousNode == null)
            {
                Head = currentNode.Next;
                Head.Previous = null;
            }

            else if (index == Length - 1)
            {
                previousNode.Next = currentNode.Next;
                Tail = previousNode;
                currentNode = null;
            }

            else
            {
                previousNode.Next = currentNode.Next;
                currentNode.Next.Previous = previousNode;
            }

            Length--;
        }

        public int GetIndexData(T data)
        {
            int index = 0;
            Node<T> currentNode = Head;

            while (currentNode != null)
            {
                if ((currentNode.Data != null) && (currentNode.Data.Equals(data)))
                {
                    return index;
                }

                index++;
                currentNode = currentNode.Next;
            }

            return -1;
        }

        public T GetData(int index)
        {
            int counter = 0;
            Node<T> currentNode = Head;

            if (index < 0 || index >= Length)
            {
                throw new IndexOutOfRangeException("This index does not exist in this list.");
            }

            while (index != counter)
            {
                currentNode = currentNode.Next;
                counter++;
            }

            return currentNode.Data;
        }

    }
}
