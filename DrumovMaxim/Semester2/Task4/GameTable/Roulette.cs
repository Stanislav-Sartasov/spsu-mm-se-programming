using GameTable.BotStructure;
using GameTable.SectorTypeEnum;
using GameTable.BetsType;

namespace GameTable
{
    public class Roulette
    {
        public SectorType[] Sectors;
        public IBot bot { get; private set; }    
        public List<Bet> bets { get; private set; }
        private Random rand;

        public Roulette(IBot bot)
        {
            Sectors = new SectorType[37]
            { 
                new SectorType(0, null, null, null),
                new SectorType(32, ColourEnum.Red, ParityEnum.Even, DozenEnum.ThirdDozen),
                new SectorType(15, ColourEnum.Black, ParityEnum.Odd, DozenEnum.SecondDozen),
                new SectorType(19, ColourEnum.Red, ParityEnum.Odd, DozenEnum.SecondDozen),
                new SectorType(4, ColourEnum.Black, ParityEnum.Even, DozenEnum.FirstDozen),
                new SectorType(21, ColourEnum.Red, ParityEnum.Odd, DozenEnum.SecondDozen),
                new SectorType(2, ColourEnum.Black, ParityEnum.Even, DozenEnum.FirstDozen),
                new SectorType(25, ColourEnum.Red, ParityEnum.Odd, DozenEnum.ThirdDozen),
                new SectorType(17, ColourEnum.Black, ParityEnum.Odd, DozenEnum.SecondDozen),
                new SectorType(34, ColourEnum.Red, ParityEnum.Even, DozenEnum.ThirdDozen),
                new SectorType(6, ColourEnum.Black, ParityEnum.Even, DozenEnum.FirstDozen),
                new SectorType(27, ColourEnum.Red, ParityEnum.Odd, DozenEnum.ThirdDozen),
                new SectorType(13, ColourEnum.Black, ParityEnum.Odd, DozenEnum.SecondDozen),
                new SectorType(36, ColourEnum.Red, ParityEnum.Even, DozenEnum.ThirdDozen),
                new SectorType(11, ColourEnum.Black, ParityEnum.Odd, DozenEnum.FirstDozen),
                new SectorType(30, ColourEnum.Red, ParityEnum.Even, DozenEnum.ThirdDozen),
                new SectorType(8, ColourEnum.Black, ParityEnum.Even, DozenEnum.FirstDozen),
                new SectorType(23, ColourEnum.Red, ParityEnum.Odd, DozenEnum.SecondDozen),
                new SectorType(10, ColourEnum.Black, ParityEnum.Even, DozenEnum.FirstDozen),
                new SectorType(5, ColourEnum.Red, ParityEnum.Odd, DozenEnum.FirstDozen),
                new SectorType(24, ColourEnum.Black, ParityEnum.Even, DozenEnum.SecondDozen),
                new SectorType(16, ColourEnum.Red, ParityEnum.Even, DozenEnum.SecondDozen),
                new SectorType(33, ColourEnum.Black, ParityEnum.Odd, DozenEnum.ThirdDozen),
                new SectorType(1, ColourEnum.Red, ParityEnum.Odd, DozenEnum.FirstDozen),
                new SectorType(20, ColourEnum.Black, ParityEnum.Even, DozenEnum.SecondDozen),
                new SectorType(14, ColourEnum.Red, ParityEnum.Even, DozenEnum.SecondDozen),
                new SectorType(31, ColourEnum.Black, ParityEnum.Odd, DozenEnum.ThirdDozen),
                new SectorType(9, ColourEnum.Red, ParityEnum.Odd, DozenEnum.FirstDozen),
                new SectorType(22, ColourEnum.Black, ParityEnum.Even, DozenEnum.SecondDozen),
                new SectorType(18, ColourEnum.Red, ParityEnum.Even, DozenEnum.SecondDozen),
                new SectorType(29, ColourEnum.Black, ParityEnum.Odd, DozenEnum.ThirdDozen),
                new SectorType(7, ColourEnum.Red, ParityEnum.Odd, DozenEnum.FirstDozen),
                new SectorType(28, ColourEnum.Black, ParityEnum.Even, DozenEnum.ThirdDozen),
                new SectorType(12, ColourEnum.Red, ParityEnum.Even, DozenEnum.FirstDozen),
                new SectorType(35, ColourEnum.Black, ParityEnum.Odd, DozenEnum.ThirdDozen),
                new SectorType(3, ColourEnum.Red, ParityEnum.Odd, DozenEnum.FirstDozen),
                new SectorType(26, ColourEnum.Black, ParityEnum.Even, DozenEnum.ThirdDozen)
            };
            this.bot = bot; 
            rand = new Random();
        }

        public void TableVisualization()
        {
            Console.WriteLine("");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("       {0}       |", Sectors[0].Number);
            for (int i = 1; i < Sectors.Length; i++)
            {
                int temp;
                for (int j = 1; j < Sectors.Length; j++)
                {
                    if (i == Sectors[j].Number)
                    {
                        temp = j;
                        int number = Sectors[temp].Number;
                        if (Sectors[temp].Colour == ColourEnum.Red)
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            if (Sectors[temp].Number % 3 == 0 && Sectors[temp].Number <= 9)
                            {
                                Console.WriteLine($"|{number}   |");
                            }
                            else if (Sectors[temp].Number <= 9)
                            {
                                Console.Write($"|{number}   ");
                            }
                            else if (Sectors[temp].Number % 3 == 0)
                            {
                                Console.WriteLine($"|{number}  |");
                            }
                            else
                            {
                                Console.Write($"|{number}  ");
                            }
                            Console.ResetColor();
                        }
                        else if (Sectors[temp].Colour == ColourEnum.Black)
                        {
                            Console.ForegroundColor = ConsoleColor.DarkGray;

                            if (Sectors[temp].Number % 3 == 0 && Sectors[temp].Number <= 9)
                            {
                                Console.WriteLine($"|{number}   |");
                            }
                            else if (Sectors[temp].Number <= 9)
                            {
                                Console.Write($"|{number}   ");
                            }
                            else if (Sectors[temp].Number % 3 == 0)
                            {
                                Console.WriteLine($"|{number}  |");
                            }
                            else
                            {
                                Console.Write($"|{number}  ");
                            }
                            Console.ResetColor();
                        }
                    }
                }
            }
            Console.WriteLine("");
        }
            
        public void Game()
        {
            Console.WriteLine("\nRoulette Table:");
            TableVisualization();
            while(bot.State == BotState.Play)
            {
                Play();
            }
        }

        public void Play()
        {
            bets = new List<Bet>();
            MakeBets();

            if (bot.State == BotState.Stop)
            {
                return;
            }

            LaunchGame();
        }

        public void MakeBets()
        {
            bets = bot.NewBet(bot.MinBet);
            bot.GameCounter--;
        }

        public void LaunchGame()
        {
            int random = rand.Next(37);
            SectorType temp = new SectorType(Sectors[random].Number, Sectors[random].Colour, Sectors[random].Parity, Sectors[random].Dozen);
            
            for(int i = 0; i < bets.Count; i++)
            {
                int coef = bets[i].ReviewBet(temp);

                if (coef > 0)
                {
                    bot.LastGame = true;
                    bot.WinsCounter++;
                }
                else
                {
                    bot.LastGame = false;
                }
                
                bot.Balance += bets[i].value * coef;
            }

        }

    }
}
