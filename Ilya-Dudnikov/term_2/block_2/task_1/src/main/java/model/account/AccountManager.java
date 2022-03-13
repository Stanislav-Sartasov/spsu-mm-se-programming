package model.account;

import java.util.HashMap;

public class AccountManager implements IAccountManager {
	private HashMap<String, Integer> balanceMap;

	public void createNewAccount(String id, int initialBalance) {
		if (balanceMap.containsKey(id))
			throw new IllegalArgumentException("An account with such id already exists");

		balanceMap.put(id, initialBalance);
	}

	public int getBalanceById(String id) {
		return balanceMap.get(id);
	}

	public void transfer(String from, String to, int value) {
		if (balanceMap.get(from) < value)
			throw new IllegalArgumentException("Insufficient balance");

		balanceMap.put(to, balanceMap.get(to) + value);
		balanceMap.put(from, balanceMap.get(from) + value);
	}

	public void deposit(String to, int value) {
		balanceMap.put(to, balanceMap.get(to) + value);
	}
}