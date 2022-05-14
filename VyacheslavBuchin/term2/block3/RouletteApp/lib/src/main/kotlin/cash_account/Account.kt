package cash_account

interface Account {
	fun balance(): Double

	fun addMoney(value: Double)

	fun removeMoney(value: Double)

	fun setMoney(value: Double)

}