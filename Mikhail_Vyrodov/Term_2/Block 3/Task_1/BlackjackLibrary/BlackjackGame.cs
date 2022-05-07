using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DecksLibrary;
using System.Reflection;

namespace BlackjackLibrary
{
    public class BlackjackGame
    {
        public Decks PlayingCards { get; private set; }

        public Dealer PlayingDealer { get; private set; }
        public uint InitialMoney { get; private set; }

        private int firstWager; // player's first wager after his turn
        private int secondWager; // player's second wager after his turn
        private uint firstAmount; // player's first score after his turn
        private uint secondAmount; // player's second score after his turn
        private bool surrenderFlag;
        private bool notFirstGameFlag = false; // false if this game was first, and 1 if not
        private uint wager = 16; // player's first wager is always 16
        public IPlayer Player;

        public BlackjackGame(Decks playingDecks, uint initialMoney)
        {
            PlayingCards = new Decks();
            PlayingCards = playingDecks;
            this.InitialMoney = initialMoney;
            PlayingDealer = new Dealer(playingDecks, 0);
            PlayingDealer.SetFirstCard();
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
            Player.FirstPlayersTurn();
            firstAmount = Player.FirstSum;
            secondAmount = Player.SecondSum;
            surrenderFlag = Player.SurrFlag;
            PlayingDealer.BjFlag = Player.BjFlag;
            firstWager = (int)Player.FirstWager;
            secondWager = (int)Player.SecondWager;
        }

        private void CreateBot()
        {
            this.ReceiveAttrs();
            notFirstGameFlag = true;
        }

        public int Game()
        {
            int sum;
            byte firstResult, secondResult;
            if (notFirstGameFlag)
            {
                PlayingDealer.SetFirstCard();
                InitialMoney = (uint)(InitialMoney - (int)(firstWager + secondWager));
                Player.FillAttrs(PlayingDealer.DealerCards[0], InitialMoney, wager);
                this.ReceiveAttrs();
            }
            else
            {
                this.CreateBot();
            }
            if (surrenderFlag)
            {
                InitialMoney += (uint)firstWager;
                Player.ClearAttrs();
                return firstWager;
            }
            firstResult = PlayingDealer.DealersTurn(firstAmount);
            secondResult = PlayingDealer.DealersTurn(secondAmount);
            sum = CountResult(firstResult, secondResult);
            InitialMoney = (uint)((int)InitialMoney + sum);
            Player.ClearAttrs();
            return sum;
        }
    }
}
