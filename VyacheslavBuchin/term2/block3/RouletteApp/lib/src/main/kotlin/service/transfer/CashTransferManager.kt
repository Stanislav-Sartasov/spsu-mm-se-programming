package service.transfer

import cash_account.Account

class CashTransferManager : TransferManager {

	override fun transfer(fromAccount: Account, toAccount: Account, amount: Double): Boolean {
		if (fromAccount.balance() >= amount) {
			fromAccount.removeMoney(amount)
			toAccount.addMoney(amount)
			return true
		}
		return false
	}

}