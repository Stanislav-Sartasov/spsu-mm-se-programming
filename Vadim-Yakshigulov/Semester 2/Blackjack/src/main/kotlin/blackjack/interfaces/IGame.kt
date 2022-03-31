package blackjack.interfaces

interface IGame {
    val minimumBetSize: Int
    val ioHandler: IOHandler
    val dealer: IDealer
    val player: IPlayer
    val shoe: IShoe
    fun run()
}