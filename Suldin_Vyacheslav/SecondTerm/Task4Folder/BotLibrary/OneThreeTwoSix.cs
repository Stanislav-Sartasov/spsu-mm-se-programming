namespace BotLibrary
{
    public class OneThreeTwoSix : Bot
    {
        public OneThreeTwoSix(int str)
            : base(str)
        {
        }

        public int Step = 0;
        public override void MakeBet(int hand)
        {

            bool win = LastBank < bank;
            int bet;

            if (!win || Step == 4)
                Step = 0;

            if (win && LastBank != 0)
                Step++;


            switch (Step)
            {
                case 0:
                    {
                        bet = Cycle / 2;
                        break;
                    }
                case 1:
                    {
                        bet = Cycle * 3 / 2;
                        break;
                    }
                case 2:
                    {
                        bet = Cycle;
                        break;
                    }
                case 3:
                    {
                        bet = 3 * Cycle;
                        break;
                    }
                default:
                    {
                        bet = Cycle;
                        break;
                    }
            }
            
            if (bet < bank)
            {
            }
            else bet = bank;

            bets[hand] = bet;
            LastBank = bank;
            bank -= bet;

        }
    }
}
