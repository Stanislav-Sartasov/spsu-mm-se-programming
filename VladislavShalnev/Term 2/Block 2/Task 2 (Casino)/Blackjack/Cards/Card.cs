namespace Blackjack.Cards;

public record Card
{
	internal CardSuit Suit { get; }
	internal CardType Type { get; }

	public Card(CardSuit suit, CardType type)
	{
		Suit = suit;
		Type = type;
	}

	internal int Value => Type switch
	{
		CardType.Two => 2,
		CardType.Three => 3,
		CardType.Four => 4,
		CardType.Five => 5,
		CardType.Six => 6,
		CardType.Seven => 7,
		CardType.Eight => 8,
		CardType.Nine => 9,
		CardType.Ten => 10,
		CardType.Jack => 10,
		CardType.Queen => 10,
		CardType.King => 10,
		_ => 0
	};

	public override string ToString() =>
		$"{Suit.ToString()} {Type.ToString()}";
}