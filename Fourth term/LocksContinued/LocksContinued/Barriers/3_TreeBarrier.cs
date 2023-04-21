using LocksContinued.Interfaces;
using System;
using System.Threading;

namespace LocksContinued.Barriers
{
    public class TreeBarrier : IBarrier
    {
        int childrenLimit;
        Node[] leaves;
        int leavesCount;
        ThreadLocal<bool> threadPhase;

        public TreeBarrier(int n, int childrenLimit)
        {
            this.childrenLimit = childrenLimit;
            leavesCount = 0;
            this.leaves = new Node[n / childrenLimit];
            int depth = 0;
            threadPhase = new ThreadLocal<Boolean>(() => true);

            // compute tree depth
            while (n > 1)
            {
                depth++;
                n = n / childrenLimit;
            }
            Node root = new Node(this.childrenLimit, threadPhase);
            Build(root, depth - 1);
        }

        // recursive tree constructor
        void Build(Node parent, int depth)
        {
            if (depth == 0)
            {
                leaves[leavesCount++] = parent;
            }
            else
            {
                for (int i = 0; i < childrenLimit; i++)
                {
                    Node child = new Node(parent, childrenLimit, threadPhase);
                    Build(child, depth - 1);
                }
            }
        }

        public void Await()
        {
            // диапазон от 0 до n-1
            int me = Thread.CurrentThread.ManagedThreadId;

            Node myLeaf = leaves[me / childrenLimit];
            myLeaf.Await();
        }

        private class Node
        {
            ThreadLocal<bool> threadphase;
            int childrenLimit;

            volatile int count;

            Node parent = null;
            volatile bool phase = false;

            public Node(int childrenLimit, ThreadLocal<bool> threadphase)
            {
                this.childrenLimit = childrenLimit;
                this.threadphase = threadphase;
                this.count = childrenLimit;
            }

            public Node(Node myParent, int childrenLimit, ThreadLocal<bool> threadphase) 
                : this(childrenLimit, threadphase)
            {
                parent = myParent;
            }

            public void Await()
            {
                bool myphase = threadphase.Value;
                int position = Interlocked.Decrement(ref count);

                if (position == 0)
                { // I’m last
                    if (parent != null)
                    { // Am I root?
                        parent.Await();
                    }
                    count = childrenLimit;
                    phase = myphase;
                }
                else
                {
                    while (phase != myphase) { };
                }
                threadphase.Value = !myphase;
            }
        }
    }
}
