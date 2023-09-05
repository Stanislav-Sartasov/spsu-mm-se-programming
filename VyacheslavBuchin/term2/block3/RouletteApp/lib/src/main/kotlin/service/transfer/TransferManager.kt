package service.transfer

import cash_account.Account

interface TransferManager {

	fun transfer(fromAccount: Account, toAccount: Account, amount: Double): Boolean

}