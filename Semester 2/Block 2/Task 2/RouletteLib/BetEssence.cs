using System;
using System.Collections.Generic;

namespace RouletteLib
{
	public abstract class BetEssence
	{
		public int Coefficient { get; protected set; }
		public List<int> VictoryList { get; protected set; }
	}
}
