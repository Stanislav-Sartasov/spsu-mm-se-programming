package game.controllers;

import card.AbstractCard;
import game.Action;
import player.AbstractPlayer;
import player.PlayerController;

import java.util.ArrayList;

public class HitController extends PlayerController {
	public HitController(AbstractPlayer player) {
		super(player);
	}

	@Override
	public Action getAction(ArrayList<AbstractCard> allCards) {
		return Action.HIT;
	}

	@Override
	public int getInitialBet() {
		return 10;
	}
}
