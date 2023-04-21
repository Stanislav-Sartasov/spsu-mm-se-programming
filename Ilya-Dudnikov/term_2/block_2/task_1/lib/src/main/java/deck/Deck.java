package deck;

import card.Card;
import card.Suits;

import java.util.ArrayList;
import java.util.Collections;

public class Deck implements IDeck {
	private ArrayList<Card> cardsArray;

	public Deck() {
		cardsArray = new ArrayList<>();
		for (var cardSuit : Suits.values()) {
			for (int rank = 2; rank <= 14; rank++) {
				cardsArray.add(new Card(rank, cardSuit));
			}
		}
	}

	public Card getCardAt(int pos) {
		return cardsArray.get(pos);
	}

	public void shuffle() {
		Collections.shuffle(cardsArray);
	}
}
