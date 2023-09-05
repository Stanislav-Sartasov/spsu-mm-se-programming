using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocksContinued.Locks
{
    public class CLHLock : ILock
    {
        class QNode
        {
            public volatile bool locked = false;
            public bool Locked
            {
                get { return locked; }
                set { locked = value; }
            }
        }

        volatile QNode tail = new QNode();
        ThreadLocal<QNode> myPred = new ThreadLocal<QNode>(() => null);
        ThreadLocal<QNode> myNode = new ThreadLocal<QNode>(() => new QNode());

        public void Lock()
        {
            QNode qnode = myNode.Value;
            qnode.Locked = true;
            QNode pred = Interlocked.Exchange(ref tail, qnode);
            myPred.Value = pred;
            while (pred.Locked) { }
        }

        public void Unlock()
        {
            QNode qnode = myNode.Value;
            qnode.Locked = false;
            myNode.Value = myPred.Value;
        }
    }
}
