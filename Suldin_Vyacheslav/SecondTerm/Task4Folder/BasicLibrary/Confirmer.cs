using System;

namespace BasicLibrary
{
    public class Confirmer
    {
		public virtual EnumType GetCorectAnswer<EnumType>()
			where EnumType : Enum
		{
			while (true)
			{
				string answer = Console.ReadLine();

				foreach (EnumType key in Enum.GetValues(typeof(EnumType)))
				{
					if (String.Equals(answer, key.ToString()))
					{
						return key;
					}
				}
				Console.WriteLine("Wrong input");
			}
		}

		public virtual int GetCorectInt(int bottom, int top)
		{
			int answer;
			string input = Console.ReadLine();

			if (String.Equals(input, "Exit"))
				return -1;

			while (!int.TryParse(input, out answer) || answer > top || answer < bottom)
			{
				Console.WriteLine($"Error, enter {bottom}-{top}");
				input = Console.ReadLine();
			}
			return answer;
		}

	}
}
