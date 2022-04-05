using System;
using DecksLibrary;

namespace BotsLibrary
{
    public class CardsCounterStrategy : Player
    {
        public CardsCounterStrategy(byte dealersFirst, uint money, Decks playingDecks, uint wager)
            : base(dealersFirst, money, playingDecks, wager)
        {
        }

        protected override void Doubled(byte[] hand, byte handFlag = 0)
        {
            PlayersDecisions[DecisionsCounter] = Decisions.Hitting;
            if (PlayingDecks.CountOfCards >= -2)
            {
                if (handFlag == 0)
                {
                    if (Money - FirstWager >= 0)
                    {
                        Money -= FirstWager;
                        FirstWager = FirstWager * 2;
                    }
                }
                else
                {
                    if (Money - SecondWager >= 0)
                    {
                        Money -= SecondWager;
                        SecondWager = SecondWager * 2;
                    }
                }
                PlayersDecisions[DecisionsCounter] = Decisions.Doubling;
            }
            if (handFlag == 0)
            {
                FirstHandDoublingFlag = false;
            }
            else
            {
                SecondHandDoublingFlag = false;
            }
            DecisionsCounter += 1;
            FirstDecision = false;
            Hit(hand, handFlag);
        }

        protected override void Hit(byte[] hand, byte handFlag = 0)
        {
            if (handFlag == 0)
            {
                if (FirstHandDoublingFlag)
                {
                    PlayersDecisions[DecisionsCounter] = Decisions.Hitting;
                    if (PlayingDecks.CountOfCards >= 2.0)
                    {
                        PlayersDecisions[DecisionsCounter] = Decisions.Doubling;
                        if (Money - FirstWager >= 0)
                        {
                            Money -= FirstWager;
                            FirstWager = FirstWager * 2;
                        }
                    }
                    DecisionsCounter += 1;
                }
                FirstHandDoublingFlag = true;
            }
            else if (handFlag == 1)
            {
                if (SecondHandDoublingFlag)
                {
                    PlayersDecisions[DecisionsCounter] = Decisions.Hitting;
                    if (PlayingDecks.CountOfCards >= 2.0)
                    {
                        PlayersDecisions[DecisionsCounter] = Decisions.Doubling;
                        if (Money - SecondWager >= 0)
                        {
                            Money -= SecondWager;
                            SecondWager = SecondWager * 2;
                        }
                    }
                    DecisionsCounter += 1;
                }
                SecondHandDoublingFlag = true;
            }
            int i = 0;
            while (hand[i] != 0)
                i++;
            hand[i] = PlayingDecks.GetCard();
            FirstDecision = false;
            PlayersTurn(hand, handFlag);
        }
    }
}

