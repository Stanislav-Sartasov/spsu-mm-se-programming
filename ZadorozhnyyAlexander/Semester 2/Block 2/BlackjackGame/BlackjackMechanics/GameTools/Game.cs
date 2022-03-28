using AbstractClasses;
using BlackjackBots;
using BlackjackMechanics.Cards;
using BlackjackMechanics.Players;

namespace BlackjackMechanics.GameTools
{
    public partial class Game
    {
        private int NumberOfMoves = 0;
        private DeckOfCards Deck;
        private AParticipant User;
        private Dealer Dealer;

        public Game(AParticipant player)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            User = player;
            Dealer = new Dealer();
            Dealer.CardsInHand = new List<ACard>();
        }

        public void CreateGame(int countDecks)
        {
            Deck = new DeckOfCards(countDecks);
            Deck.ShuffleDeck();
            Dealer.HandOutCards(Deck, User);
        }

        public void StartGame()
        {
            if (User.GetType() == typeof(Player))
            {
                WriteGreetingMessage();
                Console.Write("Ставка: ");
                double rate = 0;
                while (!Double.TryParse(Console.ReadLine(), out rate) || rate > ((Player)User).Money || rate <= 0)
                    Console.WriteLine("Введите корректное значение, которое можно примять за ставку.");
                ((Player)User).Rate = rate;
                MakeTurn();
            }
            else if (User.GetType() == typeof(ABot))
            {
                if (((ABot)User).IsWantNextGame)
                    MakeTurn();
            }
        }

        public void WriteGreetingMessage()
        {
            Console.WriteLine("Добро пожаловать в казино. Вы выбрали игру под названием БлэкДжек.\n" +
                "Дам доступны несколько комманд:\n" +
                "1) stand -> Если вы больше не хотите брать карты.\n" +
                "2) hit -> Если хотите, чтобы диллер дал вам еще карту.\n" +
                "3) double -> Если хотите удвоить ставку.\n" +
                "4) exit -> Если хотите прекратить играть.\n" +
                "Сейчас введите сумму ставки, после будет произведена раздача карт. \n" +
                "Вы можете увидеть их ниже. После выберете, что вы хотите делать дальше.\n");
        }

        private void WriteCard(ACard card)
        {
            /*ASCIIEncoding ascii = new ASCIIEncoding();
            switch (card.CardSuit)
            {
                case "Heart":
                    Console.WriteLine("♣❷\U0001F0AC");
                    break;
                case "Diamond":
                    Console.WriteLine("♣❷\U0001F0AC");
                    break;
                case "Club":
                    Console.WriteLine("♣❷\U0001F0AC");
                    break;
                case "Spade":
                    Console.WriteLine("♣❷\U0001F0AC");
                    break;
            }*/
            Console.WriteLine($"{card.CardName} - {card.CardSuit}");
        }

        private void WriteAllVisibleCards()
        {
            Console.WriteLine("Ваши карты: \n");
            foreach (var card in User.CardsInHand)
            {
                WriteCard(card);
            }
            Console.WriteLine("Общая сумма : " + User.GetSumOfCards());
            Console.WriteLine("Одна из карт дилера: \n");
            WriteCard(Dealer.VisibleCard);
        }

        private void WriteFinalResults()
        {
            Console.WriteLine("<----------------------------------------------->");
            Console.WriteLine("Результаты всей игры: ");
            Console.WriteLine("Ваши карты: \n");
            foreach (var card in User.CardsInHand)
            {
                WriteCard(card);
            }
            Console.WriteLine("Общая сумма : " + User.GetSumOfCards() + "\n");

            foreach (var card in Dealer.CardsInHand)
            {
                WriteCard(card);
            }
            Console.WriteLine("Общая сумма карт диллера: " + Dealer.GetSumOfCards() + "\n");
        }

        public PlayerTurn GetCommandFromUser()
        {
            string[] commands = { "stand", "hit", "double", "exit"};
            string command = "";
            Console.WriteLine("Ваша команда: ");
            command = Console.ReadLine().ToLower();
            while (!commands.Contains(command))
            {
                Console.WriteLine("Такой команды не существует повторите попытку.\n" +
                    "Ваша команда: ");
                command = Console.ReadLine().ToLower();
            }
            if (command == "hit")
                return PlayerTurn.Hit;
            else if (command == "stand")
                return PlayerTurn.Stand;
            else if (command == "double")
                return PlayerTurn.Double;
            return PlayerTurn.Exit;
        }

        public void GetAnotherCard(AParticipant player)
        {
            while (player.GetNextCard())
            {
                player.CardsInHand.Add(Deck.GetOneCard(player));
            }
        }

        public void MakeTurn() // TODO обработка бота
        {
            WriteAllVisibleCards();
            int playerTurn = (int)GetNextPlayerTurn();
            switch (playerTurn)
            {
                case 0:
                    ((Player)User).IsWantNextCard = true;
                    GetAnotherCard(User);
                    Console.Write("Вам досталась: ");
                    WriteCard(User.CardsInHand[User.CardsInHand.Count - 1]);
                    break;
                case 1:
                    GetAnotherCard(Dealer);
                    break;
                case 2:
                    ((Player)User).Rate *= 2;
                    Console.WriteLine("Ваша ставка увеличена в 2 раза. Ее размер составляет: " + ((Player)User).Rate);
                    ((Player)User).IsWantNextCard = true;
                    GetAnotherCard(User);
                    Console.Write("Вам досталась: ");
                    WriteCard(User.CardsInHand[User.CardsInHand.Count - 1]);
                    break;
                case 5:
                    return;
                default:
                    break;
            }
            CheckIsSomebodyWinOrLose(playerTurn);
        }

