using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary
{
    public class Dealer : Player
    {
        
        public void InitialDistribution(List<Gamester> gamesters, Shoes shoes, int[,] condition)
        {
            for (int k = 0; k < 2; k++)
                for (int i = 0; i < gamesters.Count; i++)
                {
                    this.GiveCard(gamesters[i], 0, shoes);
                    condition[i , 0] = 1;
                }
                    
            this.GiveCard(this, 0, shoes);
        }

        public void CalculateBets(List<Gamester> gamesters)
        {
            bool show = false;
            for (int i = 0; i < gamesters.Count; i++)
            {
                if (gamesters[i].IsNeedResult())
                {
                    show = true;
                    break;
                }
            }
            if (show) Game.ShowTable(this.ScanHand(0), gamesters);
            for (int i = 0; i < gamesters.Count; i++)
                for (int j = 0; j < 4 && gamesters[i].GetBet(j) != 0 ; j++)
                {
                    if (gamesters[i].GetSum(j) > 21)
                    {
                        this.CollectBets(gamesters[i], j);
                        if (show) Console.WriteLine("player's bust");                        
                        continue;
                    }
                    else if (this.Sum[0] > 21)
                    {
                        gamesters[i].ChangeBank(gamesters[i].GetBet(j) * 2);
                        this.CollectBets(gamesters[i], j);
                        if (show) Console.WriteLine("dealer's bust");
                        continue;
                    }

                    if (this.IsBlackJack(0))
                    {
                        if (gamesters[i].IsBlackJack(j))
                        {
                            gamesters[i].ChangeBank(gamesters[i].GetBet(j));
                            this.CollectBets(gamesters[i], j);
                            if (show) Console.WriteLine("dealer: BJ , player: BJ - draw");
                            continue;
                        }
                        else
                        {
                            this.CollectBets(gamesters[i], j);
                            if (show) Console.WriteLine($"dealer: BJ , player: {gamesters[i].GetSum(j)} - dealer won");
                            continue;
                        }
                    }

                    if (this.Sum[0] == gamesters[i].GetSum(j))
                    {
                        gamesters[i].ChangeBank(gamesters[i].GetBet(j));
                        this.CollectBets(gamesters[i], j);
                        if (show) Console.WriteLine($"dealer: {this.Sum[0]} , player: {gamesters[i].GetSum(j)} - draw");
                        continue;
                    }

                    if (gamesters[i].IsBlackJack(j))
                    {
                        gamesters[i].ChangeBank(gamesters[i].GetBet(j) * 5 / 2);
                        this.CollectBets(gamesters[i], j);
                        if (show) Console.WriteLine($"dealer: {this.Sum[0]} , player: BJ - player won");
                        continue;
                    }

                    if (this.Sum[0] < gamesters[i].GetSum(j))
                    {
                        gamesters[i].ChangeBank(gamesters[i].GetBet(j) * 2);
                        this.CollectBets(gamesters[i], j);
                        if (show) Console.WriteLine($"dealer: {this.Sum[0]} < player: {gamesters[i].GetSum(j)}");
                        continue;
                    }
                    else
                    {
                        this.CollectBets(gamesters[i], j);
                        if (show) Console.WriteLine($"dealer: {this.Sum[0]} > player: {gamesters[i].GetSum(j)}");
                        continue;
                    }

                }
        }

        public void GetCardsBack(List<Gamester> gamesters)
        {
            for (int i = 0; i < gamesters.Count; i++)
                    gamesters[i].Discard();

            this.Discard();
        }

        public bool Ask(List<Gamester> gamesters, Shoes shoes, int[,] condition )
        {
            
            for (int i = 0; i < gamesters.Count; i++)
                for (int j = 0; j < 4 && gamesters[i].GetBet(j) != 0; j++)
                {
                    if (gamesters[i].GetSum(j) >= 21 || gamesters[i].IsBlackJack(j))
                        condition[i, j] = 0;

                    if (condition[i, j] != 0)
                    {
                        condition[i, j] = gamesters[i].Answer(j,this.ScanHand(0), gamesters);
                        switch (condition[i, j])
                        {
                            case 0: // stand on cards
                                {
                                    break;
                                }
                            case 1: //h it me
                                {
                                    this.GiveCard(gamesters[i], j, shoes);
                                    break;
                                }
                            case 2: //double
                                {
                                    gamesters[i].ChangeBank(-gamesters[i].GetBet(j));
                                    gamesters[i].SetBet(j, gamesters[i].GetBet(j) * 2);
                                    this.GiveCard(gamesters[i], j, shoes);
                                    condition[i, j] = 0;
                                    break;
                                }
                            case 3: //split
                                {
                                    for (int k = j + 1; k < 4; k++)
                                        if (condition[i, k] == 0)
                                        {
                                            gamesters[i].SplitCards(j, k);
                                            condition[i, k] = 1;
                                            this.GiveCard(gamesters[i], j, shoes);
                                            this.GiveCard(gamesters[i], k, shoes);
                                            break;
                                        }
                                    break;
                                }
                            default: //sorendo
                                {
                                    condition[i, j] = 0;
                                    gamesters[i].ChangeBank(gamesters[i].GetBet(j) / 2);
                                    this.CollectBets(gamesters[i], j);
                                    break;
                                }
                        }
                        return true;
                    }

                    else if (gamesters[i].GetSum(j) + 10 < 21 &&
                        gamesters[i].ScanHand(j).Exists(x => x.GetCardInfo()[0] == 1))
                    {
                        gamesters[i].ConfirmBlackJack(j);
                    }

                }
                    
            return false;
                        
        }
        public void GiveCard(Player to, int hand, Shoes from)
        {
            to.ReceiveCard(from.Withdraw(), hand);
        }
        public void GiveBlackJack(Player to, int hand)
        {
            if (to.ScanHand(hand).Count == 0)
            {
                to.ReceiveCard(new Card(CardSuit.Clubs, CardRank.King), hand);
                to.ReceiveCard(new Card(CardSuit.Clubs, CardRank.Ace), hand);
            }
        }

        public void CollectBets(Gamester gamester, int hand)
        {
            gamester.ReturnBet(hand);
        }

    }
}
