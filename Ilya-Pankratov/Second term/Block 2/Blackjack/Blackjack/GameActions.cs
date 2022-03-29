using Player;
using GameTools;

namespace Blackjack
{
    internal static class GameActions
    {
        internal static void Hit(Hand playerHand, Dealer dealer, GameDeck gameDeck)
        {
            dealer.DrawCardToHand(gameDeck, playerHand); 
        }

        internal static void Double(Hand playerHand, Dealer dealer, GameDeck gameDeck)
        {
            dealer.DrawCardToHand(gameDeck, playerHand);
            playerHand.Bet *= 2;
            playerHand.Flag = HandState.Stand;
        }

        internal static void Surrender(Hand playerHand)
        {
            playerHand.Flag = HandState.Surrender;
        }

        internal static void Stand(Hand playerHand)
        {
            playerHand.Flag = HandState.Stand;
        }

        internal static void Split(IPlayer player, Hand playerHand)
        {
            player.Hands.Add(new Hand()); // create new hand

            Card card = playerHand.Cards.Last();

            // fill new hand

            player.Hands.Last().Cards.Add(card); // add last card
            player.Hands.Last().RecountPoints(); // add points
            player.Cash -= playerHand.Bet;
            player.Hands.Last().Bet = playerHand.Bet; // make bet

            // update old hand

            playerHand.Cards.Remove(card); // delete last card
            playerHand.RecountPoints(); // remove points
        }
    }
}