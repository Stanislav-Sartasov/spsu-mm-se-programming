package model.deck;

import model.card.BlackjackCard;

import java.util.Collections;

public interface IShoe {
    public void shuffle();

    public void resetShoe();

    public BlackjackCard dealCard();
}
