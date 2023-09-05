using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BotStructure
{
    public abstract class Bot : IBot
    {
        public string Name { get; }
        public int Balance { get; protected set; }
        public int Gain { get; set; }
        public int GameCounter { get; set; }

        public BotState State { get; set; }

        public Bot(string name, int balance, int counter)
        {
            Name = name;
            Balance = balance;
            GameCounter = counter;
            Gain = 0;
            State = BotState.Play;
        }
        public void MakeBet(int bet)
        {

        }


    }
}
