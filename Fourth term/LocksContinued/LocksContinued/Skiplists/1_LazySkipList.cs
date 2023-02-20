using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocksContinued.Skiplists
{
    public class LazySkipList<T>
    {
        private class Node<T>
        {
            Mutex _lock = new Mutex();
            public T item;
            public int Key;
            public Node<T>[] Next;
            public volatile bool Marked = false;
            public volatile bool FullyLinked = false;
            public int TopLevel;

            public Node(int key)
            { // sentinel node constructor
                this.item = default(T);
                this.Key = key;
                Next = new Node<T>[MAX_LEVEL + 1];
                TopLevel = MAX_LEVEL;
            }
            public Node(T x, int height)
            {
                item = x;
                Key = x.GetHashCode();
                Next = new Node<T>[height + 1];
                TopLevel = height;
            }
            public void Lock()
            {
                _lock.WaitOne();
            }
            public void Unlock()
            {
                _lock.ReleaseMutex();
            }
        }

        Random r = new Random();

        const int MAX_LEVEL = 42;
        Node<T> head = new Node<T>(int.MinValue);
        Node<T> tail = new Node<T>(int.MaxValue);
        public LazySkipList()
        {
            for (int i = 0; i < head.Next.Length; i++)
            {
                head.Next[i] = tail;
            }
        }

        public bool Contains(T x)
        {
            Node<T>[] preds = new Node<T>[MAX_LEVEL + 1];
            Node<T>[] succs = new Node<T>[MAX_LEVEL + 1];
            int lFound = Find(x, preds, succs);
            return (lFound != -1
                && succs[lFound].FullyLinked
                && !succs[lFound].Marked);
        }

        int Find(T x, Node<T>[] preds, Node<T>[] succs)
        {
            int key = x.GetHashCode();
            int lFound = -1;
            Node<T> pred = head;
            for (int level = MAX_LEVEL; level >= 0; level--)
            {
                Node<T> curr = Volatile.Read(ref pred.Next[level]);
                while (key > curr.Key)
                {
                    pred = curr; curr = Volatile.Read(ref pred.Next[level]);
                }
                if (lFound == -1 && key == curr.Key)
                {
                    lFound = level;
                }
                preds[level] = pred;
                succs[level] = curr;
            }
            return lFound;
        }

        public bool Add(T x)
        {
            int topLevel = r.Next(MAX_LEVEL); // must decrease exponentially

            Node<T>[] preds = new Node<T>[MAX_LEVEL + 1];
            Node<T>[] succs = new Node<T>[MAX_LEVEL + 1];
            while (true)
            {
                int lFound = Find(x, preds, succs);
                if (lFound != -1)
                {
                    Node<T> nodeFound = succs[lFound];
                    if (!nodeFound.Marked)
                    {
                        while (!nodeFound.FullyLinked) { }
                        return false;
                    }
                    continue;
                }
                int highestLocked = -1;
                try
                {
                    Node<T> pred, succ;
                    bool valid = true;
                    for (int level = 0; valid && (level <= topLevel); level++)
                    {
                        pred = preds[level];
                        succ = succs[level];
                        pred.Lock();
                        highestLocked = level;
                        valid = !pred.Marked && !succ.Marked && pred.Next[level] == succ;
                    }
                    if (!valid) continue;
                    Node<T> newNode = new Node<T>(x, topLevel);
                    for (int level = 0; level <= topLevel; level++)
                        newNode.Next[level] = succs[level];
                    for (int level = 0; level <= topLevel; level++)
                        preds[level].Next[level] = newNode;
                    newNode.FullyLinked = true; // successful add linearization point
                    return true;
                }
                finally
                {
                    for (int level = 0; level <= highestLocked; level++)
                        preds[level].Unlock();
                }
            }
        }

        public bool Remove(T x)
        {
            Node<T> victim = null; bool isMarked = false; int topLevel = -1;
            Node<T>[] preds = new Node<T>[MAX_LEVEL + 1];
            Node<T>[] succs = new Node<T>[MAX_LEVEL + 1];
            while (true)
            {
                int lFound = Find(x, preds, succs);
                if (lFound != -1) victim = succs[lFound];
                if (isMarked ||
                (lFound != -1 &&
                (victim.FullyLinked
                 && victim.TopLevel == lFound
                 && !victim.Marked)))
                {
                    if (!isMarked)
                    {
                        topLevel = victim.TopLevel;
                        victim.Lock();
                        if (victim.Marked)
                        {
                            victim.Unlock();
                            return false;
                        }
                        victim.Marked = true;
                        isMarked = true;
                    }
                    int highestLocked = -1;
                    try
                    {
                        Node<T> pred, succ; bool valid = true;
                        for (int level = 0; valid && (level <= topLevel); level++)
                        {
                            pred = preds[level];
                            pred.Lock();
                            highestLocked = level;
                            valid = !pred.Marked && pred.Next[level] == victim;
                        }
                        if (!valid) continue;
                        for (int level = topLevel; level >= 0; level--)
                        {
                            preds[level].Next[level] = victim.Next[level];
                        }
                        victim.Unlock();
                        return true;
                    }
                    finally
                    {
                        for (int i = 0; i <= highestLocked; i++)
                        {
                            preds[i].Unlock();
                        }
                    }
                }
                else return false;
            }
        }
    }
}
