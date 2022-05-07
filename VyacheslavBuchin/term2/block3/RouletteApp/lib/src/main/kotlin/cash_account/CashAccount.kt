package cash_account

class CashAccount(private var balance: Double) : Account {
	override fun balance() = balance

	override fun addMoney(value: Double) {
		balance += value
	}

	override fun removeMoney(value: Double) {
		balance -= value
	}

	override fun setMoney(value: Double) {
		balance = value
	}
}