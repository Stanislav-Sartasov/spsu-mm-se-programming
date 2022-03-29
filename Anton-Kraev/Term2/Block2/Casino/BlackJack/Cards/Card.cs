namespace Cards
{
    public class Card
    {
        public string Name { get; }

        public Card(string name)
        {
            Name = name;
        }

        public int GetCardValue()
        {
            if (!"2 3 4 5 6 7 8 9 10 A".Contains(Name))
                return 0;
            if (Name == "A")
                return 11;
            return int.Parse(Name);
        }
    }
}