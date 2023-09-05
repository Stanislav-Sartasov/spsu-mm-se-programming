package service.bet

import request.BetRequest
import cash_account.Account
import service.transfer.TransferManager
import service.transfer.CashTransferManager
import util.BetStatus

class ListBetManager(
	private val casinoAccount: Account,
	private val betList: MutableList<BetRequest>
) : BetManager {

	private val transferManager: TransferManager = CashTransferManager()

	constructor(casinoAccount: Account) : this(casinoAccount, ArrayList<BetRequest>())

	override fun add(betRequest: BetRequest) {
		if (transferManager.transfer(betRequest.account, casinoAccount, betRequest.amount))
			betList.add(betRequest)
		else
			betRequest.status = BetStatus.REJECTED
	}

	override fun remove(betRequest: BetRequest) {
		betList.remove(betRequest)
	}

	override fun clear() {
		betList.clear()
	}

	override fun process(value: Int) {
		betList.forEach {
			if (it.bet.isWon(value)) {
				it.status = BetStatus.WON
				transferManager.transfer(casinoAccount, it.account, it.amount * it.coefficient)
			} else {
				it.status = BetStatus.LOST
			}
		}
	}
}