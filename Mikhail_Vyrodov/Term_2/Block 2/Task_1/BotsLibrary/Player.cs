using System;
using DecksLibrary;

namespace BotsLibrary
{
    public class Player
    {
        protected byte DealerCard;
        public Decks PlayingDecks { get; private set; }
        public Decisions[] PlayersDecisions { get; private set; } // Hit - 1, Split - 2, Doubling - 3, Surrender - 4, for unittesting
        public byte DecisionsCounter { get; protected set; } = 0;
        protected bool FirstHandDoublingFlag = true; // If hit was called from a doubled method in first hand
        protected bool SecondHandDoublingFlag = true; // If hit was called from a doubled method in secjnd hand
        public uint FirstSum { get; private set; }
        public uint SecondSum { get; private set; } // If split was implemented
        public uint Money { get; protected set; }
        protected byte AceFlag = 0; // If player has ace
        public byte BjFlag { get; private set; } = 0; // If player has blackjack. 1 - in first hand. 2 - in second hand. 3 - in both hands
        public uint FirstWager;
        public uint SecondWager;
        public byte SurrFlag { get; private set; } = 0; // If we decided to surrender. Dealer class uses it.
        protected bool FirstDecision = true; // if we haven't made any decisions in player's turn. We can surrender only if it's the first decision
        protected bool IsNotSplitted = true; // Is player hand not splited. We can split the hand only one time.
        public byte[] FirstHand { get; private set; }
        public byte[] SecondHand { get; private set; }

        public void GetWagers(ref int fWager, ref int sWager)
        {
            fWager = (int)FirstWager;
            sWager = (int)SecondWager;
        }

        public Player(byte dealersFirst, uint money, Decks playingDecks, uint wager = 0)
        {
            Money = money;
            FirstWager = wager;
            Money -= FirstWager;
            PlayersDecisions = new Decisions[21];
            PlayingDecks = new Decks();
            PlayingDecks = playingDecks;
            for (int i = 0; i < 21; i++)
            {
                PlayersDecisions[i] = 0;
            }
            DealerCard = dealersFirst;
            FirstHand = new byte[21];
            for (int i = 2; i < 21; i++)
            {
                FirstHand[i] = 0;
            }
            FirstHand[0] = PlayingDecks.GetCard();
            FirstHand[1] = PlayingDecks.GetCard();
            FirstSum = 0;
            SecondSum = 0;
        }

        protected virtual void Hit(byte[] hand, byte handFlag = 0)
        {
            int i = 0;
            while (hand[i] != 0)
                i++;
            hand[i] = PlayingDecks.GetCard();
            FirstDecision = false;
            if ((handFlag == 0 && FirstHandDoublingFlag) || (handFlag == 1 && SecondHandDoublingFlag))
            {
                PlayersDecisions[DecisionsCounter] = Decisions.Hitting;
                DecisionsCounter += 1;
            }
            PlayersTurn(hand, handFlag);
        }

        protected virtual void Doubled(byte[] hand, byte handFlag = 0)
        {
            if (handFlag == 0)
            {
                Money -= FirstWager;
                FirstWager = FirstWager * 2;
                FirstHandDoublingFlag = false;
            }
            else
            {
                Money -= SecondWager;
                SecondWager = SecondWager * 2;
                SecondHandDoublingFlag = false;
            }
            FirstDecision = false;
            PlayersDecisions[DecisionsCounter] = Decisions.Doubling;
            DecisionsCounter += 1;
            Hit(hand, handFlag);
        }

        protected void SplitCards()
        {
            SecondHand = new byte[21];
            for (int i = 1; i < 21; i++)
            {
                SecondHand[i] = 0;
            }
            SecondHand[0] = FirstHand[1];
            FirstHand[1] = 0;
            SecondWager = FirstWager;
            FirstDecision = false;
            Money -= SecondWager;
            PlayersDecisions[DecisionsCounter] = Decisions.Splitting;
            DecisionsCounter += 1;
            PlayersTurn(FirstHand, 0);
            PlayersTurn(SecondHand, 1);
            IsNotSplitted = false;
        }

        protected void Surrender()
        {
            FirstWager /= 2;
            Money += FirstWager;
            SurrFlag = 1;
            PlayersDecisions[DecisionsCounter] = Decisions.Surrendering;
            DecisionsCounter += 1;
        }

