using GameTable.BetsType;

namespace GameTable.BetCreator
{
    public interface IBetCreator
    {
        int cash { get; set; }

        Bet FormBet();
    }
}
