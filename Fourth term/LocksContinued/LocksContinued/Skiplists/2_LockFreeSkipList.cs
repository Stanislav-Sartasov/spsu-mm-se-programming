using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LocksContinued.Skiplists
{
    public class LockFreeSkipList<T>
    {
        const int MAX_LEVEL = 42;
        readonly Node<T> head = new Node<T>(int.MinValue);
        readonly Node<T> tail = new Node<T>(int.MaxValue);
        public LockFreeSkipList()
        {
            for (int i = 0; i < head.Next.Length; i++)
            {
                head.Next[i] = new AtomicMarkableReference<Node<T>>(tail, false);
            }
        }

        private class Node<T>
        {
            public T Value;
            public int Key;
            public AtomicMarkableReference<Node<T>>[] Next;
            public int TopLevel;
            // constructor for sentinel nodes
            public Node(int key)
            {
                Value = default(T); this.Key = key;
                Next = new AtomicMarkableReference<Node<T>>[MAX_LEVEL + 1];
                for (int i = 0; i < Next.Length; i++)
                {
                    Next[i] = new AtomicMarkableReference<Node<T>>(null, false);
                }
                TopLevel = MAX_LEVEL;
            }
            // constructor for ordinary nodes
            public Node(T x, int height)
            {
                Value = x;
                Key = x.GetHashCode();
                Next = new AtomicMarkableReference<Node<T>>[height + 1];
                for (int i = 0; i < Next.Length; i++)
                {
                    Next[i] = new AtomicMarkableReference<Node<T>>(null, false);
                }
                TopLevel = height;
            }
        }

        bool Find(T x, Node<T>[] preds, Node<T>[] succs)
        {
            int key = x.GetHashCode();
            bool marked = false;
            bool snip;
            Node<T> pred = null, curr = null, succ = null;
        retry:
            while (true)
            {
                pred = head;
                for (int level = MAX_LEVEL; level >= 0; level--)
                {
                    curr = pred.Next[level].GetReference();
                    while (true)
                    {
                        succ = curr.Next[level].Get(out marked);
                        while (marked)
                        {
                            snip = pred.Next[level].CompareAndSet(curr, succ, false, false);
                            if (!snip) goto retry;
                            curr = pred.Next[level].GetReference();
                            succ = curr.Next[level].Get(out marked);
                        }
                        if (curr.Key < key)
                        {
                            pred = curr; curr = succ;
                        }
                        else
                        {
                            break;
                        }
                    }
                    preds[level] = pred;
                    succs[level] = curr;
                }
                return (curr.Key == key);
            }
        }

        public bool Contains(T x)
        {
            int v = x.GetHashCode();
            bool marked = false;
            Node<T> pred = head, curr = null, succ = null;
            for (int level = MAX_LEVEL; level >= 0; level--)
            {
                curr = pred.Next[level].GetReference();
                while (true)
                {
                    succ = curr.Next[level].Get(out marked);
                    while (marked)
                    {
                        curr = pred.Next[level].GetReference();
                        succ = curr.Next[level].Get(out marked);

                    }
                    if (curr.Key < v)
                    {
                        pred = curr;
                        curr = succ;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return (curr.Key == v);
        }

        public bool Add(T x)
        {
            int topLevel = new Random().Next(0, MAX_LEVEL);
            Node<T>[] preds = new Node<T>[MAX_LEVEL + 1];
            Node<T>[] succs = new Node<T>[MAX_LEVEL + 1];
            while (true)
            {
                bool found = Find(x, preds, succs);
                if (found)
                {
                    return false;
                }
                else
                {
                    Node<T> succ;
                    Node<T> newNode = new Node<T>(x, topLevel);
                    for (int level = 0; level <= topLevel; level++)
                    {
                        succ = succs[level];
                        newNode.Next[level].Set(succ, false);
                    }
                    Node<T> pred = preds[0];
                    succ = succs[0];
                    if (!pred.Next[0].CompareAndSet(succ, newNode, false, false))
                    {
                        continue;
                    }
                    for (int level = 1; level <= topLevel; level++)
                    {
                        while (true)
                        {
                            pred = preds[level];
                            succ = succs[level];
                            if (pred.Next[level].CompareAndSet(succ, newNode, false, false))
                                break;
                            Find(x, preds, succs);
                        }
                    }
                    return true;
                }
            }
        }

        bool Remove(T x)
        {
            Node<T>[] preds = new Node<T>[MAX_LEVEL + 1];
            Node<T>[] succs = new Node<T>[MAX_LEVEL + 1];
            Node<T> succ;
            while (true)
            {
                bool found = Find(x, preds, succs);
                if (!found)
                {
                    return false;
                }
                else
                {
                    bool marked;
                    Node<T> nodeToRemove = succs[0];
                    for (int level = nodeToRemove.TopLevel; level >= 1; level--)
                    {
                        marked = false;
                        succ = nodeToRemove.Next[level].Get(out marked);
                        while (!marked)
                        {
                            nodeToRemove.Next[level].CompareAndSet(succ, succ, false, true);
                            succ = nodeToRemove.Next[level].Get(out marked);
                        }
                    }
                    marked = false;
                    succ = nodeToRemove.Next[0].Get(out marked);
                    while (true)
                    {
                        bool iMarkedIt =
                            nodeToRemove.Next[0].CompareAndSet(succ, succ, false, true);
                        succ = nodeToRemove.Next[0].Get(out marked);
                        if (iMarkedIt)
                        {
                            Find(x, preds, succs);
                            return true;
                        }
                        else if (marked) return false;
                    }
                }
            }
        }

        
    }
}
