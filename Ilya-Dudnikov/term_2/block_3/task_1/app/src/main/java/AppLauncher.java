import game.BlackjackGameLauncher;
import player.BlackjackPlayer;
import player.PlayerController;

import java.io.IOException;
import java.net.MalformedURLException;
import java.util.ArrayList;
import java.util.Collections;
import java.util.jar.JarFile;

public class AppLauncher {
	public static void main(String[] args) {
		BotLoader botLoader = null;

		try {
			botLoader = BotLoader.createBotLoader(args[0]);
		} catch (IOException e) {
			e.printStackTrace();
			System.out.println(e.getMessage());
		}

		ArrayList<PlayerController> controllerList = botLoader.loadBotsFromPath(args[0]);

		String poolId = "pool1337";
		String dealerId = "dealer1337";

		System.out.println("Bots initially get 1000$ each");

		ArrayList<Integer> balances = new ArrayList<>(Collections.nCopies(controllerList.size(), 1000));
		int totalRounds = 40;
		int poolBalance = 1_000_000;

		int numberOfDecks = 8;

		System.out.println("Casino's pool initially has 1000000$\n");

		BlackjackGameLauncher.run(
				poolId,
				dealerId,
				controllerList,
				balances,
				totalRounds,
				poolBalance,
				numberOfDecks
		);

		System.out.println("Results after 40 rounds:");
		System.out.println("Casino has " + balances.get(balances.size() - 1) + "$");

		for (int i = 0; i < controllerList.size(); i++) {
			System.out.println(controllerList.get(i).getPlayer().getId() + " has " + balances.get(i));
		}
	}
}
