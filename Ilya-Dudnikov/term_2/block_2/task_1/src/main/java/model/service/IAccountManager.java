package model.service;

public interface IAccountManager {

	public void createNewAccount(String id, int initialBalance);

	public void transfer(String from, String to, int value);

	public int getBalanceById(String id);

	public void deposit(String to, int value);
}
