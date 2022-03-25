import card.AbstractCard;
import game.Action;
import player.AbstractPlayer;
import player.PlayerController;

import java.util.ArrayList;

public class DealerBot extends PlayerController {
	public DealerBot(AbstractPlayer player) {
		super(player);
	}

	@Override
	public Action getAction(ArrayList<AbstractCard> allCards) {
		if (player.getHand().getHandScore() < 17)
			return Action.HIT;
		return Action.STAND;
	}

	@Override
	public int getInitialBet() {
		return 100;
	}
}
