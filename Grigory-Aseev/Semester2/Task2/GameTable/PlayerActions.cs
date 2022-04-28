using ToolKit;
using PlayerStructure;

namespace GameTable
{
    public static class PlayerActions
    {
        public static void Stand(Hand playerHand)
        {
            playerHand.State = GamingState.Stand;
        }

        public static void Surrender(Hand playerHand)
        {
            playerHand.State = GamingState.Surrender;
        }

        public static void DoubleDown(IPlayer player, Hand playerHand, Dealer dealer, Shoe shoe)
        {
            Hit(playerHand, dealer, shoe);

            if (player.Balance < playerHand.Bet)
            {
                return;
            }

            Stand(playerHand);

            player.ChangeBalance(-playerHand.Bet);
            playerHand.Bet *= 2;
        }

        public static void Hit(Hand playerHand, Dealer dealer, Shoe shoe)
        {
            dealer.DrawCardToPlayer(shoe, playerHand);
            playerHand.UpdateScore();
            playerHand.State = GamingState.Hit;
        }

        public static void Split(IPlayer player, Hand playerHand, Shoe shoe, Dealer dealer)
        {
            Card card = playerHand.Cards.Last();

            // changing the old hand
            playerHand.Cards.Remove(card);
            Hit(playerHand, dealer, shoe);

            // creating a new hand
            Hand hand = new Hand(playerHand.Bet);
            hand.Cards.Add(card);
            Hit(hand, dealer, shoe);

            if (card.CardPoint == CardPoints.Ace)
            {
                if (IsEqualCards(playerHand))
                {
                    Split(player, playerHand, shoe, dealer);
                }
                else
                {
                    Stand(playerHand);
                }

                if (IsEqualCards(hand))
                {
                    Split(player, hand, shoe, dealer);
                }
                else
                {
                    Stand(hand);
                }
            }

            player.Hands.Add(hand);
        }

        private static bool IsEqualCards(Hand hand)
        {
            return hand.Cards[0].CardPoint == hand.Cards[1].CardPoint;
        }
    }
}