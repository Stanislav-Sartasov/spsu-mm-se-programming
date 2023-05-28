namespace Test.SetTests

open ConcurrentSet

type LazySetTest() =
    inherit SetTest()
    override this.Set = LazySet<int>()
