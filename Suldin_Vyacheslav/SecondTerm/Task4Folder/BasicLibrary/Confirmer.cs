using System;

namespace BasicLibrary
{
    public class Confirmer
    {
		public virtual enumType GetCorectAnswer<enumType>()
			where enumType : Enum
		{
			while (true)
			{
				string answer = Console.ReadLine();

				foreach (enumType key in Enum.GetValues(typeof(enumType)))
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
