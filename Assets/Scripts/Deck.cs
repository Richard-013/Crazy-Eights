using System.Collections.Generic;
using System.Runtime.ExceptionServices;
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
        ShuffleDeck();
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

    void ShuffleDeck()
    {
        // Modern Fisher-Yates Shuffle altered to move every card at least once
        // https://en.wikipedia.org/wiki/Fisher%E2%80%93Yates_shuffle#The_modern_algorithm

        for (int i = 51; i > 0; i--)
        {
            int cardIndex = Random.Range(0, i-1);

            Card randomCard = deckCards[cardIndex];

            deckCards[cardIndex] = deckCards[i];
            deckCards[i] = randomCard;
        }
    }

    Card DrawCard()
    {
        Card nextCard = deckCards[0];
        deckCards.RemoveAt(0);

        return nextCard;
    }
}
