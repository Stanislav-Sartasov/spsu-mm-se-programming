package model.game;

import model.player.AbstractPlayer;
import model.player.PlayerController;
import model.service.account_manager.AccountManager;
import model.service.bet_pool.BetPool;

import java.util.ArrayList;
import java.util.Queue;

public abstract class Game {
	protected BetPool betPool;
	protected ArrayList<AbstractPlayer> playerList;
	protected ArrayList<PlayerController> controllerList;
	protected AccountManager accountManager;

	public Game(String poolId, AccountManager accountManager) {
		betPool = new BetPool(poolId);
		playerList = new ArrayList<>();
		controllerList = new ArrayList<>();
		this.accountManager = accountManager;
	}

	protected abstract void addPlayer(PlayerController controller, int initialBalance);
}
