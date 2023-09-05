using System;
using DecksLibrary;
using BlackjackLibrary;

namespace BotsLibrary
{
    public class SimpleStrategy : Player, IPlayer
    {
        public SimpleStrategy(byte dealersFirst, uint money, Decks playingDecks, uint wager) :
            base(dealersFirst, money, playingDecks, wager)
        {
        }

        public override void Hit(byte[] hand, byte handFlag = 0)
        {
            uint sum = 0;
            aceFlag = false;
            for (uint counter = 0; counter < 21; counter++)
            {
                sum += hand[counter];
                if (hand[counter] == 1)
                {
                    aceFlag = true;
                }
            }
            if (aceFlag && sum + 10 <= 21)
            {
                sum += 10;
            }
            if (handFlag == 0 && firstHandDoublingFlag)
            {
                PlayersDecisions[DecisionsCounter] = Decisions.Hitting;
                if (sum <= 10)
                {
                    PlayersDecisions[DecisionsCounter] = Decisions.Doubling;
                    if (Money - FirstWager >= 0)
                    {
                        Money -= FirstWager;
                        FirstWager = FirstWager * 2;
                    }
                }
                DecisionsCounter += 1;
                firstHandDoublingFlag = true;
            }
            else if (handFlag == 1 && secondHandDoublingFlag)
            {
                PlayersDecisions[DecisionsCounter] = Decisions.Hitting;
                if (sum <= 10)
                {
                    PlayersDecisions[DecisionsCounter] = Decisions.Doubling;
                    if (Money - SecondWager >= 0)
                    {
                        Money -= SecondWager;
                        SecondWager = SecondWager * 2;
                    }
                }
                DecisionsCounter += 1;
                secondHandDoublingFlag = true;
            }
            int i = 0;
            while (hand[i] != 0)
                i++;
            hand[i] = PlayingDecks.GetCard();
            firstDecision = false;
            PlayersTurn(hand, handFlag);
        }

        public override void Doubled(byte[] hand, byte handFlag = 0)
        {
            PlayersDecisions[DecisionsCounter] = Decisions.Hitting;
            uint sum = 0;
            aceFlag = false;
            for (uint counter = 0; counter < 21; counter++)
            {
                sum += hand[counter];
                if (hand[counter] == 1)
                {
                    aceFlag = true;
                }
            }
            if (aceFlag && sum + 10 <= 21)
            {
                sum += 10;
            }
            if (sum <= 15)
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
    }
}
