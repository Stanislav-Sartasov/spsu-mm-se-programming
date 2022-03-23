package model.game;

import model.player.BlackjackDealer;
import model.player.PlayerController;
import model.service.account_manager.AccountManager;

import java.util.ArrayList;

public class BlackjackGameLauncher {
    private final int TOTAL_ROUNDS = 40;

    public void run(
            String poolId,
            String dealerId,
            ArrayList<PlayerController> controllerList,
            ArrayList<Integer> initialBalances
    ) {
        BlackjackGame game = new BlackjackGame(
                poolId,
                new AccountManager(),
                new BlackjackDealer(dealerId)
        );

        for (int i = 0; i < TOTAL_ROUNDS; i++) {
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