        public void MakeTurn(ABot bot)
        {
            int playerTurn = (int)GetNextPlayerTurn();
            switch (playerTurn)
            {
                case 0:
                    bot.IsWantNextCard = true; // TODO
                    GetAnotherCard(User);
                    break;
                case 1:
                    GetAnotherCard(Dealer);
                    break;
                case 2:
                    bot.Rate *= 2;
                    bot.IsWantNextCard = true; // TODO
                    GetAnotherCard(User);
                    break;
                case 5:
                     return;
                default:
                    break;
            }
        }

        public PlayerTurn GetAnswerAfterFirstBlackjack()
        {
            if (User.GetType() == typeof(Player))
            {
                Console.WriteLine("У вас блэкджэк, но у диллера первая карта туз, поэтому вы можете либо забрать выигрыш 1 к 1\n" +
                    "либо подождать конца игры и если у дилллера не будет блэкджека получить выигрыш 3 к 2.\n" +
                    "Введите либо take - чтобы забрать все сейчас, либо stand чтобы подождать конца игры");
                string answer = Console.ReadLine();
                while (answer != "take" && answer != "stand")
                    answer = Console.ReadLine().ToLower();
                return answer == "stand" ? PlayerTurn.Stand : PlayerTurn.Take;
            }
            else
            {
                return PlayerTurn.Exit; // TODO Bot
            }
        }

        public PlayerTurn GetNextPlayerTurn()
        {
            if (!(NumberOfMoves == 0 && User.GetSumOfCards() == 21))
                return GetCommandFromUser();
            
            if (Dealer.VisibleCard.CardName == "Ace")
            {
                PlayerTurn answer = GetAnswerAfterFirstBlackjack();
                if (answer == PlayerTurn.Take)
                    return PlayerTurn.Take;
                else
                 {
                    if (User.GetType() == typeof(Player))
                        ((Player)User).Multiplier = 1.5;
                    else if (User.GetType() == typeof(ABot))
                        ((ABot)User).Multiplier = 1.5;
                    return PlayerTurn.Stand;
                 }
            }

            else
            {
                if (User.GetType() == typeof(Player))
                    ((Player)User).Multiplier = 1.5;
                return Dealer.VisibleCard.CardNumber == 10 ? PlayerTurn.Stand: PlayerTurn.Blackjack;
            }     
        }

        public void CheckIsSomebodyWinOrLose(int playerTurn)
        {
            if (playerTurn == (int)PlayerTurn.Blackjack)
            {
                User.Win();
                ResetGame();
            }  

            else if (playerTurn == (int)PlayerTurn.Hit && User.GetSumOfCards() == 21)
            {
                GetAnotherCard(Dealer);
                if (User.GetType() == typeof(Player))
                    WriteFinalResults();
                if (Dealer.GetSumOfCards() != 21)
                    User.Win();
                else
                {
                    if (User.GetType() == typeof(Player))
                    {
                        Console.WriteLine("Пуш ---> У игрока и казино одинаковое кол-во очков." +
                                        "Все остаются при свои деньгах. ");
                        Console.WriteLine("<----------------------------------------------->\n\n");
                    }
                    User.CountGames++;
                }

                ResetGame();
            }
                
            else if (playerTurn == (int)PlayerTurn.Hit && User.GetSumOfCards() > 21)
            {
                WriteFinalResults();
                User.Lose();
                ResetGame();
            }

            else if (playerTurn == (int)PlayerTurn.Stand)
            {
                if (!(User.GetSumOfCards() == 21) && User.GetType() == typeof(Player))
                    WriteFinalResults();
                if (Dealer.GetSumOfCards() > 21 || User.GetSumOfCards() > Dealer.GetSumOfCards())
                    User.Win();
                else if (User.GetSumOfCards() < Dealer.GetSumOfCards())
                    User.Lose();
                else
                {
                    if (User.GetType() == typeof(Player))
                    {
                        WriteFinalResults();
                        Console.WriteLine("Пуш ---> У игрока и казино одинаковое кол-во очков." +
                                            "Все остаются при свои деньгах. ");
                        Console.WriteLine("<----------------------------------------------->\n\n");
                    }
                    User.CountGames++;
                }
                ResetGame();
            }

            else if (playerTurn == (int)PlayerTurn.Take)
            {
                if (User.GetType() == typeof(Player))
                {
                    Console.WriteLine("Вы решили взять ставку 1 к 1. Информация о столе: \n");
                    WriteFinalResults();
                }
                User.Win();
                ResetGame();
            }

            else
            {
                NumberOfMoves++;
                MakeTurn();
            }
        }

        private void ResetGame()
        {
            User.CardsInHand.Clear();
            Dealer.CardsInHand.Clear();
            Dealer.HandOutCards(Deck, User);
            NumberOfMoves = 0;
            if (User.GetType() == typeof(Player) && ((Player)User).Money <= 0)
            {
                Console.WriteLine("Вы больше не можете играть.");
                return;
            }
            if (User.GetType() == typeof(ABot) && !((ABot)User).IsWantNextGame)
                return;
            StartGame();
        }
    }
}
