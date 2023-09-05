package deck;

import card.BlackjackCard;

public interface IShoe {
    public void shuffle();

    public void resetShoe();

    public BlackjackCard dealCard();
}
