using System.Collections.Generic;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public List<Card> deckCards = new List<Card>();

    private Dictionary<string, int> Suits = new Dictionary<string, int>()
    {
        { "Clubs", 0 },
        { "Diamonds", 1 },
        { "Hearts", 2 },
        { "Spades", 3 }
    };

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GenerateDeck();

        for(int i = 0; i < 52; i++)
        {
            Debug.Log(deckCards[i].GetCardNumber() + ", " + deckCards[i].GetCardSuit());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void GenerateDeck()
    {
        for(int i = 1; i < 53; i++)
        {
            if(i < 14)
            {
                deckCards.Add(new Card(i, Suits["Clubs"]));
            }
            else if(i < 27)
            {
                deckCards.Add(new Card(i-13, Suits["Diamonds"]));
            }
            else if(i < 40)
            {
                deckCards.Add(new Card(i-26, Suits["Hearts"]));
            }
            else
            {
                deckCards.Add(new Card(i-39, Suits["Spades"]));
            }
        }
    }
}
