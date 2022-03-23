package model.card;

public abstract class AbstractCard {
    protected Card card;

    public AbstractCard(int rank, Suits suit) {
        this.card = new Card(rank, suit);
    }

    public AbstractCard(Card card) {
        this.card = card;
    }
}
