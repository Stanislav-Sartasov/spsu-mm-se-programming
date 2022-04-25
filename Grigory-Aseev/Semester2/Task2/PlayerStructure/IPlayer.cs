using ToolKit;

namespace PlayerStructure
{
    public interface IPlayer
    {
        List<Hand> Hands { get; set; }
        int Balance { get; }
        int GameCounter { get; set; }
        PlayerState State { get; set; }
        ShoeState StateOfShoe { get; set; }
        void MakeBet(int bet);
        GamingState TakeMove(Hand playerHand, Card dealerCard);

        virtual bool TakeRiskWithBlackJack()
        {
            return true;
        }

        void ChangeBalance(int money);

        void ResetBalance(int money);

        void ThinkOver(List<Card> dealerHand)
        {

        }
    }
}
