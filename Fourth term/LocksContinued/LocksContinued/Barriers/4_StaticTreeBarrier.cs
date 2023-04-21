using LocksContinued.Interfaces;
using LocksContinued.Locks;
using System;
using System.Threading;

namespace LocksContinued.Barriers
{
    public class StaticTreeBarrier : IBarrier
    {
        int childrenLimit;
        bool globalPhase;
        Node[] node;
        ThreadLocal<bool> threadPhase;
        int nodes;

        public StaticTreeBarrier(int size, int childrenLimit)
        {
            this.childrenLimit = childrenLimit;
            nodes = 0;
            node = new Node[size];
            int depth = 0;
            while (size > 1)
            {
                depth++;
                size = size / childrenLimit;
            }
            Build(null, depth);
            globalPhase = false;
            threadPhase = new ThreadLocal<Boolean>(() => !globalPhase);

        }
        // recursive tree constructor
        void Build(Node parent, int depth)
        {
            if (depth == 0)
            {
                node[nodes++] = new Node(parent, 0, this);
            }
            else
            {
                Node myNode = new Node(parent, childrenLimit, this);
                node[nodes++] = myNode;
                for (int i = 0; i < childrenLimit; i++)
                {
                    Build(myNode, depth - 1);
                }
            }
        }

        public void Await()
        {
            node[ThreadID.Get()].Await();
        }

        private class Node
        {
            StaticTreeBarrier root;
            private int childrenLimit;
            private volatile int childCount;
            private Node parent;

            public Node(Node myParent, int count, StaticTreeBarrier root)
            {
                this.root = root;
                childrenLimit = count;
                childCount = count;
                parent = myParent;
            }

            public void Await()
            {
                bool myphase = root.threadPhase.Value;
                while (childCount > 0) { };
                childCount = childrenLimit;
                if (parent != null)
                {
                    parent.ChildDone();
                    while (root.globalPhase != myphase) { };
                }
                else
                {
                    root.globalPhase = !root.globalPhase;
                }
                root.threadPhase.Value = !myphase;
            }

            public void ChildDone()
            {
                Interlocked.Decrement(ref childCount);
            }
        }
    }
}
