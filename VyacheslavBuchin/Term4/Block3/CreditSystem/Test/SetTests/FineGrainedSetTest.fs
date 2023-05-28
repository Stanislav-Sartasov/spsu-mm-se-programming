namespace Test.SetTests

open ConcurrentSet

type FineGrainedSetTest() =
    inherit SetTest()
    override this.Set = FineGrainedSet<int>()
