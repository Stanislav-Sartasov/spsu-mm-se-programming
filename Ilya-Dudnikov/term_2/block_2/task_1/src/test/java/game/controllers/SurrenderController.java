package game.controllers;

import card.AbstractCard;
import game.Action;
import player.AbstractPlayer;
import player.PlayerController;

import java.util.ArrayList;

public class SurrenderController extends PlayerController {
	public SurrenderController(AbstractPlayer player) {
		super(player);
	}

	@Override
	public Action getAction(ArrayList<AbstractCard> allCards) {
		return Action.SURRENDER;
	}

	@Override
	public int getInitialBet() {
		return 30;
	}
}
