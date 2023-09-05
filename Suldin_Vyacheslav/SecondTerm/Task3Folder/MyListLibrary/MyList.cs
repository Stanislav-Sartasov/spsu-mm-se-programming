using System;
using System.Collections.Generic;

namespace MyListLibrary
{
    public class MyList<T>
    {
        private int lenght;
        public Node<T> Head;
        public Node<T> Tail;

        public int Length()
        {
            return this.lenght;
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
                Tail.next = newNode;
                newNode.previous = Tail;
            }

            Tail = newNode;
            lenght++;
        }
        public void AddFirst(T item)
        {
            Node<T> newNode = new Node<T>(item);
            Node<T> temporaryNode = Head;
            newNode.next = temporaryNode;
            Head = newNode;
            if (lenght == 0) Tail = Head;
            else temporaryNode.previous = newNode;
            lenght++;
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

            lenght--;

            if (deletableNode.next == null)
            {
                deletableNode.previous.next = null;
                Tail = deletableNode.previous;
                return true;
            }
            if (deletableNode.previous == null)
            {
                deletableNode.next.previous = null;
                Head = deletableNode.next;
                return true;
            }
            else
            {
                deletableNode.next.previous = deletableNode.previous;
                deletableNode.previous.next = deletableNode.next;
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
                if (currentHead.item.Equals(item)) return currentHead;
                if (currentTail.item.Equals(item)) return currentTail;

                if (turn)
                {
                    currentHead = currentHead.next;
                    turn = false;
                }
                else
                {
                    currentTail = currentTail.previous;
                    turn = true;
                }
            }
            return null;
        }
    }
}
