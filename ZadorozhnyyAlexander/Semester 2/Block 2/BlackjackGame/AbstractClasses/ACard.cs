namespace AbstractClasses
{
    public abstract class ACard
    {
        public string CardName { get; protected set; }
        public string CardSuit { get; protected set; }
        public int CardNumber { get; protected set; }
    }
}
