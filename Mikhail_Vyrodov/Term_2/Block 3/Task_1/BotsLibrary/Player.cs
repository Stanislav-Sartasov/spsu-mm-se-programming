using System;
using DecksLibrary;

namespace BotsLibrary
{
    public class Player
    {
        public Decks PlayingDecks { get; private set; }
        public Decisions[] PlayersDecisions { get; private set; } // Hit - 1, Split - 2, Doubling - 3, Surrender - 4, for unittesting
        public byte DecisionsCounter { get; protected set; } = 0;
        public uint FirstSum { get; private set; }
        public uint SecondSum { get; private set; } // If split was implemented
        public uint Money { get; protected set; }
        public byte BjFlag { get; private set; } = 0; // If player has blackjack. 1 - in first hand. 2 - in second hand. 3 - in both hands
        public bool SurrFlag { get; private set; } = false; // If we decided to surrender. Dealer class uses it.
        public byte[] FirstHand { get; private set; }
        public byte[] SecondHand { get; private set; }
        public uint FirstWager { get; protected set; }
        public uint SecondWager { get; protected set; }

        protected byte dealerCard;
        protected bool firstHandDoublingFlag = true; // If hit was called from a doubled method in first hand. This is made for better counting decisions
        protected bool secondHandDoublingFlag = true; // If hit was called from a doubled method in second hand. This is made for better counting decisions
        protected bool aceFlag = false; // If player has ace
        protected bool firstDecision = true; // if we haven't made any decisions in player's turn. We can surrender only if it's the first decision
        protected bool isNotSplitted = true; // Is player hand not splited. We can split the hand only one time.

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
            dealerCard = dealersFirst;
            FirstHand = new byte[21];
            for (int i = 2; i < 21; i++)
            {
                FirstHand[i] = 0;
            }
            SecondHand = new byte[21];
            for (int i = 0; i < 21; i++)
            {
                SecondHand[i] = 0;
            }
            FirstHand[0] = PlayingDecks.GetCard();
            FirstHand[1] = PlayingDecks.GetCard();
            FirstSum = 0;
            SecondSum = 0;
        }

        public void ClearAttrs()
        {
            for (int i = 0; i < 21; i++)
            {
                PlayersDecisions[i] = 0;
            }
            for (int i = 2; i < 21; i++)
            {
                FirstHand[i] = 0;
            }
            for (int i = 0; i < 21; i++)
            {
                SecondHand[i] = 0;
            }
            FirstSum = 0;
            SecondSum = 0;
            firstDecision = true;
            aceFlag = false;
            isNotSplitted = true;
            firstHandDoublingFlag = true;
            secondHandDoublingFlag = true;
            DecisionsCounter = 0;
            BjFlag = 0;
            SurrFlag = false;
        }

        public void FillAttrs(byte dealersFirst, uint money, uint wager = 0)
        {
            Money = money;
            FirstWager = wager;
            Money -= FirstWager;
            dealerCard = dealersFirst;
            FirstHand[0] = PlayingDecks.GetCard();
            FirstHand[1] = PlayingDecks.GetCard();
        }

        public void FirstPlayersTurn()
        {
            PlayersTurn(FirstHand);
        }

