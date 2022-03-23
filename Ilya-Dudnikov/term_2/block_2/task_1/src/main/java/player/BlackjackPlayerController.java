package player;

import card.AbstractCard;

import java.util.ArrayList;

public class BlackjackPlayerController extends PlayerController {
    public BlackjackPlayerController(AbstractPlayer player) {
        super(player);
    }

    @Override
    public Action getAction(ArrayList<AbstractCard> allCards) {
        return null;
    }

    @Override
    public int getInitialBet() {
        return 0;
    }
}
