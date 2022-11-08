using System;

namespace Roulette
{
	class InputCheck
	{
		public static int IntCheck(int str)
		{
			while (true)
			{
				try
				{
					str = int.Parse(Console.ReadLine());
					return str;
				}
				catch
				{
					Console.WriteLine("You must enter an int variable");
				}
			}
		}

		public static int GetIntoSlot(int slot)
		{
			if (!((slot = IntCheck(slot)) > 36 || slot < 0))
				return slot;
			Console.WriteLine("Slot number not less than 0 and not more than 36");
			while (slot > 36 || slot < 0)
			{
				slot = IntCheck(slot);
			}
			return slot;
		}
	}
}
