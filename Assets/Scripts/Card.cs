using UnityEngine;

public class Card : MonoBehaviour
{
    public int number;
    public int suit;

    public string ReadCard()
    {
        string cardStatement;

        switch (number)
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
                cardStatement = "The " + number;
                break;
        }

        switch (suit)
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
                cardStatement = cardStatement + " of " + suit;
                return  cardStatement;
        }
    }
}