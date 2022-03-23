package player;

import card.AbstractCard;

import java.util.ArrayList;

public abstract class PlayerController {
    protected AbstractPlayer player;

    public PlayerController(AbstractPlayer player) {
        this.player = player;
    }

    public abstract Action getAction(ArrayList<AbstractCard> allCards);

    public abstract int getInitialBet();

    public AbstractPlayer getPlayer() {
        return player;
    }
}
