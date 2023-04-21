namespace Task3
{
    public class DoublyLinkedList<T>
    {
        private DoublyNode<T>? Head { get; set; }
        private DoublyNode<T>? Tail { get; set; }
        public int Length { get; private set; }

        public DoublyLinkedList()
        {
            Head = Tail = null;
            Length = 0;
        }

        public void Add(T data)
        {
            if (Head == null)
            {
                Head = new DoublyNode<T>(data);
            }
            else if (Tail == null)
            {
                Tail = new DoublyNode<T>(data);
                Head.Next = Tail;
                Tail.Previous = Head;
            }
            else
            {
                Tail.Next = new DoublyNode<T>(data);
                Tail.Next.Previous = Tail;
                Tail = Tail.Next;
            }
            Length++;
        }

        private DoublyNode<T> FindNodeAt(int index)
        {
            if (index < 0 || index >= Length)
            {
                throw new ArgumentOutOfRangeException("The index is out of range.");
            }

            DoublyNode<T> current;

            if (2 * index < Length)
            {
                current = Head;
                for (int i = 0; i < index; i++)
                {
                    current = current.Next;
                }
            }
            else
            {
                current = Tail;
                for (int i = Length - 1; i > index; i--)
                {
                    current = current.Previous;
                }
            }

            return current;
        }

        private (DoublyNode<T>?, int) FindNodeWith(T data)
        {
            DoublyNode<T>? current = Head;
            int index = 0;
            while (current is not null)
            {
                if (current.Data.Equals(data))
                {
                    return (current, index);
                }
                current = current.Next;
                index++;
            }
            return (null, -1);
        }

        public void Add(int index, T data)
        {
            if (index < 0 || index > Length)
            {
                throw new ArgumentOutOfRangeException("The index is out of range.");
            }

            if (Length == index)
            {
                Add(data);
                return;
            }

            DoublyNode<T> current = FindNodeAt(index);
            DoublyNode<T> newNode = new DoublyNode<T>(data);

            newNode.Previous = current.Previous;
            newNode.Next = current;

            if (index == 0)
            {
                Head = newNode;
            }
            else
            {
                current.Previous.Next = newNode;
            }

            current.Previous = newNode;
            Length++;
        }

        public bool RemoveWith(T data)
        {
            (DoublyNode<T>? current, _) = FindNodeWith(data);
            return Remove(current);
        }

        public int RemoveAll(T data)
        {
            int count = 0;
            while (RemoveWith(data))
            {
                count++;
            }
            return count;
        }

        public void RemoveAt(int index)
        {
            DoublyNode<T> current = FindNodeAt(index);
            Remove(current);
        }

        private bool Remove(DoublyNode<T>? node)
        {
            if (node is not null)
            {
                if (node.Previous is not null)
                {
                    node.Previous.Next = node.Next;
                }
                if (node.Next is not null)
                {
                    node.Next.Previous = node.Previous;
                }
                if (node.Equals(Head))
                {
                    Head = node.Next;
                }
                if (node.Equals(Tail))
                {
                    Tail = node.Previous;
                }
                Length--;
                return true;
            }
            return false;
        }

        public T Find(int index)
        {
            DoublyNode<T> current = FindNodeAt(index);
            return current.Data;
        }

        public int FindIndex(T data)
        {
            (_, int index) = FindNodeWith(data);
            return index;
        }

        public void Clear()
        {
            Head = null;
            Tail = null;
            Length = 0;
        }
    }
}
