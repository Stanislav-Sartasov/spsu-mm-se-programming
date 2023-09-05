using LocksContinued.Locks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocksContinued.Stacks
{
    public class LockFreeStack<T>
    {
        volatile Node top = null;
        const int MIN_DELAY = ...;
        const int MAX_DELAY = ...;
        Backoff backoff = new Backoff(MIN_DELAY, MAX_DELAY);


        protected class Node
        {
            public T Value;
            public volatile Node Next;
            public Node(T value)
            {
                this.Value = value;
                Next = null;
            }
        }

        public virtual void Push(T value)
        {
            Node node = new Node(value);
            while (true)
            {
                if (TryPush(node))
                {
                    return;
                }
                else
                {
                    backoff.DoBackoff();
                }
            }
        }

        protected bool TryPush(Node node)
        {
            Node oldTop = top;
            node.Next = oldTop;
            return (Interlocked.CompareExchange(ref top, node, oldTop) == oldTop);
        }

        public virtual T Pop()
        {
            while (true)
            {
                Node returnNode = TryPop();
                if (returnNode != null)
                {
                    return returnNode.Value;
                }
                else
                {
                    backoff.DoBackoff();
                }
            }
        }

        protected Node TryPop()
        {
            Node oldTop = top;
            if (oldTop == null)
            {
                throw new InvalidOperationException();
            }
            Node newTop = oldTop.Next;
            if (Interlocked.CompareExchange(ref top, newTop, oldTop) == oldTop)
            {
                return oldTop;
            }
            else
            {
                return null;
            }
        }
    }
}

