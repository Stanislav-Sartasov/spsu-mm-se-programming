using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocksContinued.Locks
{
    public class MCSLock : ILock
    {
        volatile QNode tail = null;
        ThreadLocal<QNode> myNode = new ThreadLocal<QNode>(() => new QNode());
        public void Lock()
        {
            QNode qnode = myNode.Value;
            QNode pred = Interlocked.Exchange(ref tail, qnode);
            if (pred != null)
            {
                qnode.Locked = true;
                pred.Next = qnode;
                // wait until predecessor gives up the lock
                while (qnode.Locked) { }
            }
        }
        public void Unlock()
        {
            QNode qnode = myNode.Value;
            if (qnode.Next == null)
            {
                if (Interlocked.CompareExchange(ref tail, null, qnode) == qnode)
                    return;
                // wait until successor fills in the next field
                while (qnode.Next == null) { }
            }
            qnode.Next.Locked = false;
            qnode.Next = null;
        }
        class QNode
        {
            public volatile bool Locked = false;
            public volatile QNode Next = null;
        }
    }
}
