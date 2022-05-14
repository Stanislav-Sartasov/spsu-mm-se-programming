package service.bet

import request.BetRequest

interface BetManager {
	fun add(betRequest: BetRequest)
	fun remove(betRequest: BetRequest)
	fun clear()
	fun process(value: Int)
}