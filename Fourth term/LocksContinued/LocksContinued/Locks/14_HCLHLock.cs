using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocksContinued.Locks
{
    public class HCLHLock : ILock
    {
        const int MAX_CLUSTERS = 100500;
        volatile List<QNode> localQueues = new List<QNode>(MAX_CLUSTERS);
        volatile QNode globalQueue = new QNode();
        ThreadLocal<QNode> currNode = new ThreadLocal<QNode>(() => new QNode());
        ThreadLocal<QNode> predNode = new ThreadLocal<QNode>(() => new QNode());

        public void Lock() {
            QNode myNode = currNode.Value;
            QNode localQueue = localQueues[ThreadID.GetCluster()];
            // splice my QNode into local queue
            QNode myPred = null;
            do {
                myPred = localQueue;
            } while (Interlocked.CompareExchange(ref localQueue, myPred, myNode) != myNode);

            if (myPred != null) {
                bool iOwnLock = myPred.WaitForGrantOrClusterMaster();
                if (iOwnLock) {
                    predNode.Value = myPred;
                    return;
                    }
                }
            // I am the cluster master: splice local queue into global queue.
            QNode localTail = null;
            do {
                myPred = globalQueue;
                localTail = localQueue;
            } while (Interlocked.CompareExchange(ref globalQueue, myPred, localTail) != localTail);
            // inform successor it is the new master
            localTail.TailWhenSpliced = true;
            while (myPred.SuccessorMustWait) { };
            predNode.Value = myPred;
            return;
        }

        public void Unlock()
        {
            QNode myNode = currNode;
            myNode.SuccessorMustWait = false;
            QNode node = predNode.Value;
            node.NodeUnlock();
            currNode.Value = node;
        }

        class QNode
        {
            // private boolean tailWhenSpliced;
            const int TWS_MASK = 0x40000000;
            // private boolean successorMustWait = false;
            const int SMW_MASK = 0x20000000;
            // private int clusterID;
            const int CLUSTER_MASK = 0x1FFFFFFF;
            volatile int state = 0;

            public bool TailWhenSpliced
            {
                get { return (state & TWS_MASK) == 1; }
                set { Interlocked.Exchange(ref state, value? state | TWS_MASK : state &~TWS_MASK); }
            }


            public bool SuccessorMustWait
            {
                get { return (state & SMW_MASK) == 1; }
                set { Interlocked.Exchange(ref state, value ? state | SMW_MASK : state & ~SMW_MASK); }
            }

            public void NodeUnlock()
            {
                int oldState = 0;
                int newState = (int)ThreadID.GetCluster();
                // successorMustWait = true;
                newState |= SMW_MASK;
                // tailWhenSpliced = false;
                newState &= (~TWS_MASK);
                do
                {
                    oldState = state;
                } while (Interlocked.CompareExchange(ref state, oldState, newState) != oldState);
            }

            public int GetClusterID()
            {
                return state & CLUSTER_MASK;
            }
            // other getters and setters omitted.
        }
    }
}
