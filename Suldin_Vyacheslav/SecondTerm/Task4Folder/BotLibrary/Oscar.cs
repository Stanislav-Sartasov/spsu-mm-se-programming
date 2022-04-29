namespace BotLibrary
{
    public class Oscar : Bot
    {

        public int Wins;
        public Oscar(int str)
            : base(str)
        {
        }
        public override void MakeBet(int hand)
        {

            if (LastBank < bank)
                Wins++;
            int bet = 100 + Wins*3;

            if (bet < bank)
            {
            }
            else bet = bank;

            bets[hand] = bet;
            LastBank = bank;
            bank -= bet;

        }
    }
}
