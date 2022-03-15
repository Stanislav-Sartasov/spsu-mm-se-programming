package model.deck;

import model.card.BlackjackCard;

import java.util.ArrayList;
import java.util.Collections;

public class Shoe {
	private ArrayList<Deck> decksArray;
	private int currentCardPointer;
	private int currentDeckPointer;

	public final int DECK_LENGTH = 52;

	private Shoe() {
		decksArray = new ArrayList<>();
		currentCardPointer = 0;
		currentDeckPointer = 0;
	}

	public static Shoe createShoe() {
		Shoe shoe = new Shoe();
		shoe.decksArray.add(new Deck());
		shoe.shuffle();

		return shoe;
	}

	public Shoe createShoeWithNDecks(int numberOfDecks) {
		if (numberOfDecks < 1 || numberOfDecks > 8)
			throw new IllegalArgumentException("You cannot play Blackjack with given amount of decks in a shoe");

		Shoe shoe = new Shoe();
		for (int i = 0; i < numberOfDecks; i++) {
			shoe.decksArray.add(new Deck());
		}
		shoe.shuffle();

		return shoe;
	}

	public void shuffle() {
		Collections.shuffle(decksArray);
		for (var deck : decksArray) {
			deck.shuffle();
		}
	}

	public void resetShoe() {
		this.shuffle();
		currentCardPointer = 0;
		currentDeckPointer = 0;
	}

	public BlackjackCard dealCard() {
		currentCardPointer++;
		if (currentCardPointer >= DECK_LENGTH) {
			currentCardPointer = 0;
			currentDeckPointer++;
		}

		if (currentDeckPointer >= decksArray.size()) {
			this.resetShoe();
		}


		return new BlackjackCard(decksArray.get(currentDeckPointer).getCardAt(currentCardPointer));
	}
}
