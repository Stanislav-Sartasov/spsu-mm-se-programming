namespace BlackjackBots
{
    public class RiskyStrategyBot : IBot
    {
        public bool Hit(byte croupierCard, byte playerSum)
        {
            return playerSum < 16;
        }
    }
}
