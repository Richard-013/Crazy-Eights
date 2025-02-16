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

}
