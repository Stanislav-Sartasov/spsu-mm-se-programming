namespace BlackjackBots
{
    public class BaseStrategyBot : IBot
    {
        public bool Hit(byte croupierCard, byte playerSum) 
        {
            if ((playerSum < 12) ||
                (playerSum == 12 && croupierCard < 5) ||
                ((playerSum < 15 && playerSum > 12) && (croupierCard < 7 && croupierCard > 2)) ||
                (playerSum < 17 && croupierCard > 6))
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}
