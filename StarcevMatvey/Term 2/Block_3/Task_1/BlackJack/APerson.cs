using BlackJackEnumerations;

namespace BlackJack
{
    public abstract class APerson
    {
        public List<Card> Cards { get; internal set; }

        public APerson()
        {
            Cards = new List<Card>();
        }

        public void TookCard(Card card)
        {
            Cards.Add(card);
        }

        public void GetNewHand(ShuffleMachine machine)
        {
            Cards = new List<Card>();
            Cards.Add(machine.TrowCard());
            Cards.Add(machine.TrowCard());
        }

        public int GetScore()
        {
            List<int> possibleSums = new List<int> { 0 };
            foreach (Card card in Cards)
            {
                if (card.Value == CardValue.Ace)
                {
                    possibleSums = possibleSums.Select(x => x + 1).Concat(possibleSums.Select(x => x + 11)).ToList();
                }
                else
                {
                    possibleSums = possibleSums.Select(x => x + card.GetValue()).ToList();
                }
            }

            return possibleSums.Select(x => Convert.ToInt32(x < 22) * x).Max();
        }
    }
}
