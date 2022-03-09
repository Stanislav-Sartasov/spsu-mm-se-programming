
namespace Utils.Parser
{
	public class Parser
	{
		private readonly byte[] data;

		public Parser(byte[] data)
		{
			this.data = data;
		}

		// Parses value from slice of byte array
		public int ParseInt(int position) => BitConverter.ToInt32(data.Skip(position).Take(sizeof(int)).ToArray());

		public short ParseShort(int position) => BitConverter.ToInt16(data.Skip(position).Take(sizeof(short)).ToArray());
	}
}
