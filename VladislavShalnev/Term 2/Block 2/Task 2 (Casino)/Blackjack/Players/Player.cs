using Blackjack.Cards;

namespace Blackjack.Players;

public abstract class Player : BasePlayer
{
	public string Name { get; init; }

	private int _money;
	public int Money { 
		get => _money;
		set => _money = Math.Max(value, 0);
	}
	
	private int _bet;
	public int Bet
	{
		get => _bet;
		protected set => _bet = Math.Max(Math.Min(Money, value), 0);
	}

	internal PlayerAction Action =>
		Score > 18 ? PlayerAction.Stand : PlayerAction.Hit;
	
	// Player events
	public event Action? OnLoss;
	public event Action? OnWin;
	public event Action? OnTie;
	public event Action? OnKick;
	public event Action? OnHit;
	public event Action? OnStand;
	public event Action? OnBet;

	public virtual void MakeBet() =>
		OnBet?.Invoke();

	public override void Clear()
	{
		base.Clear();
		Bet = 0;
	}

	public void Hit(Card card)
	{
		OnHit?.Invoke();
		TakeCard(card);
	}
	
	public void Stand() =>
		OnStand?.Invoke();
	
	public void Kick() =>
		OnKick?.Invoke();

	public void Finish(Dealer dealer)
	{
		base.Finish();
		if (Score > 21)
		{
			// Loss
			Money -= Bet;
			OnLoss?.Invoke();
		}
		else if (Score == 21)
		{
			if (HasBlackjack)
			{
				if (dealer.HasBlackjack)
				{
					// Tie
					OnTie?.Invoke();
				}
				else
				{
					// Blackjack!
					Money += (int) (1.5 * Bet);
					OnWin?.Invoke();
				}
			}
			else
			{
				if (dealer.Score == 21)
				{
					// Tie
					OnTie?.Invoke();
				}
				else
				{
					// Win!
					Money += Bet;
					OnWin?.Invoke();
				}
			}
		}
		else
		{
			if (dealer.Score == Score)
			{
				// Tie
				OnTie?.Invoke();
			}
			else if (dealer.Score < Score || dealer.Score > 21)
			{
				// Win!
				Money += Bet;
				OnWin?.Invoke();
			}
			else
			{
				// Loss
				Money -= Bet;
				OnLoss?.Invoke();
			}
		}
	}
}