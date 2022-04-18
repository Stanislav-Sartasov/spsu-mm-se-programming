import game.BlackjackGameLauncher;
import player.BlackjackPlayer;
import player.PlayerController;

import java.util.ArrayList;
import java.util.Collections;

public class AppLauncher {
	public static void main(String[] args) {
		String poolId = "pool1337";
		String dealerId = "dealer1337";

		PlayerController basicBot = new BasicBot(new BlackjackPlayer("BasicBot"));
		PlayerController dealerBot = new DealerBot(new BlackjackPlayer("DealerBot"));
		PlayerController randomBot = new RandomBot(new BlackjackPlayer("RandomBot"));

		ArrayList<PlayerController> controllerList = new ArrayList<>();
		controllerList.add(basicBot);
		controllerList.add(dealerBot);
		controllerList.add(randomBot);

		System.out.println("Bots initially get 1000$ each");

		ArrayList<Integer> balances = new ArrayList<>(Collections.nCopies(3, 1000));
		int totalRounds = 40;
		int poolBalance = 1_000_000;

		int numberOfDecks = 8;

		System.out.println("Casino's pool initially has 1000000$\n");

		BlackjackGameLauncher.run(poolId, dealerId, controllerList, balances, totalRounds, poolBalance, numberOfDecks);

		System.out.println("Results after 40 rounds:");
		System.out.println("Casino has " + balances.get(3) + "$");
		System.out.println("Bot with basic strategy has " + balances.get(0) + "$");
		System.out.println("Bot that acted like dealer has " + balances.get(1) + "$");
		System.out.println("Bot that made random moves has " + balances.get(2) + "$");
	}
}
