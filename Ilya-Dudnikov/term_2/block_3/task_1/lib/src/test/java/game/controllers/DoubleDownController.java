package game.controllers;

import card.AbstractCard;
import game.Action;
import player.AbstractPlayer;
import player.PlayerController;

import java.util.ArrayList;

public class DoubleDownController extends PlayerController {
	public DoubleDownController(AbstractPlayer player) {
		super(player);
	}

	@Override
	public Action getAction(ArrayList<AbstractCard> allCards) {
		return Action.DOUBLE_DOWN;
	}

	@Override
	public int getInitialBet() {
		return 25;
	}
}
