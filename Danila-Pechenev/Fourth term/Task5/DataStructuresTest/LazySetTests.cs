using DataStructures;

namespace DataStructuresTest;

public class LazySetTests : SetTests
{
    protected override DataStructures.ISet<int> set { get; } = new LazySet<int>();
}