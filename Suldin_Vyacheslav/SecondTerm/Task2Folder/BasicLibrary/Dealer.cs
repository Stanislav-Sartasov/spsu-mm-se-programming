using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibrary
{
    public class Dealer : Player
    {
        
        public void InitialDistribution(List<Gamester> gamesters, Shoes shoes, PlayerMove[,] condition)
        {
            for (int k = 0; k < 2; k++)
                for (int i = 0; i < gamesters.Count; i++)
                {
                    this.GiveCard(gamesters[i], 0, shoes);
                    condition[i , 0] = PlayerMove.Call;
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
            if (show) Game.ShowTable(this[0], gamesters);
            for (int i = 0; i < gamesters.Count; i++)
                for (int j = 0; j < 4 && gamesters[i].GetBet(j) != 0 ; j++)
                {
                    if (gamesters[i].GetSum(j) > 21)
                    {
                        this.CollectBets(gamesters[i], j);
                        if (show) Console.WriteLine("player's bust");                        
                        continue;
                    }
                    else if (this.sum[0] > 21)
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

                    if (this.sum[0] == gamesters[i].GetSum(j))
                    {
                        gamesters[i].ChangeBank(gamesters[i].GetBet(j));
                        this.CollectBets(gamesters[i], j);
                        if (show) Console.WriteLine($"dealer: {this.sum[0]} , player: {gamesters[i].GetSum(j)} - draw");
                        continue;
                    }

                    if (gamesters[i].IsBlackJack(j))
                    {
                        gamesters[i].ChangeBank(gamesters[i].GetBet(j) * 5 / 2);
                        this.CollectBets(gamesters[i], j);
                        if (show) Console.WriteLine($"dealer: {this.sum[0]} , player: BJ - player won");
                        continue;
                    }

                    if (this.sum[0] < gamesters[i].GetSum(j))
                    {
                        gamesters[i].ChangeBank(gamesters[i].GetBet(j) * 2);
                        this.CollectBets(gamesters[i], j);
                        if (show) Console.WriteLine($"dealer: {this.sum[0]} < player: {gamesters[i].GetSum(j)}");
                        continue;
                    }
                    else
                    {
                        this.CollectBets(gamesters[i], j);
                        if (show) Console.WriteLine($"dealer: {this.sum[0]} > player: {gamesters[i].GetSum(j)}");
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

        public bool Ask(List<Gamester> gamesters, Shoes shoes, PlayerMove[,] condition )
        {
            
            for (int i = 0; i < gamesters.Count; i++)
                for (int j = 0; j < 4 && gamesters[i].GetBet(j) != 0; j++)
                {
                    if (gamesters[i].GetSum(j) >= 21 || gamesters[i].IsBlackJack(j))
                        condition[i, j] = PlayerMove.Pass;

                    if (condition[i, j] != PlayerMove.Pass)
                    {
                        condition[i, j] = gamesters[i].Answer(j,this[0], gamesters);
                        switch (condition[i, j])
                        {
                            case PlayerMove.Pass:
                                {
                                    break;
                                }
                            case PlayerMove.Call:
                                {
                                    this.GiveCard(gamesters[i], j, shoes);
                                    break;
                                }
                            case PlayerMove.Double:
                                {
                                    gamesters[i].ChangeBank(-gamesters[i].GetBet(j));
                                    gamesters[i].SetBet(j, gamesters[i].GetBet(j) * 2);
                                    this.GiveCard(gamesters[i], j, shoes);
                                    condition[i, j] = PlayerMove.Pass;
                                    break;
                                }
                            case PlayerMove.Split:
                                {
                                    for (int k = j + 1; k < 4; k++)
                                        if (condition[i, k] == PlayerMove.Pass)
                                        {
                                            gamesters[i].SplitCards(j, k);
                                            condition[i, k] = PlayerMove.Call;
                                            this.GiveCard(gamesters[i], j, shoes);
                                            this.GiveCard(gamesters[i], k, shoes);
                                            break;
                                        }
                                    break;
                                }
                            case PlayerMove.Surrender:
                                {
                                    condition[i, j] = PlayerMove.Pass;
                                    gamesters[i].ChangeBank(gamesters[i].GetBet(j) / 2);
                                    this.CollectBets(gamesters[i], j);
                                    break;
                                }
                        }
                        return true;
                    }

                    else if (gamesters[i].GetSum(j) + 10 < 21 &&
                        gamesters[i][j].Exists(x => x.GetCardValue() == 1))
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
            if (to[hand].Count == 0)
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
