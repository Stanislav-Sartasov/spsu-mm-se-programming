package game;

import player.AbstractPlayer;
import player.PlayerController;
import service.account_manager.AccountManager;
import service.bet_pool.BetPool;

import java.util.ArrayList;

public abstract class Game {
	protected BetPool betPool;
	protected ArrayList<AbstractPlayer> playerList;
	protected ArrayList<PlayerController> controllerList;
	protected AccountManager accountManager;

	public Game(String poolId, AccountManager accountManager, int initialPoolBalance) {
		betPool = new BetPool(poolId);
		playerList = new ArrayList<>();
		controllerList = new ArrayList<>();
		this.accountManager = accountManager;
		accountManager.createNewAccount(poolId, initialPoolBalance);
	}

	protected abstract void addPlayer(PlayerController controller, int initialBalance);
}
