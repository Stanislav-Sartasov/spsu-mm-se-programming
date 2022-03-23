package model.player;

import model.card.CardStatus;
import model.deck.Shoe;

public interface IDealer {
    public void dealTo(AbstractPlayer player, Shoe shoe, CardStatus cardStatus);
}
