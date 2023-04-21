namespace DoubleLinkedList
{
    // "Александр".Length % 3 + 1 = 9 % 3 + 1 = 1 
    public class LinkedList<T>
        where T : IComparable
    {
        public int Length { get; private set; } = 0;
        public ListElement<T> Head { get; private set; }
        public ListElement<T> Tail { get; private set; }

        public void Add(T data)
        {
            var curr = new ListElement<T>(data);
            var copyHead = Head;
            if (Length == 0)
            {
                Head = curr;
                Tail = curr;
            }
            else
            {
                while (copyHead.Next != null && copyHead.Next.Data.CompareTo(data) < 0)
                {
                    copyHead = copyHead.Next;
                }

                if (copyHead.Next == null)
                {
                    Tail.Next = curr;
                    curr.Previous = Tail;
                    Tail = curr;
                }
                else
                {
                    if (copyHead.Data.CompareTo(data) > 0)
                    {
                        Head.Previous = curr;
                        curr.Next = Head;
                        Head = curr;
                    }
                    else
                    {
                        curr.Previous = copyHead;
                        curr.Next = copyHead.Next;
                        copyHead.Next = curr;
                        if (copyHead.Next.Next == Tail)
                            Tail.Previous = copyHead.Next;
                    }
                }
            }
            Length++;
        }

        public void Remove(T data)
        {
            if (Length == 0)
                return;
            var curr = Head;
            while (curr != null && curr.Data.CompareTo(data) < 0)
            {
                curr = curr.Next;
            }

            if (curr != null && curr.Data.Equals(data))
            {
                if (curr.Next != null)
                    curr.Next.Previous = curr.Previous;
                else
                    Tail = curr.Previous;

                if (curr.Previous != null)
                    curr.Previous.Next = curr.Next;
                else
                    Head = Head.Next;

                Length--;
            }
        }

        public bool Find(T data)
        {
            var curr = Head;

            while (curr != null && curr.Data.CompareTo(data) < 0)
                curr = curr.Next;

            return curr != null ? curr.Data.Equals(data) : false;
        }

        public void Clear()
        {
            Head = null;
            Tail = null;
            Length = 0;
        }

        public T[] GetAllElements()
        {
            var resultList = new T[Length];
            var curr = Head;
            for (int i = 0; i < resultList.Length; i++)
            {
                resultList[i] = curr.Data;
                curr = curr.Next;
            }

            return resultList;
        }
    }
}
