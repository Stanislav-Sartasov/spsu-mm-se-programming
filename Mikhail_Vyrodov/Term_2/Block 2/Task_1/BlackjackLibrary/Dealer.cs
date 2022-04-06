using System;
using DecksLibrary;
using BotsLibrary;

namespace BlackjackLibrary
{
    public class Dealer
    {
        public Decks PlayingDecks { get; private set; }
        public byte[] DealerCards { get; private set; } // dealers cards
        public byte[] DealerCardsCopy { get; private set; } // for correct testing

        private uint initialMoney; // initial balance of player
        private Bots playerStrategy; // what strategy player uses
        private byte bjFlag; // if player has blackjack
        private bool surrenderFlag;
        private uint firstAmount; // players score in first hand
        private uint secondAmount; // players score in secon hand
        private int firstWager; // players wager in first hand after his turn
        private int secondWager; // players wager in second hand after his turn

        public Dealer(Decks Cards, uint initialMoney, Bots strategy = 0, byte bjFlag = 0)
        {
            PlayingDecks = new Decks();
            PlayingDecks = Cards;
            DealerCards = new byte[21];
            playerStrategy = strategy;
            this.initialMoney = initialMoney;
            this.bjFlag = bjFlag;
        }

        public int Game()
        {
            DealerCards[0] = PlayingDecks.GetCard();
            byte firstResult, secondResult;
            int sum = 0; 
            uint wager = 16; // we choose the initial wager for player
            if (playerStrategy == Bots.BasicStrategy)
            {
                Player player = new Player(DealerCards[0], initialMoney, PlayingDecks, wager);
                player.PlayersTurn(player.FirstHand);
                firstAmount = player.FirstSum;
                secondAmount = player.SecondSum;
                surrenderFlag = player.SurrFlag;
                bjFlag = player.BjFlag;
                player.GetWagers(ref firstWager, ref secondWager);
            }
            else if (playerStrategy == Bots.CardsCounterStrategy)
            {
                CardsCounterStrategy player = new CardsCounterStrategy(DealerCards[0], initialMoney, PlayingDecks, wager);
                player.PlayersTurn(player.FirstHand);
                firstAmount = player.FirstSum;
                secondAmount = player.SecondSum;
                surrenderFlag = player.SurrFlag;
                bjFlag = player.BjFlag;
                player.GetWagers(ref firstWager, ref secondWager);
            }
            else
            {
                SimpleStrategy player = new SimpleStrategy(DealerCards[0], initialMoney, PlayingDecks, wager);
                player.PlayersTurn(player.FirstHand);
                firstAmount = player.FirstSum;
                secondAmount = player.SecondSum;
                surrenderFlag = player.SurrFlag;
                bjFlag = player.BjFlag;
                player.GetWagers(ref firstWager, ref secondWager);
            }
            // Console.Write("Players result - {0} {1} ", first_amount, second_amount);
            initialMoney = (uint)(initialMoney - (int)(firstWager + secondWager)); // that is players balance after his turn
            DealerCardsCopy = new byte[21];
            if (surrenderFlag)
            {
                for (int i = 0; i < 21; i++)
                {
                    DealerCardsCopy[i] = DealerCards[i];
                }
                initialMoney += (uint)firstWager;
                return firstWager;
            }
            firstResult = DealersTurn(firstAmount);
            if (secondAmount != 0)
            {
                secondResult = DealersTurn(secondAmount);
                if (secondResult == 1) // If dealer wins
                {
                    secondWager = (-1) * secondWager;
                }
                else if (secondResult == 0) // If player wins
                {
                    secondWager = secondWager * 2;
                }
                sum += secondWager; // If we have draw than we just add player wagers to sum
            }
            // Console.WriteLine();
            if (firstResult == 1)
            {
                firstWager = (-1) * firstWager;
            }
            else if (firstResult == 0)
            {
                firstWager = firstWager * 2;
            }
            sum += firstWager;
            initialMoney = (uint)((int)initialMoney + sum); // that is players balance after one game
            for (int i = 0; i < 21; i++)
            {
                DealerCardsCopy[i] = DealerCards[i];
                DealerCards[i] = 0;
            }
            return sum;
        }

        public byte DealersTurn(uint playerScore)
        {
            uint sum = 0;
            uint position = 0;
            byte flag = 0;
            for (uint i = 0; i < 21; i++)
            {
                sum += DealerCards[i];
            }
            while (sum < 17)
            {
                for (uint i = 0; i < 21; i++)
                {
                    if (DealerCards[i] == 1)
                    {
                        flag = 1;
                    }
                    else if (DealerCards[i] == 0)
                    {
                        position = i;
                        break;
                    }
                }
                if (flag != 0 && sum < 17)
                {
                    if (sum + 10 == 21 && bjFlag != 0)
                    {
                        if (bjFlag == 2)
                            bjFlag = 0;
                        else
                            bjFlag -= 1;
                        if (DealerCards[2] == 0)
                        {
                            return 2;
                        }
                        else
                            return 0;
                    }
                    else if (sum + 10 == 21 && bjFlag == 0 && playerScore == 21)
                    {
                        if (DealerCards[2] == 0)
                            return 1;
                        else
                            return 2;
                    }
                    if (sum + 10 >= playerScore && sum + 10 <= 21)
                    {
                        sum += 10;
                        if (sum > 17)
                            break;
                    }
                }
                Hit();
                sum += DealerCards[position];
            }
            if (sum < playerScore || (sum > 21 && playerScore != 0))
            {
                return 0;
            }
            else if (sum > playerScore)
            {
                return 1;
            }
            return 2;
        }

        private void Hit()
        {
            int i = 0;
            while (DealerCards[i] != 0)
                i++;
            DealerCards[i] = PlayingDecks.GetCard();
        }
    }
}
