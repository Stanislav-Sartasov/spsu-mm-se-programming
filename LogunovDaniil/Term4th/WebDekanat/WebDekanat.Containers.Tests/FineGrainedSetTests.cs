namespace WebDekanat.Containers.Tests
{
	public class FineGrainedSetTests : AExamSystemTests
	{
		protected override IExamSystem GetNewSet()
		{
			return new FineGrainedSet();
		}
	}
}
