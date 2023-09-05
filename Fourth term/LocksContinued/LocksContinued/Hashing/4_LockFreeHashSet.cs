using System;
using System.Threading;

namespace LocksContinued.Hashing
{
    public class LockFreeHashSet<T>
    {
        const int THRESHOLD = 4;

        private BucketList<T>[] bucket;
        private int bucketSize;
        private int setSize;
        public LockFreeHashSet(int capacity)
        {
            bucket = new BucketList<T>[capacity];
            bucket[0] = new BucketList<T>();
            bucketSize = 2;
            setSize = 0;
        }

        public bool Add(T x)
        {
            int myBucket = Math.Abs(x.GetHashCode()) % bucketSize;
            BucketList<T> b = GetBucketList(myBucket);
            if (!b.Add(x))
                return false;
            int setSizeNow = Interlocked.Increment(ref setSize);
            int bucketSizeNow = bucketSize;
            if (setSizeNow / bucketSizeNow > THRESHOLD)
                Interlocked.CompareExchange(
                    ref bucketSize, bucketSizeNow, 2 * bucketSizeNow);
            return true;
        }

        private BucketList<T> GetBucketList(int myBucket)
        {
            if (bucket[myBucket] == null)
                InitializeBucket(myBucket);
            return bucket[myBucket];
        }
        private void InitializeBucket(int myBucket)
        {
            int parent = GetParent(myBucket);
            if (bucket[parent] == null)
                InitializeBucket(parent);
            BucketList<T> b = bucket[parent].GetSentinel(myBucket);
            if (b != null)
                bucket[myBucket] = b;
        }

        private int GetParent(int myBucket)
        {
            int highestBit = myBucket;

            // WTF?!
            highestBit |= (highestBit >> 1);
            highestBit |= (highestBit >> 2);
            highestBit |= (highestBit >> 4);
            highestBit |= (highestBit >> 8);
            highestBit |= (highestBit >> 16);
            highestBit =  highestBit - (highestBit >> 1);

            /** above is the same as
            int highestBit = bucketSize; 
            do
            {
                highestBit = highestBit >> 1;
            } while (highestBit > myBucket);

            but more effective
            **/

            var parent = myBucket - highestBit;

            return parent;
        }
    }
}
