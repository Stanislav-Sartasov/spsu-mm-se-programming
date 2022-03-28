using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractClasses
{
    public abstract partial class ABot : AParticipant
    {
        public double Money;
        public double Multiplier = 1;
        public double Rate;
        public bool IsWantNextCard = false;
        public bool IsWantNextGame = false;
        public bool IsWonLastGame = false;
        public Strategy BotStrategy;

        public ABot(double money)
        {
            Money = money;
        }

        private void CheckIsWantNextGame()
        {
            IsWantNextGame = CountGames <= 40 && Money > 0;
        }

        protected abstract void PrepareToNextGame();

        public override void Win()
        {
            Money += Rate * Multiplier;
            CountGames++;
            CountWinGames++;
            Multiplier = 1;
            IsWonLastGame = true;
            CheckIsWantNextGame();
            PrepareToNextGame();
        }

        public override void Lose()
        {
            Money -= Rate;
            CountGames++;
            IsWonLastGame = false;
            CheckIsWantNextGame();
            PrepareToNextGame();
        }
    }
}
