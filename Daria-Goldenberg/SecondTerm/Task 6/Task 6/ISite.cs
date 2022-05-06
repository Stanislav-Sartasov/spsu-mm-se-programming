namespace Task_6
{
	public interface ISite
	{
		string Name { get; }

		public Weather GetData();
	}
}