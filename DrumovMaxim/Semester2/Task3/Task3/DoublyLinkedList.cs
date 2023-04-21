namespace Task3
{
    public class DoublyLinkedList<T>
    {
        private DoublyLinkedNode<T>? Head { get; set; }
        private DoublyLinkedNode<T>? Bottom { get; set; }
        public int Length {get; set; }

        public DoublyLinkedList()
        {
            Head = null;
            Bottom = null;
            Length = 0;
        }

        public void Add(T value)
        {
            DoublyLinkedNode<T> node = new DoublyLinkedNode<T>(value);

            if (Head == null)
            {
                Head = node;
            }
            else if (Bottom == null)
            {
                Bottom = node;
                Head.Next = Bottom;
                Bottom.Previous = Head;
            }
            else
            {
                Bottom.Next = node;
                Bottom.Next.Previous = Bottom;
                Bottom = Bottom.Next;
            }

            Length++;
        }

        public int FindIndex(T value)
        {
            DoublyLinkedNode<T>? currentNode = Head;
            int index = 0;

            while(currentNode is not null)
            {
                if(currentNode.Data.Equals(value)) return index;

                currentNode = currentNode.Next;
                index++;
            }

            return -1;
        }

        public T? FindAtIndex(int index)
        {
            if(0 <= index && index < Length)
            {
                DoublyLinkedNode<T> currentNode;
                if(index <= Length - index)
                {
                    currentNode = Head;
                    for (int i = 0; i < index; i++)
                    {
                        currentNode = currentNode.Next;
                    }
                }
                else
                {
                    currentNode = Bottom;
                    for(int i = Length - 1; i > index; i--)
                    {
                        currentNode = currentNode.Previous;
                    }
                }
                return currentNode.Data;
            }
            else throw new ArgumentOutOfRangeException($"This index: {index} is out of range of the list.");
        }

        public bool Remove(int index)
        {
            if (0 <= index && index < Length)
            {
                DoublyLinkedNode<T> currentNode;
                if (index <= Length - index)
                {
                    currentNode = Head;
                    for (int i = 0; i < index; i++)
                    {
                        currentNode = currentNode.Next;
                    }
                }
                else
                {
                    currentNode = Bottom;
                    for (int i = Length - 1; i > index; i--)
                    {
                        currentNode = currentNode.Previous;
                    }
                }

                if (currentNode.Previous is not null)
                {
                    currentNode.Previous.Next = currentNode.Next;
                }

                if (currentNode.Next is not null)
                {
                    currentNode.Next.Previous = currentNode.Previous;
                }

                if (currentNode.Equals(Head))
                {
                    Head = currentNode.Next;
                }

                if (currentNode.Equals(Bottom))
                {
                    Bottom = currentNode.Next;
                }

                Length--;
                return true;
            }
            else throw new ArgumentOutOfRangeException($"This index: {index} is out of range of the list.");
        }

        public void Clear()
        {
            Head = null;
            Bottom = null;
            Length = 0;
        }
        
    }
}
