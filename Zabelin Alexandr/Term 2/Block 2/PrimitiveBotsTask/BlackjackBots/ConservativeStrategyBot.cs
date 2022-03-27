namespace BlackjackBots
{
    public class ConservativeStrategyBot : IBot
    {
        public bool Hit(byte croupierCard, byte playerSum)
        {
            return playerSum < 12;
        }
    }
}
