using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocksContinued.Sets
{
    public class FineGrainedSet<T>
    {
        private Node<T> head;

        public FineGrainedSet()
        {
            head = new Node<T>(int.MinValue);
            head.Next = new Node<T>(Int32.MaxValue);
        }

        public bool Add(T item)
        {
            int key = item.GetHashCode();
            head.Lock.Lock();
            Node<T> pred = head;
            try
            {
                Node<T> curr = pred.Next;
                curr.Lock.Lock();
                try
                {
                    while (curr.Key < key)
                    {
                        pred.Lock.Unlock();
                        pred = curr;
                        curr = curr.Next;
                        curr.Lock.Lock();
                    }
                    if (curr.Key == key)
                    {
                        return false;
                    }
                    Node<T> newNode = new Node<T>(item);
                    newNode.Next = curr;
                    pred.Next = newNode;
                    return true;
                }
                finally
                {
                    curr.Lock.Unlock();
                }
            }
            finally
            {
                pred.Lock.Unlock();
            }
        }

        public bool Remove(T item)
        {
            Node<T> pred = null, curr = null;
            int key = item.GetHashCode();
            head.Lock.Lock();
            try
            {
                pred = head;
                curr = pred.Next;
                curr.Lock.Lock();
                try
                {
                    while (curr.Key < key)
                    {
                        pred.Lock.Unlock();
                        pred = curr;
                        curr = curr.Next;
                        curr.Lock.Lock();
                    }
                    if (curr.Key == key)
                    {
                        pred.Next = curr.Next;
                        return true;
                    }
                    return false;
                }
                finally
                {
                    curr.Lock.Unlock();
                }
            }
            finally
            {
                pred.Lock.Unlock();
            }
        }

        public bool Contains(T item)
        {
            Node<T> pred = null, curr = null;
            int key = item.GetHashCode();
            head.Lock.Lock();
            try
            {
                pred = head;
                curr = pred.Next;
                curr.Lock.Lock();
                try
                {
                    while (curr.Key < key)
                    {
                        pred.Lock.Unlock();
                        pred = curr;
                        curr = curr.Next;
                        curr.Lock.Lock();
                    }
                    return curr.Key == key;
                }
                finally
                {
                    curr.Lock.Unlock();
                }
            }
            finally
            {
                pred.Lock.Unlock();
            }
        }
    }
}
