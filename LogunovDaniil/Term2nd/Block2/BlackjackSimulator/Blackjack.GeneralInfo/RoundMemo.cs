using DeckLibrary;

namespace Blackjack.GeneralInfo;

public class RoundMemo
{
	private readonly List<Card> DealerHand;
	private readonly List<Card> PlayerHand;
	public readonly RoundResult Result;
	public readonly int PlayerReturn;
	public readonly int PlayerBet;

	public RoundMemo(RoundResult res)
	{
		DealerHand = new List<Card>();
		PlayerHand = new List<Card>();
		Result = res;
	}

	public RoundMemo(List<Card> dealer,
		List<Card> player,
		int bet,
		int won,
		RoundResult res)
	{
		DealerHand = dealer;
		PlayerHand = player;
		PlayerBet = bet;
		PlayerReturn = won;
		Result = res;
	}

	public List<Card> GetDealersHandCopy()
	{
		return new List<Card>(DealerHand);
	}

	public List<Card> GetPlayersHandCopy()
	{
		return new List<Card>(PlayerHand);
	}
}
