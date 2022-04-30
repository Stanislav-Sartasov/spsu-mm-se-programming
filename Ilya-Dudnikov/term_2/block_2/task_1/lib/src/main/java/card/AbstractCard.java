package card;

public abstract class AbstractCard {
    protected Card card;

    public AbstractCard(int rank, Suits suit) {
        this.card = new Card(rank, suit);
    }

    public Card getCard() {
        return card;
    }
}
