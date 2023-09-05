using BotStructure;
using Cards;

namespace BaseBot
{
    public sealed class BaseStrategyBot : Bot, IBot
    {
        public BaseStrategyBot(int balance) : base(balance) { }

        protected override int NewBet(Shoes shoes)
        {
            return CurrentBet;
        }
    }
}