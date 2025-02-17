using UnityEngine;

public class Card : MonoBehaviour
{
    public int number;
    public int suit;

    public string ReadCard()
    {
        string cardStatement;

        switch (suit)
        {
            case 0:
                cardStatement = "cardClubs";
                break;
            case 1:
                cardStatement = "cardDiamonds";
                break;
            case 2:
                cardStatement = "cardHearts";
                break;
            case 3:
                cardStatement = "cardSpades";
                break;
            default:
                cardStatement = "card" + suit;
                break;
        }

        switch (number)
        {
            case 1:
                cardStatement += "A";
                return cardStatement;
            case 11:
                cardStatement += "J";
                return cardStatement;
            case 12:
                cardStatement += "Q";
                return cardStatement;
            case 13:
                cardStatement += "K";
                return cardStatement;
            default:
                cardStatement += number.ToString();
                return cardStatement;
        }
    }
}