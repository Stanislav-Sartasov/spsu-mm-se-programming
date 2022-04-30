package game.controllers;

import card.AbstractCard;
import game.Action;
import player.AbstractPlayer;
import player.PlayerController;

import java.util.ArrayList;

public class StandController extends PlayerController {
	public StandController(AbstractPlayer player) {
		super(player);
	}

	@Override
	public Action getAction(ArrayList<AbstractCard> allCards) {
		return Action.STAND;
	}

	@Override
	public int getInitialBet() {
		return 15;
	}
}
