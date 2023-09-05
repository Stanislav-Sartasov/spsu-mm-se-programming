using Sets;

namespace SetTests;

public class OptimisticSetTests : SetTests
{
    protected override Sets.ISet<int> set { get; } = new OptimisticSet<int>();
}