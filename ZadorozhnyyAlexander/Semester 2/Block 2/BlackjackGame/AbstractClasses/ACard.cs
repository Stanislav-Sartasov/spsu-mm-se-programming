namespace AbstractClasses
{
    public abstract class ACard
    {
        public CardNames CardName { get; protected set; }
        public CardSuits CardSuit { get; protected set; }
        public int CardNumber { get; protected set; }
    }
}
