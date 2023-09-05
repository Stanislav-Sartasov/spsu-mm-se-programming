import card.AbstractCard;
import game.Action;
import player.AbstractPlayer;
import player.PlayerController;

import java.util.ArrayList;
import java.util.Random;

public class RandomBot extends PlayerController {
	public RandomBot(AbstractPlayer player) {
		super(player);
	}

	@Override
	public Action getAction(ArrayList<AbstractCard> allCards) {
		Random random = new Random();
		return Action.values()[random.nextInt(5)];
	}

	@Override
	public int getInitialBet() {
		return 100;
	}
}
