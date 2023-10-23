namespace WebDekanat.Containers.Tests
{
	public class LazySetTests : AExamSystemTests
	{
		protected override IExamSystem GetNewSet()
		{
			return new LazySet();
		}
	}
}
