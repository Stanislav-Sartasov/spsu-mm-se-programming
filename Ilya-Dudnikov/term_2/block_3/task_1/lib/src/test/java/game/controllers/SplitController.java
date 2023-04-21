package game.controllers;

import card.AbstractCard;
import game.Action;
import player.AbstractPlayer;
import player.PlayerController;

import java.util.ArrayList;

public class SplitController extends PlayerController {
	public SplitController(AbstractPlayer player) {
		super(player);
	}

	@Override
	public Action getAction(ArrayList<AbstractCard> allCards) {
		return Action.SPLIT;
	}

	@Override
	public int getInitialBet() {
		return 20;
	}
}
