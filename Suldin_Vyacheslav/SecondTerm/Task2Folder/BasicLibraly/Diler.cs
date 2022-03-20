﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BasicLibraly
{
    public class Diler : Player
    {
        public Deck MixDeck(Deck deck)
        {
            Random rand = new Random();

            for (int i = deck.Cards.Length - 1; i >= 1; i--)
            {
                int j = rand.Next(i + 1);

                Card tmp = deck.Cards[j];
                deck.Cards[j] = deck.Cards[i];
                deck.Cards[i] = tmp;
            }
            return deck;
        }

        public void FillShoe(Shoes to, int numberOfDecks)
        {
            to.Queue.Clear();
            to.Current = 0;
            for (int i = 0; i < numberOfDecks; i++)
            {
                Deck deck = new Deck();
                this.MixDeck(deck);
                to.Queue.AddRange(deck.Cards); 
            }
            
        }

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

            for (int i = 0; i < gamesters.Count; i++)
                for (int j = 0; j<4 && gamesters[i].Bets[j] !=0 ; j++)
                {
                    if (gamesters[i].Sum[j]>21)
                    {
                        gamesters[i].Bets[j] = 0;
                        continue;
                    }
                    else if (this.Sum[0]>21)
                    {
                        gamesters[i].Bank += gamesters[i].Bets[j] * 2;
                        gamesters[i].Bets[j] = 0;
                        continue;
                    }

                    if (this.IsBlackJack(0))
                    {
                        if (gamesters[i].IsBlackJack(j))
                        {
                            gamesters[i].Bank += gamesters[i].Bets[j];
                            gamesters[i].Bets[j] = 0;
                            continue;
                        }
                        else
                        {
                            gamesters[i].Bets[j] = 0;
                            continue;
                        }
                    }

                    if (this.Sum[0] == gamesters[i].Sum[j])
                    {
                        gamesters[i].Bank += gamesters[i].Bets[j];
                        gamesters[i].Bets[j] = 0;
                        continue;
                    }

                    if (gamesters[i].IsBlackJack(j))
                    {
                        gamesters[i].Bank += gamesters[i].Bets[j] * 5 / 2;
                        gamesters[i].Bets[j] = 0;
                        continue;
                    }

                    if (this.Sum[0] < gamesters[i].Sum[j])
                    {
                        gamesters[i].Bank += gamesters[i].Bets[j] * 2;
                        gamesters[i].Bets[j] = 0;
                        continue;
                    }
                    else
                    {
                        gamesters[i].Bets[j] = 0;
                        continue;
                    }

                }
        }

        public void GetCardsBack(List<Gamester> gamesters)
        {
            for (int i = 0; i < gamesters.Count; i++)
                for (int j = 0; j < 4; j++)
                {
                    gamesters[i].Hands[j].Clear();
                    gamesters[i].Sum[j] = 0;
                }
            this.Hands[0].Clear();
            this.Sum[0] = 0;
        }

        public bool Ask(List<Gamester> gamesters, Shoes shoes, int[,] condition )
        {
            for (int i = 0; i < gamesters.Count; i++)
                for (int j = 0; j<4 && gamesters[i].Bets[j]!=0; j++)
                {
                    if (gamesters[i].Sum[j]>=21 || gamesters[i].IsBlackJack(j))
                        condition[i, j] = 0;

                    if (condition[i, j] != 0)
                    {
                        condition[i, j] = gamesters[i].Answer(j,this.Hands[0],gamesters, shoes);
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
                                    gamesters[i].Bank -= gamesters[i].Bets[j];
                                    gamesters[i].Bets[j] *= 2;
                                    this.GiveCard(gamesters[i], j, shoes);
                                    condition[i, j] = 0;
                                    break;
                                }
                            case 3: //split
                                {
                                    for (int k = j + 1; k < 4; k++)
                                        if (condition[i, k] == 0)
                                        {
                                            gamesters[i].Hands[k].Add(gamesters[i].Hands[j][0]);
                                            gamesters[i].Hands[j].Remove(gamesters[i].Hands[j][0]);
                                            gamesters[i].Sum[j] /= 2;
                                            gamesters[i].Sum[k] += gamesters[i].Sum[j];
                                            gamesters[i].Bets[k] = gamesters[i].Bets[j];
                                            gamesters[i].Bank -= gamesters[i].Bets[j];
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
                                    gamesters[i].Bank += gamesters[i].Bets[j] / 2;
                                    gamesters[i].Bets[j] = 0;
                                    break;
                                }
                        }
                        return true;
                    }

                    else if (gamesters[i].Sum[j] + 10 < 21 &&
                        gamesters[i].Hands[j].Exists(x => x.Rank == "Ace"))
                    {
                        gamesters[i].Sum[j] += 10;
                    }

                }
                    
            return false;
                        
        }
        public void GiveCard(Player to, int hand, Shoes from)
        {
            to.ReceiveCard(from.Queue[from.Current], hand);
            from.Current++;
        }
        public void GiveBlackJack(Player to, int hand)
        {
            if (to.Hands[hand].Count == 0)
            {
                to.ReceiveCard(new Card("diamonds", "Jack"), hand);
                to.ReceiveCard(new Card("diamonds", "Ace"), hand);
            }
        }
    }
}