        protected virtual void Hit(byte[] hand, byte handFlag = 0)
        {
            int i = 0;
            while (hand[i] != 0)
                i++;
            hand[i] = PlayingDecks.GetCard();
            firstDecision = false;
            if ((handFlag == 0 && firstHandDoublingFlag) || (handFlag == 1 && secondHandDoublingFlag))
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
                firstHandDoublingFlag = false;
            }
            else
            {
                Money -= SecondWager;
                SecondWager = SecondWager * 2;
                secondHandDoublingFlag = false;
            }
            firstDecision = false;
            PlayersDecisions[DecisionsCounter] = Decisions.Doubling;
            DecisionsCounter += 1;
            Hit(hand, handFlag);
        }

        protected void SplitCards()
        {
            SecondHand[0] = FirstHand[1];
            FirstHand[1] = 0;
            SecondWager = FirstWager;
            firstDecision = false;
            Money -= SecondWager;
            PlayersDecisions[DecisionsCounter] = Decisions.Splitting;
            DecisionsCounter += 1;
            PlayersTurn(FirstHand, 0);
            PlayersTurn(SecondHand, 1);
            isNotSplitted = false;
        }

        protected void Surrender()
        {
            FirstWager /= 2;
            Money += FirstWager;
            SurrFlag = true;
            PlayersDecisions[DecisionsCounter] = Decisions.Surrendering;
            DecisionsCounter += 1;
        }

        // handFlag = 0 if we play with first hand, 1 if we play with second hand.
        // It is made to make correct wagers on both hands.
        public void PlayersTurn(byte[] hand, byte handFlag = 0)
        {
            uint sum = 0;
            aceFlag = false;
            for (int i = 0; i < 21; i++)
            {
                sum += hand[i];
                if (hand[i] == 1)
                {
                    aceFlag = true;
                }
            }
            if (aceFlag && sum + 10 <= 21)
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
                if (((hand[0] == 1 && hand[1] == 8) || (hand[1] == 1 && hand[0] == 8)) && dealerCard == 6)
                {
                    Doubled(hand, handFlag);
                    return;
                }
                else if ((hand[0] == 1 && hand[1] == 6) || (hand[1] == 1 && hand[0] == 6))
                {
                    if (dealerCard >= 3 && dealerCard <= 6)
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
                        (dealerCard >= 2 && dealerCard <= 6))
                {
                    Doubled(hand, handFlag);
                    return;
                }
                else if (((hand[0] == 1 && hand[1] == 7) || (hand[1] == 7 && hand[0] == 1)) &&
                        (dealerCard == 9 || dealerCard == 10 || dealerCard == 1))
                {
                    Hit(hand, handFlag);
                    return;
                }
                else if ((hand[0] == 1 && (hand[1] == 4 || hand[1] == 5)) || (hand[1] == 1 && (hand[0] == 4 || hand[0] == 5)))
                {
                    if (dealerCard == 4 || dealerCard == 5 || dealerCard == 6)
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
                    if (dealerCard == 5 || dealerCard == 6)
                    {
                        Doubled(hand, handFlag);
                    }
                    else
                    {
                        Hit(hand, handFlag);
                    }
                    return;
                }
                else if (hand[0] == 1 && hand[1] == 1 && isNotSplitted)
                {
                    SplitCards();
                    return;
                }
                else if ((hand[0] == 9 && hand[1] == 9) && (dealerCard != 7 || dealerCard != 10 || dealerCard != 1) && isNotSplitted)
                {
                    SplitCards();
                    return;
                }
                else if ((hand[0] == 8 && hand[1] == 8) && dealerCard != 1 && isNotSplitted)
                {
                    SplitCards();
                    return;
                }
                else if ((hand[0] == 8 && hand[1] == 8) && dealerCard == 1 && firstDecision)
                {
                    Surrender();
                    return;
                }
                else if (hand[0] == 7 && hand[1] == 7)
                {
                    if ((dealerCard == 2 || dealerCard == 3 || dealerCard == 4 || dealerCard == 5 || dealerCard == 6) && isNotSplitted)
                        SplitCards();
                    else
                        Hit(hand, handFlag);
                    return;
                }
                else if (hand[0] == 5 && hand[1] == 5)
                {
                    if (dealerCard == 10 || dealerCard == 1)
                        Hit(hand, handFlag);
                    else
                        Doubled(hand, handFlag);
                    return;
                }
                else if (hand[0] == 4 && hand[1] == 4)
                {
                    if ((dealerCard == 5 || dealerCard == 6) && isNotSplitted)
                        SplitCards();
                    else
                        Doubled(hand, handFlag);
                    return;
                }
                else if ((hand[0] == 2 && hand[1] == 2) || (hand[0] == 3 && hand[1] == 3) || (hand[1] == 2 && hand[0] == 2) || (hand[1] == 3 && hand[0] == 3))
                {
                    if ((dealerCard != 8 && dealerCard != 9 && dealerCard != 10 && dealerCard != 1) && isNotSplitted)
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
                if (dealerCard >= 3 && dealerCard <= 6)
                    Doubled(hand, handFlag);
                else
                    Hit(hand, handFlag);
            }
            else if (sum == 10)
            {
                if (dealerCard != 1 || dealerCard != 10)
                    Doubled(hand, handFlag);
                else
                    Hit(hand, handFlag);
            }
            else if (sum == 11)
            {
                Doubled(hand, handFlag);
            }
            else if (sum == 12 && (dealerCard != 4 || dealerCard != 5 || dealerCard != 6))
            {
                Hit(hand, handFlag);
            }
            else if ((sum == 13 || sum == 14) && (dealerCard == 7 || dealerCard == 8 || dealerCard == 9 || dealerCard == 10 || dealerCard == 1))
            {
                Hit(hand, handFlag);
            }
            else if (sum == 15)
            {
                if (dealerCard == 7 || dealerCard == 8 || dealerCard == 9)
                    Hit(hand, handFlag);
                else if ((dealerCard == 10 || dealerCard == 1) && firstDecision)
                    Surrender();
            }
            else if (sum == 16)
            {
                if (dealerCard == 7 || dealerCard == 8)
                    Hit(hand, handFlag);
                else if ((dealerCard == 10 || dealerCard == 1 || dealerCard == 9) && firstDecision)
                    Surrender();
            }
            else if (sum == 17 && dealerCard == 1 && firstDecision)
            {
                Surrender();
            }
        }
    }
}
