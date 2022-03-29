using System;
using System.Collections.Generic;

namespace MyListLibrary
{
    public class MyList<T>
    {
        private int Lenght;
        public Node<T> Head;
        public Node<T> Tail;

        public int Length()
        {
            return this.Lenght;
        }
        public void AddLast(T item)
        {
            Node<T> newNode = new Node<T>(item);

            if (Head == null)
            {
                Head = newNode;
            }
            else
            {
                Tail.Next = newNode;
                newNode.Previous = Tail;
            }

            Tail = newNode;
            Lenght++;
        }
        public void AddFirst(T item)
        {
            Node<T> newNode = new Node<T>(item);
            Node<T> temporaryNode = Head;
            newNode.Next = temporaryNode;
            Head = newNode;
            if (Lenght == 0) Tail = Head;
            else temporaryNode.Previous = newNode;
            Lenght++;
        }

        public void AddRange(T[] items)
        {
            foreach (T item in items)
            {
                this.AddFirst(item);
            }
        }
        public bool Delete(T item)
        {
            Node<T> deletableNode = this.Find(item);
            if (deletableNode == null) return false;

            Lenght--;

            if (deletableNode.Next == null)
            {
                deletableNode.Previous.Next = null;
                Tail = deletableNode.Previous;
                return true;
            }
            if (deletableNode.Previous == null)
            {
                deletableNode.Next.Previous = null;
                Head = deletableNode.Next;
                return true;
            }
            else
            {
                deletableNode.Next.Previous = deletableNode.Previous;
                deletableNode.Previous.Next = deletableNode.Next;
                return true;
            }
            
        }
        public Node<T> Find(T item)
        {
            bool turn = true;
            Node<T> currentHead = Head;
            Node<T> currentTail = Tail;
            while (currentHead != currentTail)
            {
                if (currentHead.Item.Equals(item)) return currentHead;
                if (currentTail.Item.Equals(item)) return currentTail;

                if (turn)
                {
                    currentHead = currentHead.Next;
                    turn = false;
                }
                else
                {
                    currentTail = currentTail.Previous;
                    turn = true;
                }
            }
            return null;
        }
    }
}
