package model.bet;

import bet.Bet;
import bet.BetStatus;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class BetTest {
    private Bet bet;
    private final String bettorId = "bettor1337";
    private final int betValue = 50;

    @BeforeEach
    void setUp() {
        bet = new Bet(bettorId, betValue);
    }

    @Test
    void testGetters() {
        assertAll(
                () -> assertEquals(bettorId, bet.getBettorId()),
                () -> assertEquals(betValue, bet.getValue()),
                () -> assertEquals(BetStatus.PENDING, bet.getBetStatus())
        );
    }

    @Test
    void testSetter() {
        bet.changeBetStatus(BetStatus.DRAW);

        assertEquals(BetStatus.DRAW, bet.getBetStatus());
    }
}