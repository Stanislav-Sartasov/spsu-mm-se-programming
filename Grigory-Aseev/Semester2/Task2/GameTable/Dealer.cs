using ToolKit;
using PlayerStructure;

namespace GameTable
{
    public class Dealer
    {
        public List<Card> Hand { get; private set; }
        public int Points { get; private set; }
        public Card? VisibleCard
        {
            get { return Hand.FirstOrDefault(); }
        }

        public Dealer()
        {
            Hand = new List<Card>();
            Points = 0;
        }

        public void Play(Shoe shoe)
        {
            Points += Hand[1].GetPoints(Points);

            while (Points < 17)
            {
                Hand.Add(shoe.TakeCard());
                Points += Hand.LastOrDefault().GetPoints(Points);
            }
        }

        public bool UpdateShoe(ref Shoe shoe, int countOfDecks)
        {
            if (shoe.CheckUpdate())
            {
                shoe = new Shoe(countOfDecks);
                return true;
            }

            return false;
        }

        public void ResetTable(IPlayer player)
        {
            player.Hands.Clear();
            player.Hands.Add(new Hand());
            Hand.Clear();
            Points = 0;
        }

        public Card DrawCard(Shoe shoe)
        {
            return shoe.TakeCard();
        }

        public void DrawCardToPlayer(Shoe shoe, Hand hand)
        {
            hand.Cards.Add(DrawCard(shoe));
        }

        public void StartGame(Shoe shoe, IPlayer player)
        {
            if (player.State == PlayerState.Stop)
            {
                return;
            }

            for (int i = 0; i < 2; i++)
            {
                DrawCardToPlayer(shoe, player.Hands[0]);
                Hand.Add(DrawCard(shoe));
            }

            Points += VisibleCard.GetPoints(0);
        }

        public bool IsBlackJack()
        {
            return (VisibleCard?.GetPoints() >= 10) && (Points + Hand[1].GetPoints(Points)) == 21;
        }
    }
}