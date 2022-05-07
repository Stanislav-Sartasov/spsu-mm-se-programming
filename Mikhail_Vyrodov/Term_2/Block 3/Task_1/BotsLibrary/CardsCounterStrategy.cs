using System;
using DecksLibrary;
using BlackjackLibrary;

namespace BotsLibrary
{
    public class CardsCounterStrategy : Player, IPlayer
    {
        public CardsCounterStrategy(byte dealersFirst, uint money, Decks playingDecks, uint wager)
            : base(dealersFirst, money, playingDecks, wager)
        {
        }

        public override void Doubled(byte[] hand, byte handFlag = 0)
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
                firstHandDoublingFlag = false;
            }
            else
            {
                secondHandDoublingFlag = false;
            }
            DecisionsCounter += 1;
            firstDecision = false;
            Hit(hand, handFlag);
        }

        public override void Hit(byte[] hand, byte handFlag = 0)
        {
            if (handFlag == 0)
            {
                if (firstHandDoublingFlag)
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
                firstHandDoublingFlag = true;
            }
            else if (handFlag == 1)
            {
                if (secondHandDoublingFlag)
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
                secondHandDoublingFlag = true;
            }
            int i = 0;
            while (hand[i] != 0)
                i++;
            hand[i] = PlayingDecks.GetCard();
            firstDecision = false;
            PlayersTurn(hand, handFlag);
        }
    }
}

