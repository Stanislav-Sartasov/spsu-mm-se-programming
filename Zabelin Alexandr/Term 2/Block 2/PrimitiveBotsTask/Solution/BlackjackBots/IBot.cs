namespace BlackjackBots
{
    public interface IBot
    {
        public bool Hit(byte croupierCard, byte playerSum);      // must return true to hit and false to stand
    }
}