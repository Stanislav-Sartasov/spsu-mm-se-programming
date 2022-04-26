using GameTools;

namespace Player
{
    public interface IPlayer
    {
        public List<Hand> Hands { get; set; }
        public PlayerState Flag { get; set; }
        public int Cash { get; set; }
        public int GamePlayed { get; set; }
        public HandState Play(Hand hand, List<Card> dealerCards);
        public void MakeBet(int minBet);
        public bool IsLeave();
    }
}
