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
            ArrayList<Integer> initialBalances,
            int totalRounds,
            int poolBalance
    ) {
        BlackjackGame game = new BlackjackGame(
                poolId,
                new AccountManager(),
                new BlackjackDealer(dealerId),
                poolBalance
        );

        for (int i = 0; i < totalRounds; i++) {
            for (int index = 0; index < controllerList.size(); index++) {
                game.addPlayer(controllerList.get(index), initialBalances.get(index));
            }
        }

        for (int i = 0; i < controllerList.size(); i++) {
            game.initialBet(i);
        }
        game.playRound();
    }
}
