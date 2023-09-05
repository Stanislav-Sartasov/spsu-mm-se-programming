using System;
using System.Threading;

namespace LocksContinued.Sets
{
    public class CoarseList<T>
    {
        private Node<T> head;
        private object sync = new object();
        public CoarseList()
        {
            head = new Node<T>(int.MinValue);
            head.Next = new Node<T>(Int32.MaxValue);
        }

        public bool Add(T item)
        {
            Node<T> pred, curr;
            int key = item.GetHashCode();
            Monitor.Enter(sync);
            try
            {
                pred = head;
                curr = pred.Next;
                while (curr.Key < key)
                {
                    pred = curr;
                    curr = curr.Next;
                }
                if (key == curr.Key)
                {
                    return false;
                }
                else
                {
                    Node<T> node = new Node<T>(item);

                    node.Next = curr;
                    pred.Next = node;
                    return true;
                }
            }
            finally
            {
                Monitor.Exit(sync);
            }
        }

        public bool Remove(T item)
        {
            Node<T> pred, curr;
            int key = item.GetHashCode();
            Monitor.Enter(sync);
            try
            {
                pred = head;
                curr = pred.Next;
                while (curr.Key < key)
                {
                    pred = curr;
                    curr = curr.Next;
                }
                if (key == curr.Key)
                {
                    pred.Next = curr.Next;
                    return true;
                }
                else
                {
                    return false;
                }
            }
            finally
            {
                Monitor.Exit(sync);
            }
        }

        public bool Contains(T item)
        {
            Node<T> pred, curr;
            int key = item.GetHashCode();
            Monitor.Enter(sync);
            try
            {
                pred = head;
                curr = pred.Next;
                while (curr.Key < key)
                {
                    pred = curr;
                    curr = curr.Next;
                }
                return key == curr.Key;
                
            }
            finally
            {
                Monitor.Exit(sync);
            }
        }
    }
}
