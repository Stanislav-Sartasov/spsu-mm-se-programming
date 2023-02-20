using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocksContinued.Locks
{
 public class CompositeLock : IMaybeLock
    {
 const int SIZE = 100500;
const int MIN_BACKOFF = ...;
const int MAX_BACKOFF = ...;
AtomicStampedReference<QNode> tail;
 QNode[] waiting= new QNode[SIZE];
 Random random = new Random();
        ThreadLocal<QNode> myNode = new ThreadLocal<QNode>(() => null);
public CompositeLock()
{
    12 tail = new AtomicStampedReference<QNode>(null, 0);
    14 for (int i = 0; i < waiting.length; i++)
    {
        15 waiting[i] = new QNode();
        16 }
    17 random = new Random();
    18 }

    1 public boolean tryLock(long time, TimeUnit unit)
2 throws InterruptedException
    {
3 long patience = TimeUnit.MILLISECONDS.convert(time, unit);
4 long startTime = System.currentTimeMillis();
5 Backoff backoff = new Backoff(MIN_BACKOFF, MAX_BACKOFF);
6 try {
7 QNode node = acquireQNode(backoff, startTime, patience);
8 QNode pred = spliceQNode(node, startTime, patience);
9 waitForPredecessor(pred, node, startTime, patience);
10 return true;
11 } catch (TimeoutException e) {
12 return false;
13 }
14 }

    1 private QNode acquireQNode(Backoff backoff, long startTime,
2 long patience)
3 throws TimeoutException, InterruptedException {
4 QNode node = waiting[random.nextInt(SIZE)];
5 QNode currTail;
6 while (true) {
7 if (node.state.compareAndSet(State.FREE, State.WAITING)) {
8 return node;
9 }
10 currTail = tail.get(currStamp);
11 State state = node.state.get();
12 if (state == State.ABORTED || state == State.RELEASED) {
13 if (node == currTail) {
14 QNode myPred = null;
15 if (state == State.ABORTED) {
16 myPred = node.pred;
17 }
18 if (tail.compareAndSet(currTail, myPred,
19 currStamp[0], currStamp[0]+1)) {
20 node.state.set(State.WAITING);
21 return node;
22 }
23 }
24 }
25 backoff.backoff();
26 if (timeout(patience, startTime)) {
27 throw new TimeoutException();
28 }
29 }
30 }

1 private QNode spliceQNode(QNode node, long startTime, long patience)
2 throws TimeoutException
{
3 QNode currTail;
4 do {
        5 currTail = tail.get(currStamp);
        6 if (timeout(startTime, patience))
        {
            7 node.state.set(State.FREE);
            8 throw new TimeoutException();
            9 }
        10 } while (!tail.compareAndSet(currTail, node,
11 currStamp [0], currStamp [0]+1));
12 return currTail;
13 }

1 private void waitForPredecessor(QNode pred, QNode node, long startTime,
2 long patience)
3 throws TimeoutException
{
4 int[]
    stamp = { 0};
5 if (pred == null) {
        6 myNode.set(node);
        7 return;
        8 }
9 State predState = pred.state.get();
    10 while (predState != State.RELEASED)
    {
        11 if (predState == State.ABORTED)
        {
            12 QNode temp = pred;
            13 pred = pred.pred;
            14 temp.state.set(State.FREE);
            15 }
        16 if (timeout(patience, startTime))
        {
            17 node.pred = pred;
            18 node.state.set(State.ABORTED);
            19 throw new TimeoutException();
            20 }
        21 predState = pred.state.get();
        22 }
    23 pred.state.set(State.FREE);
    24 myNode.set(node);
    25 return;
    26 }

19 public void unlock()
{
    20 QNode acqNode = myNode.get();
    21 acqNode.state.set(State.RELEASED);
    22 myNode.set(null);
    23 }
1 enum State { FREE, WAITING, RELEASED, ABORTED };
2 class QNode
    {
3 AtomicReference<State> state;
4 QNode pred;
5 public QNode()
        {
            6 state = new AtomicReference<State>(State.FREE);
            7 }
8 }
25 }
}
