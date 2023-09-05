using GameTable.BetsType;

namespace GameTable.BotStructure
{
    public abstract class CommonBot : IBot
    {
        public string Name { get; set; }
        public int Balance { get; set; }
        public int MinBet { get; set; }
        public int LastBet { get; set; }
        public int Gain { get; set; }
        public int GameCounter { get; set; }
        public int WinsCounter { get; set; }
        public bool LastGame { get; set; }
        public BotState State { get; set; }
       
        public CommonBot(string name, int balance, int counter)
        {
            Name = name;
            Balance = balance;
            MinBet = 100;
            LastBet = MinBet;
            GameCounter = counter;
            WinsCounter = 0;
            Gain = 0;
            LastGame = true;
            State = BotState.Play;
        }
        public abstract List<Bet> NewBet(int bet);
        
        


    }
}
