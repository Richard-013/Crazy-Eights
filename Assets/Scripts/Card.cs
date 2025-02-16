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

    public string ReadCard()
    {
        string cardStatement;

        switch (cardNumber)
        {
            case 1:
                cardStatement = "The Ace";
                break;
            case 11:
                cardStatement = "The Jack";
                break;
            case 12:
                cardStatement = "The Queen";
                break;
            case 13:
                cardStatement = "The King";
                break;
            default:
                cardStatement = "The " + cardNumber;
                break;
        }

        switch (cardSuit)
        {
            case 0:
                cardStatement = cardStatement + " of Clubs";
                return  cardStatement;
            case 1:
                cardStatement = cardStatement + " of Diamonds";
                return  cardStatement;
            case 2:
                cardStatement = cardStatement + " of Hearts";
                return  cardStatement;
            case 3:
                cardStatement = cardStatement + " of Spades";
                return  cardStatement;
            default:
                cardStatement = cardStatement + " of " + cardSuit;
                return  cardStatement;
        }
    }
}