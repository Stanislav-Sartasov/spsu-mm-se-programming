package hand;

import card.BlackjackCard;
import card.CardStatus;

import java.util.ArrayList;

public class Hand implements IHand {
	private ArrayList<BlackjackCard> cardsArray;

	public Hand() {
		cardsArray = new ArrayList<>();
	}

	public BlackjackCard getCardAt(int pos) {
		return cardsArray.get(pos);
	}

	public ArrayList<BlackjackCard> getVisibleCards() {
		ArrayList<BlackjackCard> result = new ArrayList<>();
		for (var card : cardsArray) {
			if (card.getCardStatus() == CardStatus.FACE_UP)
				result.add(card);
		}
		return result;
	}

	public void addCard(BlackjackCard card) {
		cardsArray.add(card);
	}

	public int getHandScore() {
		final int ACE_RANK = 14;
		final int ACE_VALUE = 11;

		int numberOfAces = 0;

		int totalScore = 0;
		for (var card : cardsArray) {
			if (card.getCard().rank() == ACE_RANK) {
				numberOfAces++;
				continue;
			}
			totalScore += Integer.min(card.getCard().rank(), 10);
		}

		for (int i = 0; i < numberOfAces; i++) {
			if (totalScore + ACE_VALUE > 21)
				totalScore += 1;
			else
				totalScore += ACE_VALUE;
		}

		return totalScore;
	}

	public boolean isBlackjack() {
		return (getHandScore() == 21 && cardsArray.size() == 2);
	}

	public void showCards() {
		for (int i = 0; i < cardsArray.size(); i++) {
			cardsArray.get(i).setCardStatus(CardStatus.FACE_UP);
		}
	}

	public void clear() {
		cardsArray.clear();
	}
}
