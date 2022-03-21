namespace DoublyLinkedList;

public class DoublyLinkedList<T>
{
    private Node<T>? first;
    private Node<T>? last;

    /// <summary>
    /// Gets quantity of items in the list.
    /// </summary>
    public int Count { get; private set; }

    public DoublyLinkedList()
    {
        first = null;
        last = null;
        Count = 0;
    }

    /// <summary>
    /// Adds a new value to the end of the list.
    /// </summary>
    /// <param name="value">New value.</param>
    public void Add(T value)
    {
        if (first == null)
        {
            first = new Node<T>(value);
        }
        else if (last == null)
        {
            last = new Node<T>(first, value);
            first.Next = last;
        }
        else
        {
            var newNode = new Node<T>(last, value);
            last.Next = newNode;
            last = newNode;
        }

        Count++;
    }

    /// <summary>
    /// Removes the first item that is equal to the passed value.
    /// </summary>
    /// <param name="value">Value that will be removed.</param>
    /// <returns>Whether the passed value was found and removed from the list.</returns>
    public bool Remove(T value)
    {
        var currentNode = first;
        int index = 0;
        while (currentNode != null)
        {
            if (currentNode.Data.Equals(value))
            {
                if (currentNode.Previous != null)
                {
                    currentNode.Previous.Next = currentNode.Next;
                }

                if (currentNode.Next != null)
                {
                    currentNode.Next.Previous = currentNode.Previous;
                }

                if (index == 0)
                {
                    first = currentNode.Next;
                }

                if (index == Count - 1)
                {
                    last = currentNode.Previous;
                }

                Count--;
                return true;
            }

            currentNode = currentNode.Next;
            index++;
        }

        return false;
    }

    /// <summary>
    /// Removes an item by index.
    /// </summary>
    /// <param name="index">Index of the item that will be removed.</param>
    /// <exception cref="ArgumentOutOfRangeException">Passed index is out of the range of the list.</exception>
    public void RemoveAt(int index)
    {
        if (!(index >= 0 && index < Count))
        {
            throw new ArgumentOutOfRangeException("Passed index is out of the range of the list.");
        }

        Node<T> currentNode;
        if (index <= Count - index)
        {
            currentNode = first;
            for (int i = 0; i < index; i++)
            {
                currentNode = currentNode.Next;
            }
        }
        else
        {
            currentNode = last;
            for (int i = Count - 1; i > index; i--)
            {
                currentNode = currentNode.Previous;
            }
        }

        if (currentNode.Previous != null)
        {
            currentNode.Previous.Next = currentNode.Next;
        }

        if (currentNode.Next != null)
        {
            currentNode.Next.Previous = currentNode.Previous;
        }

        if (index == 0)
        {
            first = currentNode.Next;
        }

        if (index == Count - 1)
        {
            last = currentNode.Previous;
        }

        Count--;
    }

    /// <summary>
    /// Finds index of the first item that is equal to the passed value.
    /// </summary>
    /// <param name="value">Value whose index will be found.</param>
    /// <returns>Index of the item if it was found or -1 if not.</returns>
    public int IndexOf(T value)
    {
        var currentNode = first;
        int index = 0;
        while (currentNode != null)
        {
            if (currentNode.Data.Equals(value))
            {
                return index;
            }

            currentNode = currentNode.Next;
        }

        return -1;
    }

    /// <summary>
    /// Finds value by the passed index.
    /// </summary>
    /// <param name="index">Index of the value that will be found.</param>
    /// <returns>Found value.</returns>
    /// <exception cref="ArgumentOutOfRangeException">Passed index is out of the range of the list.</exception>
    public T Get(int index)
    {
        if (!(index >= 0 && index < Count))
        {
            throw new ArgumentOutOfRangeException("Passed index is out of the range of the list.");
        }

        Node<T> currentNode;
        if (index <= Count - index)
        {
            currentNode = first;
            for (int i = 0; i < index; i++)
            {
                currentNode = currentNode.Next;
            }
        }
        else
        {
            currentNode = last;
            for (int i = Count - 1; i > index; i--)
            {
                currentNode = currentNode.Previous;
            }
        }

        return currentNode.Data;
    }
}