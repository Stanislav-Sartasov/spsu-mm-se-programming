package service.account_manager;

import org.junit.jupiter.api.BeforeEach;
import org.junit.jupiter.api.Test;

import static org.junit.jupiter.api.Assertions.*;

class AccountManagerTest {
	private AccountManager accountManager;

	@BeforeEach
	void setUp() {
		accountManager = new AccountManager();
		accountManager.createNewAccount("newcomer1337", 100);
	}

	@Test
	void createNewAccountThatAlreadyExists() {
		assertDoesNotThrow(() -> accountManager.createNewAccount("newcomer1337", 10));
	}

	@Test
	void getBalanceById() {
		assertEquals(100, accountManager.getBalanceById("newcomer1337"));
	}

	void getBalanceByNonExistentId() {
		accountManager.getBalanceById("Who are you?");
	}

	@Test
	void transfer() {
		accountManager.createNewAccount("boss1337", 50);

		accountManager.transfer("boss1337", "newcomer1337", 25);
		assertEquals(25, accountManager.getBalanceById("boss1337"));
		assertEquals(125, accountManager.getBalanceById("newcomer1337"));
	}

	@Test
	void transferWithInsufficientBalance() {
		accountManager.createNewAccount("boss1337", 50);

		assertThrows(IllegalArgumentException.class, () -> accountManager.transfer("boss1337", "newcomer1337", 100));
	}

	@Test
	void deposit() {
		accountManager.deposit("newcomer1337", 100);
		assertEquals(200, accountManager.getBalanceById("newcomer1337"));
	}
}