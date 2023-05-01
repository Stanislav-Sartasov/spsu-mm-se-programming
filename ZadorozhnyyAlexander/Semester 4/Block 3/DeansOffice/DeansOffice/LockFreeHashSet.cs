namespace DeansOffice
{

    public class LockFreeHashSet<T>
    {
        private int treshold;

        private BucketList<T>[] bucket;
        private int bucketSize;
        private int setSize;

        private int count;

        public LockFreeHashSet(int capacity, int treshold)
        {
            this.treshold = treshold;
            bucket = new BucketList<T>[capacity];
            bucket[0] = new BucketList<T>();
            bucketSize = 2;
            setSize = 0;
        }

        public int Count() => count;

        public bool Add(T x)
        {
            int myBucket = Math.Abs(x.GetHashCode()) % bucketSize;
            BucketList<T> b = GetBucketList(myBucket);
            if (!b.Add(x))
                return false;
            Interlocked.Increment(ref count);
            int setSizeNow = Interlocked.Increment(ref setSize);
            int bucketSizeNow = bucketSize;
            if (setSizeNow / bucketSizeNow > treshold)
                Interlocked.CompareExchange(
                    ref bucketSize, 2 * bucketSizeNow, bucketSizeNow);
            return true;
        }

        public bool Remove(T x)
        {
            int myBucket = Math.Abs(x.GetHashCode()) % bucketSize;
            BucketList<T> b = GetBucketList(myBucket);
            if (!b.Remove(x))
                return false;
            Interlocked.Decrement(ref count);
            Interlocked.Decrement(ref setSize);

            return true;
        }

        public bool Contains(T x)
        {
            int myBucket = Math.Abs(x.GetHashCode()) % bucketSize;
            BucketList<T> b = GetBucketList(myBucket);
            return b.Contains(x);
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
            highestBit = highestBit - (highestBit >> 1);

            var parent = myBucket - highestBit;

            return parent;
        }
    }
}
