package player;

import card.CardStatus;
import deck.Shoe;

public interface IDealer {
    public void dealTo(AbstractPlayer player, Shoe shoe, CardStatus cardStatus);
}
