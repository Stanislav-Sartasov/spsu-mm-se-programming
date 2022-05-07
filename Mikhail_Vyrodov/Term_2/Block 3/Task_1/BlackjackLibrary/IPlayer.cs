using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DecksLibrary;

namespace BlackjackLibrary
{
    public interface IPlayer
    {
        public Decks PlayingDecks { get; }
        public Decisions[] PlayersDecisions { get; } // Hit - 1, Split - 2, Doubling - 3, Surrender - 4, for unittesting
        public byte DecisionsCounter { get; }
        public uint FirstSum { get; }
        public uint SecondSum { get; } // If split was implemented
        public uint Money { get; }
        public byte BjFlag { get; } // If player has blackjack. 1 - in first hand. 2 - in second hand. 3 - in both hands
        public bool SurrFlag { get; } // If we decided to surrender. Dealer class uses it.
        public byte[] FirstHand { get; }
        public byte[] SecondHand { get; }
        public uint FirstWager { get; }
        public uint SecondWager { get; }
        public void ClearAttrs();

        public void FillAttrs(byte dealersFirst, uint money, uint wager = 0);

        public void FirstPlayersTurn();

        public void Hit(byte[] hand, byte handFlag = 0);

        public void Doubled(byte[] hand, byte handFlag = 0);

        public void SplitCards();

        public void Surrender();

        public void PlayersTurn(byte[] hand, byte handFlag = 0);

    }
}
