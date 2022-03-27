using BlackjackBots;

namespace Blackjack
{
    public class Game
    {
        private float playerBalance;

        public float StartBalance { get; set; }

        public Game(float startBalance)
        {
            this.playerBalance = startBalance;
            this.StartBalance = startBalance;
        }

        public void Play(uint betsNumber, float betValue, IBot bot)
        {
            this.playerBalance = this.StartBalance;

            for (uint i = 0; i < betsNumber; i++)
            {
                Deck deck = new Deck(8);
                byte croupierSum, playerSum, croupierLastCard, playerLastCard, croupierVisibleCard;
                bool isLost = false;
                bool doesPlayerHit;

                // Croupier takes 2 cards

                croupierVisibleCard = deck.TakeCard();
                croupierSum = croupierVisibleCard;
                croupierLastCard = deck.TakeCard();
                croupierSum += croupierLastCard == 11 ? AceWeight(croupierSum) : croupierLastCard;  // checking whether the last card is Ace, and updating the sum

                if (IsBlackjack(croupierSum))
                {
                    this.playerBalance -= betValue;

                    continue;                         // If croupier has blackjack, he wins and game stops 
                }

                // Player takes 2 cards

                playerSum = deck.TakeCard();
                playerLastCard = deck.TakeCard();
                playerSum += playerLastCard == 11 ? AceWeight(playerSum) : playerLastCard;  // checking whether the last card is Ace, and updating the sum

                if (IsBlackjack(playerSum))
                {
                    this.playerBalance += betValue * 1.5F;    // If player has blackjack, he wins and game stops 

                    continue;
                }

                // Player starts choosing variants

                doesPlayerHit = bot.Hit(croupierVisibleCard, playerSum);

                while (doesPlayerHit)
                {
                    playerLastCard = deck.TakeCard();
                    playerSum += playerLastCard;

                    if (IsBust(playerSum))
                    {
                        this.playerBalance -= betValue;
                        isLost = true;

                        break;
                    }

                    doesPlayerHit = bot.Hit(croupierVisibleCard, playerSum);
                }

                if (isLost)               // It's the end of the game, if someone is lost
                {
                    continue;
                }

                // Croupier starts taking cards and stops, when get at least 17 in his sum

                while (croupierSum < 17)
                {
                    croupierLastCard = deck.TakeCard();
                    croupierSum += croupierLastCard;

                    if (IsBust(croupierSum))
                    {
                        isLost = true;

                        break;
                    }
                }

                if (isLost)               // It's the end of the game, if someone is lost
                {
                    continue;
                }

                // Final sums counting

                if (croupierSum > playerSum)
                {
                    this.playerBalance -= betValue;
                }
                else if (croupierSum < playerSum)
                {
                    this.playerBalance += betValue;
                }
            }
        }

        public float GetPlayerBalance()
        {
            return this.playerBalance;
        }

        private bool IsBlackjack(byte sum)
        {
            return sum == 21;
        }

        private bool IsBust(byte sum)
        {
            return sum > 21;
        }

        private byte AceWeight(byte sum)           // returns 11 or 1, depending on the sum
        {
            return (byte)(sum > 10 ? 1 : 11);
        }
    }
}