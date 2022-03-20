namespace DoublyLinkedList
{
    public class DoublyLinkedList<T>
    {
        Node<T>? first;
        Node<T>? last;
        public int Count { get; private set; }
        public DoublyLinkedList()
        {
            first = null;
            last = null;
            Count = 0;
        }

        public void Add(T value)
        {
            if (first == null) first = new Node<T>(value);
            else if (last == null)
            {
                last = new Node<T>(first, value);
                first.next = last;
            }
            else
            {
                var newNode = new Node<T>(last, value);
                last.next = newNode;
            }
            Count++;
        }

        public bool Remove(T value)
        {
            var currentNode = first;
            int index = 0;
            while (currentNode != null)
            {
                if (currentNode.data.Equals(value))
                {
                    if (currentNode.previous != null) currentNode.previous.next = currentNode.next;
                    if (currentNode.next != null) currentNode.next.previous = currentNode.previous;
                    if (index == 0) first = currentNode.next;
                    if (index == Count - 1) last = currentNode.previous;
                    Count--;
                    return true;
                }
                currentNode = currentNode.next;
                index++;
            }
            return false;
        }

        public void RemoveAt(int index)
        {
            if (!(0 <= index && index < Count))
            {
                throw new ArgumentOutOfRangeException("Passed argument was out of the range of the list.");
            }
            Node<T> currentNode;
            if (index <= Count - index)
            {
                currentNode = first;
                for (int i = 0; i < index; i++) currentNode = currentNode.next;
            }
            else
            {
                currentNode = last;
                for (int i = Count - 1; i > index; i--) currentNode = currentNode.previous;
            }
            if (currentNode.previous != null) currentNode.previous.next = currentNode.next;
            if (currentNode.next != null) currentNode.next.previous = currentNode.previous;
            if (index == 0) first = currentNode.next;
            if (index == Count - 1) last = currentNode.previous;
            Count--;
        }

        public int IndexOf(T value)
        {
            var currentNode = first;
            int index = 0;
            while (currentNode != null)
            {
                if (currentNode.data.Equals(value)) return index;
                currentNode = currentNode.next;
            }
            return -1;
        }

        public T Get(int index)
        {
            if (!(0 <= index && index < Count))
            {
                throw new ArgumentOutOfRangeException("Passed argument was out of the range of the list.");
            }
            Node<T> currentNode;
            if (index <= Count - index)
            {
                currentNode = first;
                for (int i = 0; i < index; i++) currentNode = currentNode.next;
            }
            else
            {
                currentNode = last;
                for (int i = Count - 1; i > index; i--) currentNode = currentNode.previous;
            }
            return currentNode.data;
        }
    }
}
