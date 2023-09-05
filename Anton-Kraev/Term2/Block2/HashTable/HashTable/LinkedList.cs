using System.Collections;

namespace HashTable
{
    public class LinkedList<T> : ICloneable, IEnumerable<T>
    {
        public Item<T>? Head { get; private set; }
        public int Count { get; private set; }

        public LinkedList()
        {
            Head = null;
            Count = 0;
        }

        public IEnumerator<T> GetEnumerator()
        {
            var current = Head;
            while (current != null)
            {
                yield return current.Data;
                current = current.Next;
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        public object Clone()
        {
            var clone = new LinkedList<T>();
            clone.Count = Count;
            if (Head != null)
                clone.Head = (Item<T>)Head.Clone();
            return clone;
        }

        public bool Add(T data)
        {
            if (Head != null)
            {
                var current = Head;

                while (current.Next != null)
                {
                    if (current.Data!.Equals(data))
                        return false;
                    current = current.Next;
                }

                if (!current.Data!.Equals(data))
                {
                    current.Next = new Item<T>(data);
                    Count++;
                    return true;
                }

                return false;
            }
            SetFirstItem(data);
            return true;
        }

        public bool Delete(T data)
        {
            if (Head != null)
            {
                if (Head.Data!.Equals(data))
                {
                    Head = Head.Next;
                    Count--;
                    return true;
                }

                var previous = Head;
                var current = Head.Next;

                while (current != null)
                {
                    if (current.Data!.Equals(data))
                    {
                        previous!.Next = current.Next;
                        Count--;
                        return true;
                    }
                    previous = previous!.Next;
                    current = current.Next;
                }
            }
            return false;
        }

        public bool Search(T data)
        {
            if (Head != null)
            {
                var current = Head;

                while (current != null)
                {
                    if (current.Data!.Equals(data))
                        return true;
                    current = current.Next;
                }
            }
            return false;
        }

        private void SetFirstItem(T data)
        {
            var item = new Item<T>(data);
            Head = item;
            Count = 1;
        }
    }
}