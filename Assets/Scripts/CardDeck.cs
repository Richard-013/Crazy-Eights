using System.Collections.Generic;
using UnityEngine;

public class CardDeck
{
    public List<Card> deck = new List<Card>();

    private Dictionary<string, int> Suits = new Dictionary<string, int>()
    {
        { "Clubs", 0 },
        { "Diamonds", 1 },
        { "Hearts", 2 },
        { "Spades", 3 }
    };

    void Awake()
    {
        GenerateDeck();
        ShuffleDeck();
    }

    void GenerateDeck()
    {
        for(int i = 1; i < 53; i++)
        {
            if(i < 14)
            {
                deck.Add(new Card(i, Suits["Clubs"]));
            }
            else if(i < 27)
            {
                deck.Add(new Card(i-13, Suits["Diamonds"]));
            }
            else if(i < 40)
            {
                deck.Add(new Card(i-26, Suits["Hearts"]));
            }
            else
            {
                deck.Add(new Card(i-39, Suits["Spades"]));
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

            Card randomCard = deck[cardIndex];

            deck[cardIndex] = deck[i];
            deck[i] = randomCard;
        }
    }

    public Card DrawCard()
    {
        Card nextCard = deck[0];
        deck.RemoveAt(0);

        return nextCard;
    }
}
