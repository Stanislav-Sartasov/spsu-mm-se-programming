package game;

import player.BlackjackDealer;
import player.PlayerController;
import service.account_manager.AccountManager;

import java.util.ArrayList;

public class BlackjackGameLauncher {
    public static void run(
            String poolId,
            String dealerId,
            ArrayList<PlayerController> controllerList,
            ArrayList<Integer> balances,
            int totalRounds,
            int poolBalance,
            int numberOfDecks
    ) {
        BlackjackGame game = new BlackjackGame(
                poolId,
                new AccountManager(),
                new BlackjackDealer(dealerId),
                poolBalance,
                numberOfDecks
        );

        for (int i = 0; i < totalRounds; i++) {
            for (int index = 0; index < controllerList.size(); index++) {
                game.addPlayer(controllerList.get(index), balances.get(index));
            }
            game.playRound();
        }

        for (int i = 0; i < controllerList.size(); i++) {
            balances.set(i, game.accountManager.getBalanceById(controllerList.get(i).getPlayer().getId()));
        }
        balances.add(game.accountManager.getBalanceById(poolId));
    }
}
