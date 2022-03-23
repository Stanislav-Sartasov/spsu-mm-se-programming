package hand;

import card.BlackjackCard;

import java.util.ArrayList;

public interface IHand {
    public ArrayList<BlackjackCard> getVisibleCards();

    public void addCard(BlackjackCard card);

    public void clear();
}
