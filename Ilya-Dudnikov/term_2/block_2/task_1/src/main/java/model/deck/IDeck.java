package model.deck;

import model.card.Card;

import java.util.Collections;

public interface IDeck {
    public Card getCardAt(int pos);

    public void shuffle();
}
