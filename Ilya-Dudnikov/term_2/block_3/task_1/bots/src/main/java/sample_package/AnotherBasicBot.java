package sample_package;

import card.AbstractCard;
import game.Action;
import player.AbstractPlayer;
import player.PlayerController;

import java.util.ArrayList;

import static java.lang.Math.min;

public class AnotherBasicBot extends PlayerController {
	public AnotherBasicBot(AbstractPlayer player) {
		super(player);
	}

	@Override
	public Action getAction(ArrayList<AbstractCard> allCards) {
		int handScore = player.getHand().getHandScore();
		int dealerCard = allCards.get(0).getCard().rank();

		if (handScore <= 8) {
			return Action.HIT;
		}

		if (handScore <= 9 && dealerCard <= 6) {
			return Action.DOUBLE_DOWN;
		} else if (handScore <= 9) {
			return Action.HIT;
		}

		if (handScore <= 11 && dealerCard <= 6) {
			return Action.DOUBLE_DOWN;
		} else if (handScore <= 11) {
			if (dealerCard == 14 || min(dealerCard, 10) >= handScore)
				return Action.HIT;
			return Action.DOUBLE_DOWN;
		}

		if (handScore <= 16 && dealerCard <= 6) {
			return Action.STAND;
		} else if (handScore <= 16) {
			return Action.HIT;
		}

		return Action.STAND;
	}

	@Override
	public int getInitialBet() {
		return 100;
	}
}
