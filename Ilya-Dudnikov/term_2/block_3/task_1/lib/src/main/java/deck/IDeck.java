package deck;

import card.Card;

public interface IDeck {
    public Card getCardAt(int pos);

    public void shuffle();
}
