using GameTable.BetsType;

namespace GameTable.BotStructure
{
    public interface IBot
    {
        string Name { get; set; }
        int Balance { get; set; }
        int MinBet { get; set; }
        int LastBet { get; set; }
        int Gain { get; set; }
        int GameCounter { get; set; }
        int WinsCounter { get; set; }
        bool LastGame { get; set; }

        BotState State { get; set; }


        List<Bet> NewBet(int bet);
    }
}
