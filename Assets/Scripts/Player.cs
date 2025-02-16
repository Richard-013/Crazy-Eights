using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private List<Card> hand = new List<Card>();
    private bool isOpponent = true;
    
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

    public void Turn(bool isOpponentTurn)
    {
        if(isOpponentTurn)
        {
            OpponentTurn();
        }
        else
        {
            PlayerTurn();
        }
    }

    void PlayerTurn()
    {
        Debug.Log("The last card to be played was: " + game.lastPlayedCard.ReadCard());
    }

    void OpponentTurn()
    {

    }
}
