public class Card
{
    private int cardNumber;
    private int cardSuit;

    public Card(int number, int suit)
    {
        cardNumber = number;
        cardSuit = suit;
    }

    public int GetCardNumber()
    {
        return cardNumber;
    }

    public int GetCardSuit()
    {
        return cardSuit;
    }
}