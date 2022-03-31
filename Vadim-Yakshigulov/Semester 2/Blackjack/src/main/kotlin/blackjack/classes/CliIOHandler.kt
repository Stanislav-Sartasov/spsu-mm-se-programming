package blackjack.classes

import blackjack.interfaces.ICard
import blackjack.interfaces.IOHandler
import java.io.InputStream
import java.io.OutputStream
import java.io.PrintStream


class CliIOHandler(override val inStream: InputStream, override val outStream: OutputStream) : IOHandler {
    override fun println(msg: String) {
        PrintStream(outStream).println(msg)
    }

    override fun readLine(): String {
        return inStream.bufferedReader().readLine() ?: ""
    }

    override fun showCards(msg: String, cards: List<ICard>) {
        /**
         * shows only face-up cards
         */
        this.println(msg)
        for (card in cards) {
            if (card.isFaceUp)
                this.println(card.toString())
        }
    }

    override fun showHands(msg: String, hands: List<Hand>) {
        /**
         * shows only non-blocked hands
         */
        this.println(msg)
        var i = 1
        for (hand in hands) {
            if (!hand.blocked) {
                this.println("$i. ${hand.cards}")
                i++
            }
        }
    }

    override fun chooseFromPossibleActions(msg: String, vararg actionsNames: String): String {
        var choice: String
        do {
            this.println(msg)
            actionsNames.forEach { this.println(it) }
            choice = this.readLine()
        } while (choice !in actionsNames)

        return choice
    }

    override fun chooseFromHands(msg: String, defaultReturn: Hand, vararg hands: Hand): Hand {
        if (hands.isEmpty()) return defaultReturn
        if (hands.size == 1) return hands.first()
        var choice: Int
        do {
            this.println(msg)
            this.println("1..${hands.size}")
            choice = try {
                this.readLine().toInt()
            } catch (e: Exception) {
                -1
            }

        } while (choice !in 1..hands.size)

        return hands[choice - 1]
    }

    override fun chooseFromBetsInRange(msg: String, start: Int, stop: Int): Int {
        var choice: Int
        do {
            this.println(msg)
            this.println("$start..$stop")
            choice = try {
                this.readLine().toInt()
            } catch (e: Exception) {
                start - 1
            }
        } while (choice !in start..stop)

        return choice
    }
}