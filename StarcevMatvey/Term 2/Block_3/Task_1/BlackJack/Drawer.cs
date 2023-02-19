using static System.Console;

namespace BlackJack
{
    public class Drawer
    {
        public Drawer()
        {

        }

        public void DrawCard(Card card)
        {
            Write(" " + card.Suit + " " + card.Value + " ");
        }

        public void DrawHand(List<Card> cards)
        {
            cards.ForEach(x => DrawCard(x));
            WriteLine();
        }

        public void DrawPlayer(Player player)
        {
            WriteLine("\n<=================================>\n");
            WriteLine(player.Name + ": " + player.Balance + "\nBet: " + player.Bet);
            DrawHand(player.Cards);
            WriteLine("<=================================>\n");
        }

        public void DrawCroupier(Croupier croupier)
        {
            WriteLine("\n<=================================>\nCroupier");
            DrawHand(croupier.Cards);
            WriteLine("<=================================>\n");
        }
    }
}
