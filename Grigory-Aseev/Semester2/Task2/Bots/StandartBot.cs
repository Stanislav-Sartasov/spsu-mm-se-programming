namespace Bots
{
    public class StandartBot : ABasicBot
    {
        public StandartBot(int money = 10000, int games = 40) : base(money, games)
        {

        }

        public override void MakeBet(int bet)
        {
            base.MakeBet(bet * 2);
        }
    }
}