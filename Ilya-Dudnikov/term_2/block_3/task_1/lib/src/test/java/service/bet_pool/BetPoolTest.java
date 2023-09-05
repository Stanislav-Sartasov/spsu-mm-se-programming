package service.bet_pool;

import bet.Bet;
import bet.BetStatus;
import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Nested;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class BetPoolTest {
	BetPool betPool;

	@BeforeEach
	void setUp() {
		betPool = new BetPool("pool1337");
		betPool.addBet(new Bet("bettor1337", 100));
	}

	@Nested
	class GettersTest {
		@Test
		void getPoolId() {
			assertEquals("pool1337", betPool.getPoolId());
		}

		@Test
		void getBetValueAt() {
			assertEquals(100, betPool.getBetValueAt(0));
		}

		@Test
		void getBetValueAtOutOfBounds() {
			assertThrows(IndexOutOfBoundsException.class, () -> betPool.getBetValueAt(1));
		}

		@Test
		void getBetStatusAt() {
			assertEquals(BetStatus.PENDING, betPool.getBetStatusAt(0));
		}

		@Test
		void getBetStatusAtOutOfBounds() {
			assertThrows(IndexOutOfBoundsException.class, () -> betPool.getBetStatusAt(1));
		}
	}

	@Nested
	class StateChangersTest {
		@Test
		void doubleBet() {
			betPool.doubleBet(0);
			assertEquals(200, betPool.getBetValueAt(0));
		}

		@Test
		void doubleBetOutOfBounds() {
			assertThrows(IndexOutOfBoundsException.class, () -> betPool.doubleBet(1));
		}

		@Test
		void insertBet() {
			betPool.insertBet(new Bet("new bettor1337", 150), 0);

			assertEquals(150, betPool.getBetValueAt(0));
			assertEquals(100, betPool.getBetValueAt(1));
		}

		@Test
		void insertBetOutOfBounds() {
			assertThrows(IndexOutOfBoundsException.class, () -> betPool.insertBet(new Bet("new bettor1337", 100), 2));
		}

		@Test
		void addBet() {
			betPool.addBet(new Bet("new bettor1337", 150));

			assertEquals(150, betPool.getBetValueAt(1));
			assertEquals(100, betPool.getBetValueAt(0));
		}

		@Test
		void clearPool() {
			betPool.clearPool();

			assertThrows(IndexOutOfBoundsException.class, () -> betPool.getBetValueAt(0));
		}

		@Test
		void changeBetStatus() {
			betPool.changeBetStatus(0, BetStatus.LOST);

			assertEquals(BetStatus.LOST, betPool.getBetStatusAt(0));
		}

		@Test
		void changeBetStatusOutOfBounds() {
			assertThrows(IndexOutOfBoundsException.class, () -> betPool.changeBetStatus(1, BetStatus.LOST));
		}
	}
}