using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DecksLibrary;
using BotsLibrary;

namespace BlackjackLibrary
{
    public class BlackjackGame
    {
        public Decks PlayingCards { get; private set; }

        private Dealer PlayingDealer;
        private Bots strategy;
        private uint initialMoney;
        private int firstWager; // player's first wager after his turn
        private int secondWager; // player's second wager after his turn
        private uint firstAmount; // player's first score after his turn
        private uint secondAmount; // player's second score after his turn
        private bool surrenderFlag;
        private bool notFirstGameFlag = false; // false if this game was first, and 1 if not
        private uint wager = 16; // player's first wager is always 16
        private Player player;

        public BlackjackGame(Decks playingDecks, uint initialMoney, Bots strategy)
        {
            PlayingCards = new Decks();
            PlayingCards = playingDecks;
            this.strategy = strategy;
            this.initialMoney = initialMoney;
            PlayingDealer = new Dealer(playingDecks, 0);
        }
        
        private int CountResult(byte firstResult, byte secondResult)
        {
            int sum = 0;
            if (firstResult == 1)
            {
                firstWager = (-1) * firstWager;
            }
            else if (firstResult == 0)
            {
                firstWager = firstWager * 2;
            }
            if (secondWager != 0)
            {
                if (secondResult == 1)
                {
                    secondWager = (-1) * secondWager;
                }
                else if (firstResult == 0)
                {
                    secondWager = secondWager * 2;
                }
                sum += secondWager;
            }
            sum += firstWager;
            return sum;
        }

        private void ReceiveAttrs()
        {
            player.FirstPlayersTurn();
            firstAmount = player.FirstSum;
            secondAmount = player.SecondSum;
            surrenderFlag = player.SurrFlag;
            PlayingDealer.BjFlag = player.BjFlag;
            player.GetWagers(ref firstWager, ref secondWager);
        }

        private void CreateBot()
        {
            if (strategy == Bots.BasicStrategy)
            {
                player = new Player(PlayingDealer.DealerCards[0], initialMoney, PlayingCards, wager);
                
            }
            else if (strategy == Bots.CardsCounterStrategy)
            {
                player = new CardsCounterStrategy(PlayingDealer.DealerCards[0], initialMoney, PlayingCards, wager);
            }
            else
            {
                player = new SimpleStrategy(PlayingDealer.DealerCards[0], initialMoney, PlayingCards, wager);
            }
            this.ReceiveAttrs();
            notFirstGameFlag = true;
        }

        public int Game()
        {
            int sum;
            byte firstResult, secondResult;
            PlayingDealer.SetFirstCard();
            initialMoney = (uint)(initialMoney - (int)(firstWager + secondWager));
            if (notFirstGameFlag)
            {
                player.FillAttrs(PlayingDealer.DealerCards[0], initialMoney, wager);
                this.ReceiveAttrs();
            }
            else
            {
                this.CreateBot();
            }
            if (surrenderFlag)
            {
                initialMoney += (uint)firstWager;
                player.ClearAttrs();
                return firstWager;
            }
            firstResult = PlayingDealer.DealersTurn(firstAmount);
            secondResult = PlayingDealer.DealersTurn(secondAmount);
            sum = CountResult(firstResult, secondResult);
            initialMoney = (uint)((int)initialMoney + sum);
            player.ClearAttrs();
            return sum;
        }
    }
}
