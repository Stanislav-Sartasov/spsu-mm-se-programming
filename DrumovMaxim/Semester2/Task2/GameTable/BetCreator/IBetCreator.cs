using GameTable.BetsType;

namespace GameTable.BetCreator
{
    public interface IBetCreator
    {
        int Cash { get; set; }

        Bet FormBet();
    }
}
