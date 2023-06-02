using Sets;

namespace SetTests;

public class FineGrainedSetTests : SetTests
{
    protected override Sets.ISet<int> set { get; } = new FineGrainedSet<int>();
}