        public void PlayersTurn(byte[] hand, byte handFlag = 0)
        {
            uint sum = 0;
            AceFlag = 0;
            for (int i = 0; i < 21; i++)
            {
                sum += hand[i];
                if (hand[i] == 1)
                {
                    AceFlag = 1;
                }
            }
            if (AceFlag == 1 && sum + 10 <= 21)
            {
                sum += 10;
            }
            if (handFlag == 0)
            {
                FirstSum = sum;
            }
            else
            {
                SecondSum = sum;
            }
            if (sum == 21 && hand[2] == 0)
            {
                if (BjFlag > 0)
                    BjFlag = 3;
                    
                else if (handFlag == 0)
                    BjFlag = 1;
                else if (handFlag == 1)
                    BjFlag = 2;
            }
            if (sum > 21)
            {
                sum = 0;
                if (handFlag == 0)
                    FirstSum = sum;
                else
                    SecondSum = sum;
                return;
            }
            if (hand[1] == 0)
            {
                Hit(hand, handFlag);
                return;
            }
            if (hand[2] == 0)
            {
                if (((hand[0] == 1 && hand[1] == 8) || (hand[1] == 1 && hand[0] == 8)) && DealerCard == 6)
                {
                    Doubled(hand, handFlag);
                    return;
                }
                else if ((hand[0] == 1 && hand[1] == 6) || (hand[1] == 1 && hand[0] == 6))
                {
                    if (DealerCard >= 3 && DealerCard <= 6)
                    {
                        Doubled(hand, handFlag);
                    }
                    else
                    {
                        Hit(hand, handFlag);
                    }
                    return;
                }
                else if (((hand[0] == 1 && hand[1] == 7) || (hand[1] == 7 && hand[0] == 1)) &&
                        (DealerCard >= 2 && DealerCard <= 6))
                {
                    Doubled(hand, handFlag);
                    return;
                }
                else if (((hand[0] == 1 && hand[1] == 7) || (hand[1] == 7 && hand[0] == 1)) &&
                        (DealerCard == 9 || DealerCard == 10 || DealerCard == 1))
                {
                    Hit(hand, handFlag);
                    return;
                }
                else if ((hand[0] == 1 && (hand[1] == 4 || hand[1] == 5)) || (hand[1] == 1 && (hand[0] == 4 || hand[0] == 5)))
                {
                    if (DealerCard == 4 || DealerCard == 5 || DealerCard == 6)
                    {
                        Doubled(hand, handFlag);
                    }
                    else
                    {
                        Hit(hand, handFlag);
                    }
                    return;
                }
                else if ((hand[0] == 1 && (hand[1] == 2 || hand[1] == 3)) || (hand[1] == 1 && (hand[0] == 2 || hand[0] == 3)))
                {
                    if (DealerCard == 5 || DealerCard == 6)
                    {
                        Doubled(hand, handFlag);
                    }
                    else
                    {
                        Hit(hand, handFlag);
                    }
                    return;
                }
                else if (hand[0] == 1 && hand[1] == 1 && IsNotSplitted)
                {
                    SplitCards();
                    return;
                }
                else if ((hand[0] == 9 && hand[1] == 9) && (DealerCard != 7 || DealerCard != 10 || DealerCard != 1) && IsNotSplitted)
                {
                    SplitCards();
                    return;
                }
                else if ((hand[0] == 8 && hand[1] == 8) && DealerCard != 1 && IsNotSplitted)
                {
                    SplitCards();
                    return;
                }
                else if ((hand[0] == 8 && hand[1] == 8) && DealerCard == 1 && FirstDecision)
                {
                    Surrender();
                    return;
                }
                else if (hand[0] == 7 && hand[1] == 7)
                {
                    if ((DealerCard == 2 || DealerCard == 3 || DealerCard == 4 || DealerCard == 5 || DealerCard == 6) && IsNotSplitted)
                        SplitCards();
                    else
                        Hit(hand, handFlag);
                    return;
                }
                else if (hand[0] == 5 && hand[1] == 5)
                {
                    if (DealerCard == 10 || DealerCard == 1)
                        Hit(hand, handFlag);
                    else
                        Doubled(hand, handFlag);
                    return;
                }
                else if (hand[0] == 4 && hand[1] == 4)
                {
                    if ((DealerCard == 5 || DealerCard == 6) && IsNotSplitted)
                        SplitCards();
                    else
                        Doubled(hand, handFlag);
                    return;
                }
                else if ((hand[0] == 2 && hand[1] == 2) || (hand[0] == 3 && hand[1] == 3) || (hand[1] == 2 && hand[0] == 2) || (hand[1] == 3 && hand[0] == 3))
                {
                    if ((DealerCard != 8 && DealerCard != 9 && DealerCard != 10 && DealerCard != 1) && IsNotSplitted)
                        SplitCards();
                    else
                        Hit(hand, handFlag);
                    return;
                }
            }
            if (sum >= 5 && sum <= 8)
            {
                Hit(hand, handFlag);
            }
            else if (sum == 9)
            {
                if (DealerCard >= 3 && DealerCard <= 6)
                    Doubled(hand, handFlag);
                else
                    Hit(hand, handFlag);
            }
            else if (sum == 10)
            {
                if (DealerCard != 1 || DealerCard != 10)
                    Doubled(hand, handFlag);
                else
                    Hit(hand, handFlag);
            }
            else if (sum == 11)
            {
                Doubled(hand, handFlag);
            }
            else if (sum == 12 && (DealerCard != 4 || DealerCard != 5 || DealerCard != 6))
            {
                Hit(hand, handFlag);
            }
            else if ((sum == 13 || sum == 14) && (DealerCard == 7 || DealerCard == 8 || DealerCard == 9 || DealerCard == 10 || DealerCard == 1))
            {
                Hit(hand, handFlag);
            }
            else if (sum == 15)
            {
                if (DealerCard == 7 || DealerCard == 8 || DealerCard == 9)
                    Hit(hand, handFlag);
                else if ((DealerCard == 10 || DealerCard == 1) && FirstDecision)
                    Surrender();
            }
            else if (sum == 16)
            {
                if (DealerCard == 7 || DealerCard == 8)
                    Hit(hand, handFlag);
                else if ((DealerCard == 10 || DealerCard == 1 || DealerCard == 9) && FirstDecision)
                    Surrender();
            }
            else if (sum == 17 && DealerCard == 1 && FirstDecision)
            {
                Surrender();
            }
        }
    }
}
