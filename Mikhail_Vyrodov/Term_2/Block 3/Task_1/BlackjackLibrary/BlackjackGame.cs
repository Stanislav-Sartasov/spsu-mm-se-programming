using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DecksLibrary;
using System.Reflection;
using PluginLibrary;

namespace BlackjackLibrary
{
    public class BlackjackGame
    {
        public Decks PlayingCards { get; private set; }

        private Dealer playingDealer;
        private Bots strategy;
        private uint initialMoney;
        private int firstWager; // player's first wager after his turn
        private int secondWager; // player's second wager after his turn
        private uint firstAmount; // player's first score after his turn
        private uint secondAmount; // player's second score after his turn
        private bool surrenderFlag;
        private bool notFirstGameFlag = false; // false if this game was first, and 1 if not
        private uint wager = 16; // player's first wager is always 16
        private PluginHelper pluginHelper;

        public BlackjackGame(Decks playingDecks, uint initialMoney, Bots strategy)
        {
            PlayingCards = new Decks();
            PlayingCards = playingDecks;
            this.strategy = strategy;
            this.initialMoney = initialMoney;
            playingDealer = new Dealer(playingDecks, 0);
            LibraryLoader libraryLoader = new LibraryLoader("../../../../BotsLibrary/BotsLibrary.dll");
            Assembly asm = libraryLoader.LoadLibrary();
            pluginHelper = new PluginHelper(asm);
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
            pluginHelper.ImplementMethod("FirstPlayersTurn");
            firstAmount = (uint)pluginHelper.ReceiveProperty("FirstSum");
            secondAmount = (uint)pluginHelper.ReceiveProperty("SecondSum");
            surrenderFlag = (bool)pluginHelper.ReceiveProperty("SurrFlag");
            playingDealer.BjFlag = (byte)pluginHelper.ReceiveProperty("BjFlag");
            uint uintFirstWager = (uint)pluginHelper.ReceiveProperty("FirstWager");
            uint uintSecondWager = (uint)pluginHelper.ReceiveProperty("SecondWager");
            firstWager = (int)uintFirstWager;
            secondWager = (int)uintSecondWager;
        }

        private void CreateBot()
        {
            object[] parameters = new object[] { playingDealer.DealerCards[0], initialMoney, PlayingCards, wager };
            if (strategy == Bots.BasicStrategy)
            {
                pluginHelper.CreatePlayer("Player", parameters);
            }
            else if (strategy == Bots.CardsCounterStrategy)
            {
                pluginHelper.CreatePlayer("CardsCounterStrategy", parameters);
            }
            else
            {
                pluginHelper.CreatePlayer("SimpleStrategy", parameters);
            }
            this.ReceiveAttrs();
            notFirstGameFlag = true;
        }

        public int Game()
        {
            int sum;
            byte firstResult, secondResult;
            playingDealer.SetFirstCard();
            initialMoney = (uint)(initialMoney - (int)(firstWager + secondWager));
            if (notFirstGameFlag)
            {
                object[] parameters = new object[] { playingDealer.DealerCards[0], initialMoney, wager };
                pluginHelper.ImplementMethod("FillAttrs", parameters);
                this.ReceiveAttrs();
            }
            else
            {
                this.CreateBot();
            }
            if (surrenderFlag)
            {
                initialMoney += (uint)firstWager;
                pluginHelper.ImplementMethod("ClearAttrs");
                return firstWager;
            }
            firstResult = playingDealer.DealersTurn(firstAmount);
            secondResult = playingDealer.DealersTurn(secondAmount);
            sum = CountResult(firstResult, secondResult);
            initialMoney = (uint)((int)initialMoney + sum);
            pluginHelper.ImplementMethod("ClearAttrs");
            return sum;
        }
    }
}
