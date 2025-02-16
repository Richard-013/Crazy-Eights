using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<Card> hand = new List<Card>();
    private bool isOpponent = true;
    private Card lastPlayedCard;
    
    public GameManager game;

    void Awake()
    {
        
    }

    public void SetAsPlayer()
    {
        isOpponent = false;
    }

    public void AddCardToHand(Card newCard)
    {
        hand.Add(newCard);
    }

    public void ShowHand()
    {
        for(int i = 0; i < hand.Count; i++)
        {
            Debug.Log(hand[i].ReadCard());
        }
    }

    public Card Turn(bool isOpponentTurn, Card recentCard)
    {
        lastPlayedCard = recentCard;

        if(isOpponentTurn)
        {
            return OpponentTurn();
        }
        else
        {
            return PlayerTurn();
        }
    }

    Card PlayerTurn()
    {
        Debug.Log("The last card to be played was: " + lastPlayedCard.ReadCard());
        return hand[0];
    }

    Card OpponentTurn()
    {
        return hand[0];
    }

    bool IsValidMove(Card chosenCard)
    {
        if(chosenCard.GetCardSuit() == lastPlayedCard.GetCardSuit())
        {
            return true;
        }
        else if(chosenCard.GetCardNumber() == lastPlayedCard.GetCardNumber())
        {
            return true;
        }

        return false;
    }
}
