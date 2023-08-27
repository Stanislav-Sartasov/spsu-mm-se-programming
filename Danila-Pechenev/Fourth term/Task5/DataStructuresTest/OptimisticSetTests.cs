using DataStructures;

namespace DataStructuresTest;

public class OptimisticSetTests : SetTests
{
    protected override DataStructures.ISet<int> set { get; } = new OptimisticSet<int>();
}