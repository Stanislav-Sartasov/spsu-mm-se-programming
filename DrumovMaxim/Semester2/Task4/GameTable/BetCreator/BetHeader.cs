using GameTable.BetsType;

namespace GameTable.BetCreator
{
    public class BetHeader
    {
		private IBetCreator betBuffer;

		public BetHeader(IBetCreator buffer)
		{
			betBuffer = buffer;
		}

		public Bet CreateBet(int bet)
        {
			betBuffer.cash = bet;
			return betBuffer.FormBet();
		}
	}
}
