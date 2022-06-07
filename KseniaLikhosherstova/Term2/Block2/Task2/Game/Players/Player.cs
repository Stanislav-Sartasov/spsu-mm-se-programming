using Game.Cards;



namespace Game.Players
{
    public class Player : Hand
    {
        public override string Name { get; }
        public PlayerState State { get; private set; }
        public int Money { get; protected set; }


        public Player(string name, int money)
        {
            Name = name;
            Money = money;
        }


        public virtual PlayerAction Move() { return PlayerAction.Hit; }
        public virtual int Bet() { return 0; }


        public override void ClearHand()
        {
            base.ClearHand();

            State = PlayerState.FirstMove;
        }

        public override void Hit(Card card)
        {
            base.Hit(card);
            State = PlayerState.Playing;

        }

        public void Double(Card card, int initialBet)
        {

            Money -= initialBet;
            Hit(card);

            if (State != PlayerState.Busted)
                State = PlayerState.Standing;
        }

        public void Stand()
        {
            State = PlayerState.Standing;
        }

        public void TakeMoney(int amount)
        {
            Money += amount;
        }

    }
}
