using Roulette.Common;
using Roulette.Common.Bet;
using System.Reflection;

namespace Roulette.BotLoader
{
	public class BotWrapper : IPlayer
	{
		private Type _botType;
		private object _instance;
		public readonly string Name;
		public readonly string Description;

		public int Money
		{
			get
			{
				return (int)_botType.GetProperty("Money").GetValue(_instance);
			}
		}

		public BotWrapper(Type botType, int startingMoney)
		{
			_botType = botType;
			_instance = _botType.GetConstructors()[0].Invoke(new object[] { startingMoney });

			Description = botType.GetField("Description").GetValue(_instance).ToString();
			Name = botType.GetField("Name").GetValue(_instance).ToString();
		}

		public void GiveMoney(int amount)
		{
			_botType.InvokeMember("GiveMoney", BindingFlags.InvokeMethod, null, _instance, new object[] { amount });
		}

		public List<Bet> MakeBets()
		{
			return (List<Bet>)_botType.InvokeMember("MakeBets", BindingFlags.InvokeMethod, null, _instance, new object[] { });
		}

		public int TakeMoney(int requestedAmount)
		{
			return (int)_botType.InvokeMember("TakeMoney", BindingFlags.InvokeMethod, null, _instance, new object[] { requestedAmount });
		}
	}
}
