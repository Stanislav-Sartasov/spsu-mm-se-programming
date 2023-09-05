using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocksContinued.Locks
{
    public class TOLock : IMaybeLock
    {
        static QNode AVAILABLE = new QNode();

        volatile QNode tail = null; // was AtomicReference in Java
        ThreadLocal<QNode> myNode = new ThreadLocal<QNode>(() => null);

        public bool TryLock(long patienceInMs)
        {
            DateTime startTime = DateTime.Now;

            QNode qnode = myNode.Value = new QNode();
            qnode.Pred = null;

            QNode myPred = Interlocked.Exchange(ref tail, qnode);
            if (myPred == null || myPred.Pred == AVAILABLE)
            {
                return true;
            }
            while (DateTime.Now.Subtract(startTime).TotalMilliseconds < patienceInMs)
            {
                QNode predPred = myPred.Pred;
                // it's free
                if (predPred == AVAILABLE)
                {
                    return true;
                }
                // it's abandoned
                else if (predPred != null)
                {
                    myPred = predPred;
                }
            }
            if (Interlocked.CompareExchange(ref tail, myPred, qnode) != qnode)
                qnode.Pred = myPred;
            return false;
        }

        public void Unlock()
        {
            QNode qnode = myNode.Value;
            if (Interlocked.CompareExchange(ref tail, null, qnode) != qnode)
                qnode.Pred = AVAILABLE;
        }

        class QNode
        {
            public volatile QNode Pred = null;
        }
    }
}
